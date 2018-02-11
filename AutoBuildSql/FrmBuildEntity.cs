using System;
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
    public partial class FrmBuildEntity : Form
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
//            DataGridViewCheckBoxColumn chkCbo = new DataGridViewCheckBoxColumn();
//            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
//            chkCbo.HeaderCell = cbHeader;
//            chkCbo.Width = 40;
//            dg1.Columns.Insert(0,chkCbo);
//            cbHeader.OnCheckBoxClicked += cbHeader_OnCheckBoxClicked;
            _dt = DataHelper.GetAllColumn();
            dg1.DataSource = _dt;
            _tableSource = _dt.Copy();
            cboDataBase.ValueMember = "database";
            cboDataBase.DisplayMember = "database";
            cboDataBase.DataSource = DataHelper.GetDataBases();
        }

        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            dg1.EndEdit();
            dg1.Rows.OfType<DataGridViewRow>().ToList().ForEach(t => t.Cells[0].Value = state);
        }

        private void cboDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "1 = 1";
            if (cboDataBase.SelectedValue != null)
            {
                DataView dv = _tableSource.DefaultView;
                dv.RowFilter = $" TABLE_SCHEMA='{cboDataBase.SelectedValue}'";
                DataTable dataTableDistinct = dv.ToTable(true, "TABLE_NAME");

                cboTableName.ValueMember = "TABLE_NAME";
                cboTableName.DisplayMember = "TABLE_NAME";
                cboTableName.DataSource = dataTableDistinct;
            }
        }

        private void FilterData(string fieldText)
        {
            DataView dv = _dt.DefaultView;
            dv.RowFilter = $" COLUMN_NAME='{fieldText}' or COLUMN_COMMENT like '%{fieldText}%'";
            dg1.DataSource = dv;
        }

        private void FilterData(string tableSchema,string tableName)
        {
            DataView dv = _dt.DefaultView;
            dv.RowFilter = $" table_schema='{tableSchema}' and table_name = '{tableName}'";
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

        }
    }
}
