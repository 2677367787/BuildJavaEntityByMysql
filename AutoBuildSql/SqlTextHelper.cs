/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：SqlHelper
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/2/10 11:37:36
** 修改记录： 
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Xsl;

namespace AutoBuildSql
{
    public class SqlTextHelper
    {
        /// <summary>
        /// 表别名和表的对应关系
        /// </summary>
        private static readonly Dictionary<string, string> DictTbNameRelt = new Dictionary<string, string>();

        /// <summary>
        /// 主表和关联表字段关联关系
        /// </summary>
        private static readonly IList<TableRelt> ListTableRelt = new List<TableRelt>();
        public static Dictionary<string, IList<string>> Analysis(string originalSqlText,string dataBaseName)
        {
            DictTbNameRelt.Clear();
            ListTableRelt.Clear();
            LocalData.Logs.Clear();

            Dictionary<string, IList<string>> dictSqlText = new Dictionary<string, IList<string>>();
            IList<string> listAdd = new List<string>();
            IList<string> listDel = new List<string>();
            IList<string> listUpd = new List<string>();
            dictSqlText.Add("add", listAdd);
            dictSqlText.Add("del", listDel);
            dictSqlText.Add("upd", listUpd);

            try
            {
                string sqlText = originalSqlText.Replace("\r\n", " ").Replace("`","").ToLower();
                sqlText = Regex.Replace(sqlText, "\\s{2,}", " ");
                int formIndex = sqlText.IndexOf(" from ", StringComparison.Ordinal);
                int whereIndex = sqlText.IndexOf(" where ", StringComparison.Ordinal);
                string tableAndRelation = sqlText.Substring(formIndex + 6, whereIndex - formIndex);
                string conditonsStr = sqlText.Substring(whereIndex + 7); 

                //要执行查询的SQL语句
                string excutSql = sqlText.Substring(formIndex);

                IList<string> condList = new List<string>();
                //去除关键字
                IList<string> list = RemoveKeyWord(tableAndRelation, excutSql, condList);
                
                foreach (var tables in list)
                {
                    var tableNames = tables.Trim().Split(' ');
                    if (tableNames.Length > 1)
                    {
                        DictTbNameRelt.Add(tableNames[1], tableNames[0]);
                    }
                    else
                    {
                        DictTbNameRelt.Add(tableNames[0], tableNames[0]);
                    }
                    string tableAbbName = tableNames[0];
                    string tableName = string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);
                    LocalData.Logs.AppendLine("解析后表名：" + tableName);
                    //excutSql = excutSql.Replace(tableAbbName, tableName);
                }
                foreach (var con in condList)
                {
                    ResolveCond(con);
                }
                ResolveCond(conditonsStr);

                DataSet ds = new DataSet();
                foreach (var tables in list)
                {
                    var tableNames = tables.Trim().Split(' ');
                    string tableAbbName = tableNames[0];
                    string tableName = tableAbbName;//string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);
                    string tableSchemaName = string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);
                    string tableFullName = tableNames[0];
                    if (tableNames.Length > 1)
                    {
                        tableAbbName = tableNames[1];
                    }

                    //查询数据要去重
                    string sql = string.Format("select DISTINCT {0}.* {1}", tableAbbName, excutSql);
                    LocalData.Logs.AppendLine("执行SQL：" + sql);
                    DataTable dt = MySqlHelper.GetDataSetBySqlText(sql).Tables[0];
                    dt.TableName = tableName;
                    ds.Tables.Add(dt.Copy());
                }

