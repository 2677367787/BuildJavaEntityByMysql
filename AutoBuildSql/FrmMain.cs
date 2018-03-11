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
            txtResult.Text = SqlTextHelper.Analysis(txtSqlText.Text, cboDataBase.SelectedValue.ToString());
        } 

        private void button1_Click(object sender, EventArgs e)
        {
             
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            cboConnName.DataSource = MySqlHelper.ConnectionAttr;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void cboConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlHelper.Conn = cboConnName.SelectedValue.ToString();
            cboDataBase.DataSource = DataHelper.GetDataBases();
        }
    }
}
