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
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoBuildSql
{
    public class SqlTextHelper
    {
        private static readonly Dictionary<string, string> DictTbNameRelt = new Dictionary<string, string>();
        private static readonly IList<TableRelt> ListTableRelt = new List<TableRelt>();
        public static Dictionary<string, IList<string>> Analysis(string originalSqlText,string dataBaseName)
        {
            DictTbNameRelt.Clear();
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
                string sqlText = originalSqlText.Replace("\r\n", " ").ToLower();
                sqlText = Regex.Replace(sqlText, "\\s{2,}", " ");
                int formIndex = sqlText.IndexOf(" from ", StringComparison.Ordinal);
                int whereIndex = sqlText.IndexOf(" where ", StringComparison.Ordinal);
                string tableAndRelation = sqlText.Substring(formIndex + 6, whereIndex - formIndex);
                string conditonsStr = sqlText.Substring(whereIndex + 7);

                //要执行查询的SQL语句
                string excutSql = sqlText.Substring(formIndex);

                //去除关键字
                IList<string> list = RemoveKeyWord(tableAndRelation, excutSql);
                
                foreach (var tables in list)
                {
                    var tableNames = tables.Trim().Split(' ');
                    if (tableNames.Length > 1)
                    {
                        DictTbNameRelt.Add(tableNames[1], tableNames[0]);
                    }
                    string tableAbbName = tableNames[0];
                    string tableName = string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);
                    LocalData.Logs.AppendLine("解析后表名：" + tableName);
                    excutSql = excutSql.Replace(tableAbbName, tableName);
                }
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
                    ds.Tables.Add(dt);
                    DataTable dtColmnInfo = DataHelper.GetColumnByTableName(tableFullName);

                    string insertSqlText = BuildInsertSqlText(dt, dtColmnInfo, tableSchemaName);
                    listAdd.Add(insertSqlText);
                    LocalData.Logs.AppendLine("生成插入语句："+ insertSqlText);

                    string updateSqlText = BuildUpdateSqlText(dt, dataBaseName, tableSchemaName);
                    listUpd.Add(updateSqlText);
                    LocalData.Logs.AppendLine("生成更新语句：" + updateSqlText);

                    string deleteSqlText = BuildDeleteSqlText(dt, dataBaseName, tableSchemaName);
                    listDel.Add(deleteSqlText);
                    LocalData.Logs.AppendLine("生成删除语句：" + deleteSqlText);
                }

                DataTable dtColumnInfo = DataHelper.GetAllColumn();
                //遍历所有表,查找每个表的主键,根据主键类型生成新主键，然后修改关联的外键表
                foreach (DataTableCollection dt in ds.Tables)
                {
                    string tbName = dt[0].TableName;
                    IList<string> listKeys = DataHelper.GetKey(tbName, dataBaseName);
                    foreach (var key in listKeys)
                    {
                        DataRow[] drs = dtColumnInfo.Select(
                            string.Format("TABLE_SCHEMA='{0}' AND COLUMN_NAME='{1}' and TABLE_NAME='{2}'", dataBaseName,
                                key, tbName));
                        foreach (var dr in drs)
                        {
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LocalData.Logs.Append("出现异常："+ex.Message);
            }
            return dictSqlText;
        }

        private static IList<string> RemoveKeyWord(string strSql, string excutSql)
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
                var list2 = GetStrList(keyWrod, list);
                list.Clear();
                list = list2;
            }
            return list;
        }

        private static IList<string> GetStrList(string keyWrod, IList<string> list)
        {
            IList<string> list2 = new List<string>();
            foreach (string sqls in list)
            {
                string[] sql = sqls.Split(new[] { keyWrod }, StringSplitOptions.None);
                if (keyWrod == " on ")
                {
                    list2.Add(sql[0]);
                    ResolveCond(sql[1]);
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

        private static void ResolveCond(string conditonsStr)
        {
            string[] conditons = conditonsStr.Split(new[] { " and " }, StringSplitOptions.None);
            foreach (var conditon in conditons)
            {
                string[] cond = conditon.Split('=');
                if (Utils.IsNumeric(cond[0].Trim()) || Utils.IsNumeric(cond[1].Trim())) continue;
                string[] tbcolumn = cond[0].Split('.');
                string[] tbcolumn2 = cond[1].Split('.');
                if (!DictTbNameRelt.ContainsKey(tbcolumn[0]) || !DictTbNameRelt.ContainsKey(tbcolumn2[0]))
                {
                    TableRelt tr = new TableRelt
                    {
                        PtbName = tbcolumn[0],
                        PcolName = tbcolumn[1],
                        FtbName = tbcolumn2[0],
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
