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
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoBuildSql
{
    public class SqlTextHelper
    {
        public static string Analysis(string originalSqlText,string dataBaseName)
        {
            StringBuilder logs = new StringBuilder();
            try
            {
                string sqlText = originalSqlText.Replace("\r\n", " ").ToLower();
                sqlText = Regex.Replace(sqlText, "\\s{2,}", " ");
                int formIndex = sqlText.IndexOf(" from ", StringComparison.Ordinal);
                int whereIndex = sqlText.IndexOf(" where ", StringComparison.Ordinal);
                string tableAndRelation = sqlText.Substring(formIndex + 6, whereIndex - formIndex);
                string conditon = sqlText.Substring(whereIndex + 7);

                string excutSql = sqlText.Substring(formIndex);

                IList<string> list = RemoveKeyWord(tableAndRelation, excutSql);

                foreach (var tables in list)
                {
                    var tableNames = tables.Trim().Split(' ');
                    string tableAbbName = tableNames[0];
                    string tableName = string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);
                    logs.AppendLine("解析后表名：" + tableName);
                    excutSql = excutSql.Replace(tableAbbName, tableName);
                }

                foreach (var tables in list)
                {
                    var tableNames = tables.Trim().Split(' ');
                    string tableAbbName = tableNames[0];
                    string tableName = string.Format("`{0}`.`{1}`", dataBaseName, tableAbbName);

                    string tableFullName = tableNames[0];
                    if (tableNames.Length > 1)
                    {
                        tableAbbName = tableNames[1];
                    }

                    string sql = string.Format("select DISTINCT {0}.* {1}", tableAbbName, excutSql);
                    logs.AppendLine("执行SQL：" + sql);
                    DataTable dt = MySqlHelper.GetDataSetBySqlText(sql).Tables[0];
                    dt.TableName = tableName;
                     
                    DataTable dtColmnInfo = DataHelper.GetColumnByTableName(tableFullName);
                    string insertSqlText = BuildInsertSqlText(dt, dtColmnInfo);
                    logs.AppendLine("生成插入语句："+ insertSqlText);
                    string updateSqlText = BuildUpdateSqlText(dt, dataBaseName);
                    logs.AppendLine("生成更新语句：" + updateSqlText);
                    string deleteSqlText = BuildDeleteSqlText(dt, dataBaseName);
                    logs.AppendLine("生成删除语句：" + deleteSqlText);
                }
            }
            catch (Exception ex)
            { 
                logs.Append("出现异常："+ex.Message);
            }
            return logs.ToString();
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
        /// 生成insert语句
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtColumnInfo"></param>
        /// <returns></returns>
        public static string BuildInsertSqlText(DataTable dt,DataTable dtColumnInfo)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.AppendFormat("Insert into {0}(", dt.TableName);
            foreach (var column in dt.Columns)
            {
                sqlText.AppendFormat("`{0}`,", column);
            }
            sqlText.Remove(sqlText.Length - 1, 1);
            sqlText.Append(") ");

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                sqlText.Append("values(");
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

        public static string BuildUpdateSqlText(DataTable dt, string dataBaseName)
        {
            IList<string> rowKeys = DataHelper.GetKey(dt.TableName, dataBaseName);
            StringBuilder sqlText = new StringBuilder(); 
            foreach (DataRow row in dt.Rows)
            {
                sqlText.AppendFormat("UPDATE {0} SET ", dt.TableName);
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
            
            return sqlText.ToString();
        }

        public static string BuildDeleteSqlText(DataTable dt, string dataBaseName)
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

            return sqlText.ToString();
        }
    }
}
