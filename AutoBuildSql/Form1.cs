using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBuildSql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tsmBuildData_Click(object sender, EventArgs e)
        {
            string sqlText = txtSqlText.Text.Replace("\r\n"," ").ToLower();
            int formIndex = sqlText.IndexOf(" from ", StringComparison.Ordinal)+6;
            int whereIndex = sqlText.IndexOf(" where ", StringComparison.Ordinal);
            string tableAndRelation = sqlText.Substring(formIndex, whereIndex- formIndex);
            string conditon = sqlText.Substring(whereIndex+7);
            IList<string> list = new List<string>();

            //tableAndRelation = RemoveKeyWord(tableAndRelation);
        }

        private string RemoveKeyWord(string str, IList<string> list)
        {
            string[] keyWrods =
            {
                "left outer join",
                "right outer join",
                "inner outer join",
                "inner join",
                "left join",
                "right join",
                " join "
            };
            foreach (var keyWrod in keyWrods)
            {
                string[] strs = keyWrod.Split(new[]{keyWrod}, StringSplitOptions.None);
            }
            return keyWrods.Aggregate(str, (current, keyWord) => current.Replace(keyWord, " "));
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }
    }
}
