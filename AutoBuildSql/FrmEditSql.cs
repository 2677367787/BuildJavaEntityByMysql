using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoBuildSql
{
    public partial class FrmEditSql : Form
    {
        public FrmEditSql()
        {
            InitializeComponent();
        }

        private void txtSqlText_TextChanged(object sender, EventArgs e)
        {
            string sqlText = txtSqlText.Text.Replace("\r\n", " ").ToLower();
            sqlText = Regex.Replace(sqlText, "\\s{2,}", " ");
            if (txtSqlText.Text.IndexOf("insert into", StringComparison.Ordinal) != -1)
            {

            }
            int top = 15;    //顶部起点像素位置
            int left = 15;   //左边起点像素位置 

//            foreach (TableExplain columnInfo in list)
//            {
//                string columnName = StringUtils.FormatToolColumn(columnInfo.Column);
//                if (StringUtils.ContainsIgnoreCase("rowState,npcName,Key,indexSn", columnName))
//                    continue;
//
//                string value = dr.Cells[columnName].Value.ToString();
//
//                var width = columnInfo.Width; //控件宽度 
//                var height = columnInfo.Height; //控件高度 
//
//                if (900 - left - 100 < width)
//                {
//                    left = 15;
//                    top += 30;
//                }
//                string labText = columnInfo.Column;
//                if (!string.IsNullOrEmpty(columnInfo.Text))
//                {
//                    labText = columnInfo.Text;
//                }
//                Label tips = new Label
//                {
//                    Text = labText,
//                    Top = top + 4,
//                    Left = left,
//                    Width = 100,
//                    TextAlign = ContentAlignment.TopRight
//                };
//
//                left += tips.Width + 10;
//                //如果填写了列的注释信息,就附加上去
//
//                TextBox tb = new TextBox
//                {
//                    Tag = columnName,
//                    Text = value,
//                    Top = top,
//                    Left = left,
//                    Width = width
//                };
//
//                left += tb.Width + 5;
//                if (height > 21)
//                {
//                    tb.Multiline = true;
//                    tb.Height = height;
//                    top += height + 9;
//                    left = 15;
//                }
//
//                form.Controls.Add(tips);
//                form.Controls.Add(tb);
//            }
//            form.Height = top + 180;
        }
    }
}
