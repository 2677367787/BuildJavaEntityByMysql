using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AutoBuildSql
{
    public partial class FrmSynData : DockContent
    {
        public FrmSynData()
        {
            InitializeComponent();
        }

        private void FrmSynData_Load(object sender, EventArgs e)
        {
            Utils.BinderComboBox(cboConnSource, MySqlHelper.ConnectionAttr);
            Utils.BinderComboBox(cboConnTarger, MySqlHelper.ConnectionAttr);
        }

        private void cboConnSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlHelper.Conn = cboConnSource.SelectedValue.ToString();
            cboDbSource.DataSource = DataHelper.GetDataBases();
        }

        private void cboConnTarger_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlHelper.Conn = cboConnSource.SelectedValue.ToString();
            cboDbTarger.DataSource = DataHelper.GetDataBases();
        }

        private void btnSynData_Click(object sender, EventArgs e)
        {
            Dictionary<string, IList<string>> sqlList = SqlTextHelper.Analysis(txtSqlText.Text,
                cboDbSource.SelectedValue.ToString());
            txtResult.Text += " Begin ";
            txtResult.Text += string.Join("\r\n", sqlList["del"].ToArray());
            txtResult.Text += string.Join("\r\n", sqlList["add"].ToArray());
            txtResult.Text += " End; ";

            MessageBox.Show(""+MySqlHelper.ExecuteNonQuery(txtResult.Text));
        }
    }
}
