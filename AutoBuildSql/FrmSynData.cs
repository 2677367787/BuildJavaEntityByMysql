using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoBuildSql.Dto;
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
            Utils.BinderComboBox(cboDbSource, DataHelper.GetDataBases());

        }

        private void cboConnTarger_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlHelper.Conn = cboConnSource.SelectedValue.ToString();
            Utils.BinderComboBox(cboDbTarger, DataHelper.GetDataBases());
        }

        private void btnSynData_Click(object sender, EventArgs e)
        {
            string sourceDb = cboDbSource.SelectedValue.ToString();
            string targerDb = cboDbTarger.SelectedValue.ToString();

            AnalysisData ai = SqlTextHelper.Analysis(txtSqlText.Text,
                cboDbSource.SelectedValue.ToString(), false,null);
            IDictionary<string, IList<string>> sqlList = ai.SqlText;
             
            txtResult.Text += string.Join("\r\n", sqlList["del"].ToArray()).Replace(sourceDb, targerDb);
            txtResult.Text += string.Join("\r\n", sqlList["add"].ToArray()).Replace(sourceDb, targerDb); ;
            try
            {
                MessageBox.Show("" + MySqlHelper.ExecuteNonQuery(txtResult.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
