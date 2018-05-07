/***************************************************** 
** 命名空间：AutoBuildSql.Dto
** 文件名称：AnalysisData
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/4/23 22:28:19
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoBuildSql.Dto
{
    public class AnalysisData
    {
        /// <summary>
        /// 增删改语句
        /// </summary>
        public IDictionary<string, IList<string>> SqlText { get; set; }

        /// <summary>
        /// 字段名为Key,表名为值
        /// </summary>
        public IDictionary<string, string> FieldAndTable { get; set; }

        /// <summary>
        /// 别名为key,表为值
        /// </summary>
        public IDictionary<string, string> AliasField { get; set; }

        /// <summary>
        /// 解析后的全表名称
        /// </summary>
        public IList<string> Tables { get; set; }

        /// <summary>
        /// 条件对应的指
        /// </summary>
        public IDictionary<string, string> ConditionValue { get; set; }
    }
}
