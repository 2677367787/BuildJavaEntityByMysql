using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoBuildSql.Dto;
using WeifenLuo.WinFormsUI.Docking;

namespace AutoBuildSql
{
    public partial class FrmMain : DockContent
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cboConnName.DataSource = MySqlHelper.ConnectionAttr;
        }

        private void cboConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlHelper.Conn = cboConnName.SelectedValue.ToString();
            Utils.BinderComboBox(cboDataBase,DataHelper.GetDataBases());
        } 

        private void btnResolve_Click(object sender, EventArgs e)
        {
            AnalysisData ai  = SqlTextHelper.Analysis(txtSqlText.Text,cboDataBase.SelectedValue.ToString(), chkIsOnly.Checked);
            IDictionary<string, IList<string>> sqlList = ai.SqlText;

            if (chkAdd.Checked)
            {
                txtResult.Text = string.Join("\r\n", sqlList["add"].ToArray());
            }
            if (chkDel.Checked)
            {
                txtResult.Text += string.Join("\r\n", sqlList["del"].ToArray());
            }
            if (chkUpd.Checked)
            {
                txtResult.Text += string.Join("\r\n", sqlList["upd"].ToArray());
            }

            txtLog.Text = LocalData.Logs.ToString();
            txtResult.Text += "解析得到的字段：\r\n";

            //json 字符串
            IDictionary<string, string> aliasField = ai.AliasField;
            IDictionary<string, string> fieldAndTable = ai.FieldAndTable;
            txtResult.Text += aliasField.ToString();
            txtResult.Text += fieldAndTable.ToString();
        }

        private void txtSqlText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            JavaMapping jm = new JavaMapping();
            jm.SqlType = "varchar";
            jm.JavaType = "String";
            IList<JavaMapping> list = new List<JavaMapping>();
            list.Add(jm);
            XmlHelper.SaveConfig(list, "JavaMapping.xml");
        }
    }
}