                DataTable dtColumnInfo = DataHelper.GetAllColumn();
                Dictionary<string, Dictionary<string, string>> tbAndKeyCol =
                    new Dictionary<string, Dictionary<string, string>>();
                //遍历所有表,查找每个表的主键,根据主键类型生成新主键，然后修改关联的外键表
                foreach (DataTable dt in ds.Tables)
                {
                    int i = 0;
                    string tbName = dt.TableName;
                    Dictionary<string, string> keyCol = new Dictionary<string, string>();
                    IList<string> listKeys = DataHelper.GetKey(tbName, dataBaseName);
                    tbAndKeyCol.Add(tbName, keyCol);
                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (var key in listKeys)
                        {
                            if (i == 0)
                            {
                                keyCol.Add(key, key);
                            }
                            
                            //查找所有主键
                            DataRow[] colDr = dtColumnInfo.Select(
                                string.Format("TABLE_SCHEMA='{0}' AND COLUMN_NAME='{1}' and TABLE_NAME='{2}'",
                                    dataBaseName,
                                    key, tbName));
                            string dataType = colDr[0]["data_type"].ToString().ToLower();
                            string colLength = colDr[0]["COLUMN_TYPE"].ToString().ToLower();
                            string id="";
                            switch (dataType)
                            {
                                case "int":
                                    id = DataHelper.GetIntKey(tbName, key);
                                    break;
                                case "varchar":
                                    string unCode = dr[key].ToString();
                                    if (unCode.IndexOf("-", StringComparison.Ordinal) != -1 && unCode.Length >= 20)
                                    {
                                        id = Guid.NewGuid().ToString();
                                        //unCode＝unCode;
                                    }
                                    else
                                    {
                                        int dataLength = int.Parse(DataHelper.GetRegexValue(colLength));
                                        int valueLenght = unCode.Length;
                                        if (valueLenght + 4 > dataLength)
                                        {
                                            id = "Test" +
                                                     unCode.Substring(0, dataLength - (valueLenght + 4 - dataLength));
                                        }
                                        else
                                        {
                                            id = unCode;
                                        }
                                    }
                                    break;
                                default:
                                    id = "";
                                    break;
                            }

                            dr[key] = id;

                            //查找此ID作为外键的表条件在左
                            List<TableRelt> ftbList= ListTableRelt.Where(t => t.FtbName == tbName && t.FcolName == key).ToList();
                            //循环所有关联列赋值
                            foreach (TableRelt ftb in ftbList)
                            {
                                foreach (DataRow fdr in ds.Tables[ftb.PtbName].Rows)
                                {
                                    fdr[ftb.PcolName] = id;
                                }
                                ListTableRelt.Remove(ftb);
                            }

                            //查找此ID作为外键的表条件在右
                            List<TableRelt> ptbList = ListTableRelt.Where(t => t.PtbName == tbName && t.PcolName == key).ToList();
                            //循环所有关联列赋值
                            foreach (TableRelt ptb in ptbList)
                            {
                                foreach (DataRow fdr in ds.Tables[ptb.FtbName].Rows)
                                {
                                    fdr[ptb.FcolName] = id;
                                }
                                ListTableRelt.Remove(ptb);
                            }
                        }
                        i++;
                    }
                }

