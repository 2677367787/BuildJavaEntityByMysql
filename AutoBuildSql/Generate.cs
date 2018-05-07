/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：Generate
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/2/11 23:09:47
** 修改记录： 
*****************************************************/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoBuildSql.Dto;

namespace AutoBuildSql
{
    public class Generate
    {　
        public static string GenerateSql(Dictionary<string,ColumnInfo> fields,string entityName,string type)
        {
            Dictionary<string, string> dictTypeMapping;
            if (File.Exists("JavaMapping.xml"))
            {
                IList<JavaMapping> javaMapping = XmlHelper.ReadConfig<JavaMapping>("JavaMapping.xml");
                dictTypeMapping = javaMapping.ToDictionary(t => t.SqlType, t => t.JavaType);
            }
            else
            {
                dictTypeMapping = new Dictionary<string, string>
                {
                    {"varchar", "String"},
                    {"char", "String"},
                    {"blob", "byte[]"},
                    {"text", "String"},
                    {"integer", "Long"},
                    {"tinyint", "Integer"},
                    {"smallint", "Integer"},
                    {"mediumint", "Integer"},
                    {"bit", "Boolean"},
                    {"bigint", "BigInteger"},
                    {"float", "Float"},
                    {"double", "Double"},
                    {"decimal", "BigDecimal"},
                    {"boolean", "Integer"},
                    {"id", "Long"},
                    {"date", "Date"},
                    {"time", "Time"},
                    {"datetime", "Timestamp"},
                    {"timestamp", "Timestamp"},
                    {"year", "Date"},
                    {"int", "Integer"}
                };
            }
            StringBuilder entity = new StringBuilder();
            entity.Append("");
            string wrap = "\r\n";
            string tabs = "\t";

            entity.Append($"package xxxx;{wrap}");
            entity.Append($"import java.io.Serializable;{wrap}");
            entity.Append($"public class {entityName} implements Serializable {wrap}{{");

            int i = 1;
            foreach (var field in fields)
            {
                if (!dictTypeMapping.ContainsKey(field.Value.DataType))
                {
                    entity.Append(field.Value.DataType + "没有配置映射，请打开javamapping.xml文件配置映射");
                }
                string dataType = dictTypeMapping[field.Value.DataType];
                string fieldName = GetField(field.Value.Name);
                string comment = field.Value.Comment;
                entity.Append($"{tabs}/*{wrap}");
                entity.Append($"{tabs} *{comment}{wrap}");
                entity.Append($"{tabs} */{wrap}");

                if (type == "Export")
                {
                    entity.AppendFormat($"{tabs}@Excel(name='{comment}',orderNum=\"{i}\",isImportField='{fieldName}'){wrap}");
                }
                entity.AppendFormat($"{tabs}private {dataType} {fieldName};{wrap}");
                
                entity.Append($"{tabs}public void {GetSetMethodName(fieldName)}({dataType} {fieldName}){{{wrap}" );
                entity.Append($"{tabs}{tabs}this.{fieldName}={fieldName};{wrap}");
                entity.Append($"{tabs}}}{wrap}");

                entity.Append($"{tabs}public {dataType} {GetGetMethodName(fieldName)}(){{{wrap}");
                entity.Append($"{tabs}{tabs}return {fieldName};{wrap}");
                entity.Append($"{tabs}}}{wrap}");
                i++;
            }
            entity.Append("}");
            return entity.ToString();
        }

        private static string GetSetMethodName(string field)
        {
            return "set" + GetMethodName(field); 
        }

        private static string GetGetMethodName(string field)
        {
            return "get" + GetMethodName(field);
        }

        private static string GetMethodName(string field)
        {
            string fieldName = string.Empty;
            string[] str = field.Split('_');
            return str.Aggregate(fieldName, (current, t) => current + (t.Substring(0, 1).ToUpper() + t.Substring(1)));
        }

        private static string GetField(string field)
        {
            string fieldName = string.Empty;
            string[] str = field.Split('_');
            for (int i = 1; i < str.Length; i++)
            {
                fieldName += str[i].Substring(0, 1).ToUpper() + str[i].Substring(1);
            }
            return str[0]+fieldName;
        }
    }
}
