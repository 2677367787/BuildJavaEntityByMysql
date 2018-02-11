/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：Generate
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/2/11 23:09:47
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBuildSql
{
    public class Generate
    {　

        public string GenerateSql(Dictionary<string,ColumnInfo> fields)
        {
            Dictionary<string, string> dictTypeMapping = new Dictionary<string, string>();
            dictTypeMapping.Add("varchar", "String");
            dictTypeMapping.Add("char", "String");
            dictTypeMapping.Add("blob", "byte[]");
            dictTypeMapping.Add("text", "String");
            dictTypeMapping.Add("integer", "Long");
            dictTypeMapping.Add("tinyint", "Integer");
            dictTypeMapping.Add("smallint", "Integer");
            dictTypeMapping.Add("mediumint", "Integer");
            dictTypeMapping.Add("bit", "Boolean");
            dictTypeMapping.Add("bigint", "BigInteger");
            dictTypeMapping.Add("float", "Float");
            dictTypeMapping.Add("double", "Double");
            dictTypeMapping.Add("decimal", "BigDecimal");
            dictTypeMapping.Add("boolean", "Integer");
            dictTypeMapping.Add("id", "Long");
            dictTypeMapping.Add("date", "Date");
            dictTypeMapping.Add("time", "Time");
            dictTypeMapping.Add("datetime", "Timestamp");
            dictTypeMapping.Add("timestamp", "Timestamp");
            dictTypeMapping.Add("year", "Date");
            StringBuilder entity = new StringBuilder();
            entity.Append("");
            string wrap = "\r\n";
            foreach (var field in fields)
            {
                entity.Append($"/*{wrap}");
                entity.Append($" *{wrap}");
                entity.Append($" */{wrap}");
                entity.AppendFormat($"private {dictTypeMapping[field.Value.DataType]} {field.Value.Name};{wrap}");

                entity.Append(getSetMethodName(field.Value.Name));

            }
            return "";
        }

        private string getSetMethodName(string field)
        {
            string fieldName = string.Empty;
            string[] str = field.Split('_');
            for (int i = 0; i < str.Length; i++)
            {
                fieldName += str[i].Substring(0, 1).ToUpper() + str[i].Substring(1);
            }
            return "set" + fieldName;
        }
    }
}
