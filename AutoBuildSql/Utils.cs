/***************************************************** 
** 命名空间：AutoBuildSql
** 文件名称：Utils
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/3/12 22:44:56
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoBuildSql
{
    public class Utils
    {
        /// <summary>
        /// 绑定数据到下拉框
        /// </summary>
        /// <param name="cb">下拉框对象</param>
        /// <param name="list">数据源</param>
        public static void BinderComboBox<T>(ComboBox cb, IList<T> list)
        {
            BindingSource bs = new BindingSource {DataSource = list};
            cb.DataSource = bs; 
        }
    }
}
