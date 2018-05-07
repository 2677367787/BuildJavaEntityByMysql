/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：LocalData
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/3/12 22:07:38
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBuildSql
{
    public class LocalData
    {
        public static StringBuilder Logs = new StringBuilder();
        public static StringBuilder ErrLogs = new StringBuilder();
    }

    public class TableRelt
    {
        public string PtbName { get; set; }

        public string PcolName { get; set; }

        public string FtbName { get; set; }

        public string FcolName { get; set; }

    }
}
