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
using System.Data;
using System.Text;

namespace AutoBuildSql
{
    public class SqlHelper
    {
        public static void Resolve(string sqlText)
        {
            int formIndex = sqlText.IndexOf(" from ", StringComparison.Ordinal) + 6;
            int whereIndex = sqlText.IndexOf(" where ", StringComparison.Ordinal);
            string tableAndRelation = sqlText.Substring(formIndex, whereIndex);
            string conditon = sqlText.Substring(whereIndex + 7);

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
                sqlText.AppendFormat("{0},", column);
            }
            sqlText.Append(") ");

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
    }
}
