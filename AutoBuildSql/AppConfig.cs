/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：AppConfig
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/3/11 15:16:45
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AutoBuildSql
{
    [Serializable]
    public class AppConfig
    {
        [XmlElement]
        public List<ConnectionAttr> ConnectionAttrs { get; set; }
        [XmlElement]
        public List<Filter> FilterDatabases { get; set; }
        [XmlElement]
        public List<Filter> FilterTables { get; set; }
    }
    [Serializable]
    public class ConnectionAttr
    {
        public string Name { get; set; }
        public string ConnectionStr { get; set; }
    }
    [Serializable]
    public class Filter
    {
        public string Name { get; set; }
    }
}
