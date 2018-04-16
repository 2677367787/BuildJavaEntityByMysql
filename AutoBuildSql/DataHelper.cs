/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：DataHelper
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/2/10 22:13:42
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoBuildSql
{
    public class DataHelper
    {
        public static Dictionary<object, object> GetDataBases()
        {
            string sqlText = "SHOW DATABASES";
            string filterTable = "";
            foreach (var filter in MySqlHelper.FilterDatabases)
            {
                filterTable += "'"+ filter.Name + "',";
            }
            filterTable = string.Format("DATABASE not in({0})", filterTable.TrimEnd(','));
            return MySqlHelper.GetDataSetBySqlText(sqlText)
                .Tables[0].Select(filterTable).ToDictionary(k=>k["DATABASE"] , k=> k["DATABASE"]);
              
        }

        public static DataTable GetAllColumn()
        {
            string sqlText = "SELECT f.`TABLE_SCHEMA`,f.data_type,f.`TABLE_NAME`,f.`COLUMN_NAME`,f.`COLUMN_TYPE`,f.`COLUMN_COMMENT` FROM INFORMATION_SCHEMA.Columns f " +
                             " where table_schema NOT IN('information_schema','mysql','performance_schema','test')";
            return MySqlHelper.GetDataSetBySqlText(sqlText).Tables[0];
        }

        /// <summary>
        /// 查询表每列属性
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable GetColumnByTableName(string tableName)
        {
            string sqlText = "SELECT f.`TABLE_SCHEMA`,f.data_type,f.`TABLE_NAME`,f.`COLUMN_NAME`,f.`COLUMN_TYPE`,f.`COLUMN_COMMENT` FROM INFORMATION_SCHEMA.Columns f " +
                             " where table_schema NOT IN('information_schema','mysql','performance_schema','test') and table_schema='" + tableName + "'";
            return MySqlHelper.GetDataSetBySqlText(sqlText).Tables[0];
        }

        public static IList<string> GetKey(string tbName,string databaseName)
        {
            //            string sqlText = "SELECT t.TABLE_NAME, t.CONSTRAINT_TYPE, c.COLUMN_NAME, c.ORDINAL_POSITION FROM " +
            //                             "INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS t, INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS c " +
            //                             "WHERE t.TABLE_NAME = c.TABLE_NAME AND t.CONSTRAINT_TYPE IN  ('UNIQUE','PRIMARY KEY') AND" +
            //                             " t.CONSTRAINT_SCHEMA = '" + databaseName + "' AND t.TABLE_NAME = '" + tbName + "'" +
            //                             " AND c.`TABLE_SCHEMA` = '" + databaseName + "' ";
            string sqlText = "SELECT DISTINCT T.`COLUMN_NAME` FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE T " +
                "WHERE T.`CONSTRAINT_SCHEMA` = '"+ databaseName + "' AND T.`TABLE_NAME` = '"+ tbName + "'";
            DataTable dt = MySqlHelper.GetDataSetBySqlText(sqlText).Tables[0];
            IList<string> listKey = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listKey.Add(dt.Rows[i]["COLUMN_NAME"].ToString());
            }
            return listKey;
        }

        public static string GetIntKey(string tbName, string colName)
        {
            //            string sqlText = " SELECT t.rownum FROM(SELECT @rownum:=@rownum+1 AS rownum , a.`"+ colName + "`" +
            //                             " FROM(SELECT @rownum:= 0)t, "+ tbName + " a ORDER BY a."+ colName + ")t WHERE t.rownum != t.`" + colName + "`";
            //            DataTable dt = MySqlHelper.GetDataSetBySqlText(sqlText).Tables[0];
            //            if (dt.Rows.Count == 0)
            //                return "2147483647";
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            Random random = new Random(iSeed);
            return random.Next(1500000000, 2147483647).ToString();
        }

        /// <summary>
        /// 使用正则表达式截取{}中的值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetRegexValue(string str)
        {
            MatchCollection m = Regex.Matches(str, @"(?<=\()[^\[\]]+(?=\))");//正则
            return m.Count > 0 ? m[0].Value : "";
        }
    }
}
