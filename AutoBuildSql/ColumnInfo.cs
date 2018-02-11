/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：ColumnInfo
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/2/11 22:35:48
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBuildSql
{
    public class ColumnInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        public string DataType { get; set; }
        public string Length { get; set; } 
        public string Comment { get; set; }
    }
}