                foreach (DataTable dt in ds.Tables)
                {
                    string tbName = dt.TableName;
                    DataTable dtColmnInfo = DataHelper.GetColumnByTableName(tbName);

                    string insertSqlText = BuildInsertSqlText(dt, dtColmnInfo, tbName);
                    listAdd.Add(insertSqlText);
                    LocalData.Logs.AppendLine("生成插入语句：" + insertSqlText);

                    string updateSqlText = BuildUpdateSqlText(dt, dataBaseName, tbName);
                    listUpd.Add(updateSqlText);
                    LocalData.Logs.AppendLine("生成更新语句：" + updateSqlText);

                    string deleteSqlText = BuildDeleteSqlText(dt, dataBaseName, tbName);
                    listDel.Add(deleteSqlText);
                    LocalData.Logs.AppendLine("生成删除语句：" + deleteSqlText);
                }
            }
            catch (Exception ex)
            {
                LocalData.Logs.Append("出现异常："+ex.Message);
            }
            return dictSqlText;
        } 

        private static IList<string> RemoveKeyWord(string strSql, string excutSql, IList<string> condList)
        {
            IList<string> list = new List<string>();
            list.Add(strSql);
            string[] keyWrods =
            {
                "left outer join",
                "right outer join",
                "inner outer join",
                "inner join",
                "left join",
                "right join",
                " join ",
                " on "
            }; 
            foreach (string keyWrod in keyWrods)
            {
                var list2 = GetStrList(keyWrod, list, condList);
                list.Clear();
                list = list2;
            }
            
            return list;
        }

        private static IList<string> GetStrList(string keyWrod, IList<string> list, IList<string> condList)
        {
            IList<string> list2 = new List<string>();
            foreach (string sqls in list)
            {
                string[] sql = sqls.Split(new[] { keyWrod }, StringSplitOptions.None);
                if (keyWrod == " on ")
                {
                    list2.Add(sql[0]);
                    if(sql.Length == 2)
                        condList.Add(sql[1]);
                }
                else
                {
                    foreach (string s in sql)
                    {
                        list2.Add(s);
                    }
                }
            }
            return list2;
        }

        /// <summary>
        /// 解析条件语句
        /// </summary>
        /// <param name="conditonsStr"></param>
        private static void ResolveCond(string conditonsStr)
        {
            string[] conditons = conditonsStr.Split(new[] { " and " }, StringSplitOptions.None);
            foreach (var conditon in conditons)
            {
                string[] cond = conditon.Split(new[] { "=" }, StringSplitOptions.None);
                string leftCond = cond[0].Trim();
                string rightCond = cond[1].Trim();
                //排除固定值 int类型
                if (Utils.IsNumeric(leftCond) || Utils.IsNumeric(rightCond)) continue;
                //排除固定值 单引号内容
                if (leftCond.IndexOf('\'') != -1 && leftCond.IndexOf('\'') != leftCond.LastIndexOf('\'')) continue;
                if (rightCond.IndexOf('\'') != -1 && rightCond.IndexOf('\'') != rightCond.LastIndexOf('\'')) continue;
                string[] tbcolumn = leftCond.Split('.');
                string[] tbcolumn2 = rightCond.Split('.');
                if (DictTbNameRelt.ContainsKey(tbcolumn[0]) && DictTbNameRelt.ContainsKey(tbcolumn2[0]))
                {
                    string pTbName = DictTbNameRelt[tbcolumn[0]];
                    string fTbName = DictTbNameRelt[tbcolumn2[0]];
                    TableRelt tr = new TableRelt
                    {
                        PtbName = pTbName,
                        PcolName = tbcolumn[1],
                        FtbName = fTbName,
                        FcolName = tbcolumn2[1]
                    };
                    ListTableRelt.Add(tr);
                }
                else
                {
                    MessageBox.Show("条件语句" + conditon + "解析有误!");
                }
            }
        }

        /// <summary>
        /// 生成insert语句
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtColumnInfo"></param>
        /// <param name="tbName"></param>
        /// <returns></returns>
        public static string BuildInsertSqlText(DataTable dt,DataTable dtColumnInfo, string tbName)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.AppendFormat("INSERT INTO {0}(", dt.TableName);
            foreach (var column in dt.Columns)
            {
                sqlText.AppendFormat("`{0}`,", column);
            }
            sqlText.Remove(sqlText.Length - 1, 1);
            sqlText.Append(") ");

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sqlText.Append("VALUES(");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string value = "null";
                    if (!string.IsNullOrEmpty(dt.Rows[j][i].ToString()))
                    {
                        value = dt.Rows[j][i].ToString();
                    }
                    sqlText.AppendFormat("'{0}',", value);
                }
                sqlText.Remove(sqlText.Length - 1, 1);
                sqlText.Append("),");
            }
            sqlText.Remove(sqlText.Length - 1, 1);
            sqlText.Append(";");
            return sqlText.ToString();
        }

        public static string BuildValidateSqlText(DataTable dt, DataTable dtColumnInfo)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.AppendFormat("select count(1) from {0} where ", dt.TableName);
            foreach (var column in dt.Columns)
            {
                sqlText.AppendFormat("{0},", column);
            }
            sqlText.Append(" ");

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sqlText.Append("values(");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sqlText.AppendFormat("'{0}',", dt.Rows[j][i]);
                }
                sqlText.Remove(sqlText.Length - 1, 1);
                sqlText.Append("),");
            }
            sqlText.Remove(sqlText.Length - 1, 1);
            return sqlText.ToString();
        }

        public static string BuildUpdateSqlText(DataTable dt, string dataBaseName,string tbName)
        {
            IList<string> rowKeys = DataHelper.GetKey(dt.TableName, dataBaseName);
            StringBuilder sqlText = new StringBuilder(); 
            foreach (DataRow row in dt.Rows)
            {
                sqlText.AppendFormat("UPDATE {0} SET ", tbName);
                foreach (var col in dt.Columns)
                {
                    if (rowKeys.Contains(col.ToString())) continue;
                    sqlText.AppendFormat("`{0}`='{1}',", col, row[col.ToString()]); 
                }
                sqlText.Remove(sqlText.Length - 1,1);
                sqlText.Append(" where 1 = 1 ");
                foreach (var key in rowKeys)
                {
                    sqlText.AppendFormat(" and {0}='{1}'", key, row[key]);
                }
            }
            sqlText.Append(";");
            return sqlText.ToString();
        }

        public static string BuildDeleteSqlText(DataTable dt, string dataBaseName, string tbName)
        {
            IList<string> rowKeys = DataHelper.GetKey(dt.TableName, dataBaseName);
            StringBuilder sqlText = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                sqlText.AppendFormat("DELETE FROM {0}  ", dt.TableName);
                sqlText.Append(" where 1 = 1 ");
                if (rowKeys.Count == 0)
                {
                    foreach (var col in dt.Columns)
                    {
                        if (rowKeys.Contains(col.ToString())) continue;
                        sqlText.AppendFormat(" AND `{0}`='{1}'", col, row[col.ToString()]);
                    }
                }
                else
                {
                    foreach (var key in rowKeys)
                    {
                        sqlText.AppendFormat(" and {0}='{1}'", key, row[key]);
                    }
                }
            }
            sqlText.Append(";");
            return sqlText.ToString();
        }
    }
}
