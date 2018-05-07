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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Xsl;
using AutoBuildSql.Dto;

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
        public static AnalysisData Analysis(string originalSqlText,string dataBaseName,bool isOnly,List<string> filterField)
        {
            DictTbNameRelt.Clear();
            ListTableRelt.Clear();
            LocalData.Logs.Clear();
            IDictionary<string, string> conditionValue = new Dictionary<string, string>();
            IDictionary<string, IList<string>> dictSqlText = new Dictionary<string, IList<string>>();
            IList<string> listAdd = new List<string>();
            IList<string> listDel = new List<string>();
            IList<string> listUpd = new List<string>();
            dictSqlText.Add("add", listAdd);
            dictSqlText.Add("del", listDel);
            dictSqlText.Add("upd", listUpd);

            AnalysisData ai = new AnalysisData {SqlText = dictSqlText};
            try
            {
                int orgFormIndex = originalSqlText.IndexOf("\r\nFROM\r\n", StringComparison.Ordinal);
                string sqlText = originalSqlText.Replace("\r\n", " ").Replace("`","");
                sqlText = Regex.Replace(sqlText, "\\s{2,}", " ");
                int formIndex = sqlText.IndexOf(" FROM ", StringComparison.Ordinal);
                int whereIndex = sqlText.IndexOf(" WHERE ", StringComparison.Ordinal);

                #region 字段截取
                string fieldStr = originalSqlText.Substring(6, orgFormIndex - 6);
                //fieldStr = fieldStr.Substring(6, fieldStr.Length - 6);
                Regex r = new Regex("--.*?\\r\\n", RegexOptions.IgnoreCase);
                fieldStr = r.Replace(fieldStr, "");
                fieldStr = fieldStr.Replace("\r\n", " ").Replace("`", ""); 
                #endregion

                string tableAndRelation;
                if (whereIndex == -1) { 
                    whereIndex = sqlText.Length;
                    tableAndRelation = sqlText.Substring(formIndex + 6);
                }
                else
                {
                    tableAndRelation = sqlText.Substring(formIndex + 6, whereIndex - formIndex-7);
                    whereIndex = whereIndex + 7;
                }
                 
                string conditonsStr = sqlText.Substring(whereIndex); 

                //要执行查询的SQL语句
                string excutSql = sqlText.Substring(formIndex);

                //存储条件语句 A.X = B.X 形式
                IList<string> condList = new List<string>();
                //去除关键字
                IList<string> list = RemoveKeyWord(tableAndRelation, condList);
                IList<string> listTab = new List<string>(); 
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
                    string tableName = string.Format("`{0}`.`{1}`", dataBaseName, tableNames[0]);
                    
                    LocalData.Logs.AppendLine("解析后表名：" + tableName);
                    listTab.Add(tableName);
                }
                ai.Tables = listTab;
                #region 解析字段

                IDictionary<string,string> dictField = new Dictionary<string, string>();
                IDictionary<string, string> dictAliasField = new Dictionary<string, string>();
                string[] fields = fieldStr.Split(',');
                foreach (var field in fields)
                {
                    string tempField = field.Trim();
                    string[] asSplit = tempField.Split(new[] { " AS " }, StringSplitOptions.None);
                    if (asSplit.Length != 2)
                    {
                        asSplit = tempField.Split(' ');
                    }

                    string tabAndField = tempField;
                    if (asSplit.Length == 2)
                    {
                        tabAndField = asSplit[0];
                    }

                    string[] tabAndFields = tabAndField.Split('.');
                    if (DictTbNameRelt.ContainsKey(tabAndFields[0]))
                    {
                        dictField.Add(tabAndFields[1], DictTbNameRelt[tabAndFields[0]]);
                        LocalData.Logs.AppendLine("解析得到字段---- " + tabAndFields[1]);
                        if (asSplit.Length == 2)
                        {
                            dictAliasField.Add(asSplit[1], DictTbNameRelt[tabAndFields[0]]);
                            LocalData.Logs.AppendLine("别名---- " + asSplit[1]);
                        }
                    }
                    else
                    {
                        LocalData.Logs.AppendLine("字段[ " + asSplit[0] + " ]解析错误");
                    }
                }
                ai.FieldAndTable = dictField;
                ai.AliasField = dictAliasField;

                #endregion
                foreach (var con in condList)
                {
                    ResolveCond(con);
                }
                ResolveCond(conditonsStr);

                Dictionary<string, string> dictTabs = new Dictionary<string, string>();
                DataSet ds = new DataSet();
                foreach (var tables in list)
                {
                    var tableNames = tables.Trim().Split(' ');
                    string tableAbbName = tableNames[0];
                    string tableName = tableAbbName;//string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);

                    if (dictTabs.ContainsKey(tableAbbName))
                    {
                        continue; 
                    }
                    dictTabs.Add(tableAbbName, tableAbbName);
                    if (MySqlHelper.FilterTables.Where(t => t.Name == tableAbbName).ToList().Any())
                    {
                        continue;
                    } 

                    if (tableNames.Length > 1)
                    {
                        tableAbbName = tableNames[1];
                    }
                    

                    //查询数据要去重
                    string sql = string.Format("select DISTINCT {0}.* {1}", tableAbbName, excutSql);
                    LocalData.Logs.AppendLine("执行SQL：" + sql);
                    DataTable dt = MySqlHelper.GetDataSetBySqlText(sql).Tables[0];
                    LocalData.Logs.AppendLine("SQL执行结果: " + dt.Rows.Count);
                    dt.TableName = tableName;
                    ds.Tables.Add(dt.Copy());
                }

                if (isOnly)
                {
//                    for (int i = 0; i < filterField.Count; i++)
//                    {
//                        List<TableRelt> ftbList = ListTableRelt.Where(t => t.FcolName == filterField[i]).ToList();
//                        if (ftbList.Count > 0)
//                        {
//                            filterField.Remove(filterField[i]);
//                        }
//                        //查找此ID作为外键的表条件在右
//                        List<TableRelt> ptbList = ListTableRelt.Where(t => t.PcolName == filterField[i]).ToList();
//                        if (ptbList.Count > 0)
//                        {
//                            filterField.Remove(filterField[i]);
//                        }
//                    }
                    DataTable dtColumnInfo = DataHelper.GetAllColumn();
                    Dictionary<string, Dictionary<string, string>> tbAndKeyCol =
                        new Dictionary<string, Dictionary<string, string>>();
                    //遍历所有表,查找每个表的主键,根据主键类型生成新主键，然后修改关联的外键表
                    foreach (DataTable dt in ds.Tables)
                    {
                        int i = 0;
                        string tbName = dt.TableName;
                        Dictionary<string, string> keyCol = new Dictionary<string, string>();

                        //查找表中所有主键
                        IList<string> listKeys = DataHelper.GetKey(tbName, dataBaseName);
                        tbAndKeyCol.Add(tbName, keyCol);
                        foreach (DataRow dr in dt.Rows)
                        {
                            foreach (var field in filterField)
                            {
                                if (!listKeys.Contains(field) && dt.Columns.Contains(field))
                                {
                                    string original = dr[field].ToString();
                                    string uniqueStr = GetUniqueStr(dtColumnInfo, dataBaseName, field, tbName, original);
                                    dr[field] = uniqueStr;
                                    if ("2016-02-29" == uniqueStr)
                                    {
                                        DateTime startTime =
                                            TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2016, 2, 29)); // 当地时区
                                        double timeStamp = (DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
                                        conditionValue.Add(field, timeStamp.ToString(CultureInfo.InvariantCulture));
                                    }
                                    else
                                    {
                                        conditionValue.Add(field, uniqueStr);
                                    }

                                    MoidfyRelation(ds, tbName, field, original, uniqueStr);
                                }
                            }
                            foreach (var key in listKeys)
                            {
                                if (i == 0)
                                {
                                    keyCol.Add(key, key);
                                }
                                
                                string original = dr[key].ToString();
                                string id = GetUniqueStr(dtColumnInfo, dataBaseName, key, tbName, original);
                                dr[key] = id;

                                MoidfyRelation(ds, tbName, key, original, id);
                            }
                            i++;
                        }
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
            ai.ConditionValue = conditionValue;
            return ai;
        }

        private static void MoidfyRelation(DataSet ds,string tbName,string key,string original,string uniqueStr)
        {
            //查找和此字段关联的表  条件在左 ,同步更改
            List<TableRelt> ftbList =
                ListTableRelt.Where(t => t.FtbName == tbName && t.FcolName == key).ToList();
            //循环所有关联列赋值
            foreach (TableRelt ftb in ftbList)
            {
                foreach (
                    DataRow fdr in
                        ds.Tables[ftb.PtbName].Select("" + ftb.PcolName + "='" + original + "'"))
                {
                    fdr[ftb.PcolName] = uniqueStr;
                }
            }

            //查找和此字段关联的表  条件在右 ,同步更改
            List<TableRelt> ptbList =
                ListTableRelt.Where(t => t.PtbName == tbName && t.PcolName == key).ToList();
            //循环所有关联列赋值
            foreach (TableRelt ptb in ptbList)
            {
                foreach (
                    DataRow fdr in
                        ds.Tables[ptb.FtbName].Select("" + ptb.FcolName + "='" + original + "'"))
                {
                    fdr[ptb.FcolName] = uniqueStr;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtColumnInfo">所有解析出的表字段信息</param>
        /// <param name="dataBaseName">数据库名称</param>
        /// <param name="columnName">列名称</param>
        /// <param name="tbName"></param>
        /// <returns>0 dataType 1 colLength</returns>
        private static string[] GetTypeAndLength(DataTable dtColumnInfo,string dataBaseName,string columnName,string tbName)
        {
            //查找所有主键
            DataRow[] colDr = dtColumnInfo.Select(
                string.Format("TABLE_SCHEMA='{0}' AND COLUMN_NAME='{1}' and TABLE_NAME='{2}'",
                    dataBaseName, columnName, tbName));
            if (colDr.Length == 0)
            {
                LocalData.ErrLogs.AppendLine("没有在数据库"+ dataBaseName + "." + tbName + "中找到列：" + columnName);
                return new[] { "", "" };
            }
            string dataType = colDr[0]["data_type"].ToString().ToLower();
            string colLength = colDr[0]["COLUMN_TYPE"].ToString().ToLower();
            return new[] {dataType, colLength};
        }

        private static string GetUniqueStr(DataTable dtColumnInfo, string dataBaseName, string key, string tbName,string original)
        {
            string id;
            string[] typeLenght = GetTypeAndLength(dtColumnInfo, dataBaseName, key, tbName);
            string dataType = typeLenght[0];
            string colLength = typeLenght[1];
            //根据主键类型,生成新的唯一数据
            switch (dataType)
            {
                case "int":
                    id = DataHelper.GetIntKey(tbName, key);
                    break;
                case "varchar":
                    string unCode = original;
                    if (unCode.IndexOf("-", StringComparison.Ordinal) != -1 && unCode.Length >= 20)
                    {
                        id = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        int dataLength = int.Parse(DataHelper.GetRegexValue(colLength));
                        int valueLenght = unCode.Length;
                        if (valueLenght + 4 > dataLength)
                        {
                            id = "TEST" +
                                 Utils.GetRandomString(dataLength - (valueLenght + 4 - dataLength));
                        }
                        else
                        {
                            id = "TEST" + unCode;
                        }
                    }
                    break;
                case "date":
                case "datetime":
                    id = "2016-02-29";
                    break;
                default:
                    id = original;
                    break;
            }
            return id;
        }

        /// <summary>
        /// 去除数据库关键字
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="condList"></param>
        /// <returns></returns>
        private static IList<string> RemoveKeyWord(string strSql,IList<string> condList)
        {
            IList<string> list = new List<string>();
            list.Add(strSql);
            string[] keyWrods =
            {
                "LEFT OUTER JOIN",
                "RIGHT OUTER JOIN",
                "INNER OUTER JOIN",
                "INNER JOIN",
                "LEFT JOIN",
                "RIGHT JOIN",
                " JOIN ",
                " ON ",
                " WHERE "
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
                //条件
                if (keyWrod == " ON ")
                {
                    list2.Add(sql[0]);
                    if (sql.Length == 2)
                    {
                        condList.Add(sql[1]);
                    }
                }
                else
                {
                    //
                    foreach (string s in sql)
                    {
                        //去掉重复表
                        if (!list2.Contains(s))
                        {
                            list2.Add(s);
                        }
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
            string[] conditons = conditonsStr.Split(new[] { " AND " }, StringSplitOptions.None);
            foreach (var conditon in conditons)
            {
                if(string.IsNullOrEmpty(conditon))
                    continue;
                string[] cond = conditon.Split(new[] { "=" }, StringSplitOptions.None);
                if (cond.Length < 2)
                {
                    LocalData.Logs.AppendLine("条件语句" + conditon + "解析有误!");
                    continue;
                }
                
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
                    LocalData.Logs.AppendLine("条件语句" + conditon + "解析有误!");
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
                if (j == 0)
                {
                    sqlText.Append("VALUES");
                }
                sqlText.Append("(");
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
                sqlText.Append(";");
            }
            
            return sqlText.ToString();
        }
    }
}
