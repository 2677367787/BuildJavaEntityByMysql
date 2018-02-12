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
    public partial class FrmMain : Form
    {
        public FrmMain()
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

            tableAndRelation = RemoveKeyWord(tableAndRelation);
        }

        private string RemoveKeyWord(string strSql)
        {
            IList<string> list = new List<string>();
            IList<string> list2 = new List<string>();
            list.Add(strSql);
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

            foreach (string t in keyWrods)
            {
                foreach (string sqls in list)
                {
                    string[] sql = sqls.Split(new[] { t }, StringSplitOptions.None);
                    foreach (string s in sql)
                    {
                        list2.Add(s);
                    }
                }
            } 
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }
    }
}
