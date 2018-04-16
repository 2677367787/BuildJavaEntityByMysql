using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
            Dictionary<string, IList<string>> sqlList = SqlTextHelper.Analysis(txtSqlText.Text,
                cboDataBase.SelectedValue.ToString(), chkIsOnly.Checked);

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
        }

        private void txtSqlText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }
    }
}
