/***************************************************** 
** 命名空间：AutoBuildSql.Json
** 文件名称：UnixDateTimeConverter
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/7/5 22:25:20
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AutoBuildSql.Json
{
    class UnixDateTimeConverter: DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(String.Format("日期格式错误,got {0}.", reader.TokenType));
            }
            var ticks = (long)reader.Value;
            var date = new DateTime(1970, 1, 1);
            date = date.AddSeconds(ticks);
            return date;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long ticks;
            if (value is DateTime)
            {
                var epoc = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                var delta = ((DateTime)value) - epoc;
                if (delta.TotalSeconds < 0)
                {
                    throw new ArgumentOutOfRangeException("时间格式错误.1");
                }
                ticks = (long)delta.TotalMilliseconds;
            }
            else
            {
                throw new Exception("时间格式错误.2");
            }
            writer.WriteValue(ticks);
        }
    }
}
