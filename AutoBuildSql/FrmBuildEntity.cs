using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AutoBuildSql
{
    public partial class FrmBuildEntity : DockContent
    {
        public FrmBuildEntity()
        {
            InitializeComponent();
        }

        private void txtFiled_TextChanged(object sender, EventArgs e)
        {
            FilterData(txtField.Text);
        }

        private DataTable _dt;
        private DataTable _tableSource;
        private Dictionary<string, ColumnInfo> _dictInfo = new Dictionary<string, ColumnInfo>();
        private void FrmBuildEntity_Load(object sender, EventArgs e)
        { 
            _dt = DataHelper.GetAllColumn();
            dg1.DataSource = _dt;
            _tableSource = _dt.Copy();
            cboConnName.DataSource = MySqlHelper.ConnectionAttr;
        }

        private void cboDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDataBase.SelectedValue != null)
            {
                DataView dv = _tableSource.DefaultView;
                dv.RowFilter = string.Format(" TABLE_SCHEMA='{0}'",cboDataBase.SelectedValue);
                DataTable dataTableDistinct = dv.ToTable(true, "TABLE_NAME");

                cboTableName.ValueMember = "TABLE_NAME";
                cboTableName.DisplayMember = "TABLE_NAME";
                cboTableName.DataSource = dataTableDistinct;
            }
        }

        private void FilterData(string fieldText)
        {
            DataView dv = _dt.DefaultView;
            dv.RowFilter = string.Format(" COLUMN_NAME='{0}' or COLUMN_COMMENT like '%{1}%'", fieldText, fieldText);
            dg1.DataSource = dv;
        }

        private void FilterData(string tableSchema,string tableName)
        {
            DataView dv = _dt.DefaultView;
            dv.RowFilter = string.Format(" table_schema='{0}' and table_name = '{1}'", tableSchema, tableName);
            dg1.DataSource = dv;
        }

        private void dg1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dg1.RowHeadersWidth - 4,
                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dg1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dg1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void cboTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData(cboDataBase.SelectedValue.ToString(), cboTableName.SelectedValue.ToString());
        }

        private void dg1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dataGridViewRow = dg1.CurrentRow;
            if (dataGridViewRow != null)
            {
                SaveField(dataGridViewRow);
            }
        }

        private void checkedListBox_DoubleClick(object sender, EventArgs e)
        {
            _dictInfo.Remove(checkedListBox.SelectedItem.ToString());
            checkedListBox.Items.Remove( checkedListBox.SelectedItem);
            
        }

        private void btnConfirmSel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dg1.SelectedRows)
            {
                SaveField(dataGridViewRow);
            }
        }

        private void SaveField(DataGridViewRow dataGridViewRow)
        {
            string fieldName = dataGridViewRow.Cells["ColumnName"].Value.ToString();
            if (checkedListBox.Items.Contains(fieldName)) return;
            checkedListBox.Items.Add(fieldName);
            for (int j = 0; j < checkedListBox.Items.Count; j++)
                checkedListBox.SetItemChecked(j, true);
            ColumnInfo ci = new ColumnInfo()
            {
                Comment = dataGridViewRow.Cells["columncomment"].Value.ToString(),
                Name = fieldName,
                DataType = dataGridViewRow.Cells["datatype"].Value.ToString(),
                Length = DataHelper.GetRegexValue(dataGridViewRow.Cells["columntype"].Value.ToString())
            };
            _dictInfo.Add(fieldName, ci);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            txtResult.Text = Generate.GenerateSql(_dictInfo, txtEntityName.Text,"");
        }

        private void txtResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        private void btnExportEntity_Click(object sender, EventArgs e)
        {
            txtResult.Text = Generate.GenerateSql(_dictInfo, txtEntityName.Text,"Export");
        }

        private void cboConnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlHelper.Conn = cboConnName.SelectedValue.ToString(); 
            Utils.BinderComboBox(cboDataBase, DataHelper.GetDataBases());
        }
    }
}
