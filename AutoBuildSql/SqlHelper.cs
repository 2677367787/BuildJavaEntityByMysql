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

        public static void BuildSqlText(DataTable dt, DataRow[] drs)
        {
            StringBuilder sqlText = new StringBuilder();
            sqlText.Append($"Insert into {dt.TableName}(");
            foreach (var column in dt.Columns)
            {
                sqlText.Append($"{column},");
            }
            sqlText.Append(") ");

            for (int j = 0; j < drs.Length; j++)
            {
                sqlText.Append("values(");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sqlText.Append($"{dt.Rows[j][i]},");
                }
                sqlText.Remove(sqlText.Length - 1, 1);
                sqlText.Append("),");
            }
            sqlText.Remove(sqlText.Length - 1, 1);
        }
    }
}
