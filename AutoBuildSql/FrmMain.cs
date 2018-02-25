using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            sqlText = Regex.Replace(sqlText, "\\s{2,}", " ");
            int formIndex = sqlText.IndexOf(" from ", StringComparison.Ordinal);
            int whereIndex = sqlText.IndexOf(" where ", StringComparison.Ordinal);
            string tableAndRelation = sqlText.Substring(formIndex+6, whereIndex- formIndex);
            string conditon = sqlText.Substring(whereIndex+7);
            IList<string> list = new List<string>();

            string excutSql = sqlText.Substring(formIndex);

             RemoveKeyWord(tableAndRelation, excutSql);

            
        }

        private void RemoveKeyWord(string strSql,string excutSql)
        {
            IList<string> list = new List<string>();
            list.Add(strSql);
            string[] keyWrods =
            {
                "left outer join",
                "right outer join",
                "inner outer join",
                "inner join",
                "left join",
                "right join",
                " join ",
                " on "
            };

            foreach (string keyWrod in keyWrods)
            {
                var list2 = GetStrList(keyWrod, list);
                list.Clear();
                list = list2;
            }

            foreach (var tables in list)
            {
                var tableNames = tables.Trim().Split(' ');
                string tableAbbName = tableNames[0];
                string tableFullName = tableNames[0];
                if (tableNames.Length > 1)
                {
                    tableAbbName = tableNames[1];
                }

                string sql = string.Format("select {0}.* {1}", tableAbbName, excutSql);
                DataTable dt = MySqlHelper.GetDataSetBySqlText(sql).Tables[0];
                dt.TableName = tableFullName;
                DataTable dtColmnInfo = DataHelper.GetColumnByTableName(tableFullName);
                SqlHelper.BuildSqlText(dt, dtColmnInfo); 
            }
        }

        private IList<string> GetStrList(string keyWrod, IList<string> list)
        {
            IList<string> list2 = new List<string>();
            foreach (string sqls in list)
            {
                string[] sql = sqls.Split(new[] { keyWrod }, StringSplitOptions.None);
                if (keyWrod == " on ")
                {
                    list2.Add(sql[0]);
                }
                else
                {
                    foreach (string s in sql)
                    {
                        list2.Add(s);
                    }
                }
            }
            return list2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }
    }
}
