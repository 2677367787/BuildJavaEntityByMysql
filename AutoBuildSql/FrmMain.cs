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
            List<string> list = new List<string>();
            list.Add(textBox1.Text);
            
            AnalysisData ai  = SqlTextHelper.Analysis(txtSqlText.Text,cboDataBase.SelectedValue.ToString(), chkIsOnly.Checked, list);
            IDictionary<string, IList<string>> sqlList = ai.SqlText;

            if (chkAdd.Checked)
            {
                txtResult.Text = string.Join("\r\n", sqlList["add"].ToArray());
                txtResult.Text += "\r\n";
            }
            if (chkDel.Checked)
            {
                txtResult.Text += string.Join("\r\n", sqlList["del"].ToArray());
                txtResult.Text += "\r\n";
            }
            if (chkUpd.Checked)
            {
                txtResult.Text += string.Join("\r\n", sqlList["upd"].ToArray());
                txtResult.Text += "\r\n";
            }

            txtLog.Text = LocalData.Logs.ToString(); 
 
            if (!string.IsNullOrEmpty(LocalData.ErrLogs.ToString()))
            {
                txtLog.Text += LocalData.ErrLogs.ToString();
                MessageBox.Show("生成有误！请查看执行日志最下面的错误信息！");
            }

            //json 字符串
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

        private void cboDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
