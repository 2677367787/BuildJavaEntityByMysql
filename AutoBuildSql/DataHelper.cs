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
        public static DataTable GetDataBases()
        {
            string sqlText = "SHOW DATABASES";
            return MySqlHelper.GetDataSetBySqlText(sqlText).Tables[0];
        }

        public static DataTable GetAllColumn()
        {
            string sqlText = "SELECT f.`TABLE_SCHEMA`,f.data_type,f.`TABLE_NAME`,f.`COLUMN_NAME`,f.`COLUMN_TYPE`,f.`COLUMN_COMMENT` FROM INFORMATION_SCHEMA.Columns f " +
                             " where table_schema NOT IN('information_schema','mysql','performance_schema','test')";
            return MySqlHelper.GetDataSetBySqlText(sqlText).Tables[0];
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
