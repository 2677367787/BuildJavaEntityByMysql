namespace AutoBuildSql
{
    partial class FrmBuildEntity
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboDataBase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtField = new System.Windows.Forms.TextBox();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.dg1 = new System.Windows.Forms.DataGridView();
            this.TableSchema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTableName = new System.Windows.Forms.ComboBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirmSel = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnExportEntity = new System.Windows.Forms.Button();
            this.txtEntityName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboConnName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDataBase
            // 
            this.cboDataBase.DisplayMember = "database";
            this.cboDataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBase.FormattingEnabled = true;
            this.cboDataBase.Location = new System.Drawing.Point(294, 12);
            this.cboDataBase.Name = "cboDataBase";
            this.cboDataBase.Size = new System.Drawing.Size(141, 20);
            this.cboDataBase.TabIndex = 0;
            this.cboDataBase.ValueMember = "database";
            this.cboDataBase.SelectedIndexChanged += new System.EventHandler(this.cboDataBase_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(249, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入注释或字段";
            // 
            // txtField
            // 
            this.txtField.Location = new System.Drawing.Point(105, 39);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(422, 21);
            this.txtField.TabIndex = 3;
            this.txtField.TextChanged += new System.EventHandler(this.txtFiled_TextChanged);
            // 
            // checkedListBox
            // 
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(600, 66);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(198, 292);
            this.checkedListBox.TabIndex = 5;
            this.checkedListBox.DoubleClick += new System.EventHandler(this.checkedListBox_DoubleClick);
            // 
            // dg1
            // 
            this.dg1.AllowUserToAddRows = false;
            this.dg1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TableSchema,
            this.TableName,
            this.ColumnName,
            this.ColumnType,
            this.ColumnComment,
            this.dataType});
            this.dg1.Location = new System.Drawing.Point(12, 66);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg1.Size = new System.Drawing.Size(582, 292);
            this.dg1.TabIndex = 6;
            this.dg1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellDoubleClick);
            this.dg1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dg1_RowPostPaint);
            // 
            // TableSchema
            // 
            this.TableSchema.DataPropertyName = "Table_Schema";
            this.TableSchema.HeaderText = "数据库";
            this.TableSchema.Name = "TableSchema";
            // 
            // TableName
            // 
            this.TableName.DataPropertyName = "Table_Name";
            this.TableName.HeaderText = "表名";
            this.TableName.Name = "TableName";
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "Column_Name";
            this.ColumnName.HeaderText = "列名";
            this.ColumnName.Name = "ColumnName";
            // 
            // ColumnType
            // 
            this.ColumnType.DataPropertyName = "Column_Type";
            this.ColumnType.HeaderText = "数据类型";
            this.ColumnType.Name = "ColumnType";
            // 
            // ColumnComment
            // 
            this.ColumnComment.DataPropertyName = "Column_Comment";
            this.ColumnComment.HeaderText = "注释";
            this.ColumnComment.Name = "ColumnComment";
            // 
            // dataType
            // 
            this.dataType.DataPropertyName = "data_type";
            this.dataType.HeaderText = "data_type";
            this.dataType.Name = "dataType";
            this.dataType.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(452, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "表";
            // 
            // cboTableName
            // 
            this.cboTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTableName.FormattingEnabled = true;
            this.cboTableName.Location = new System.Drawing.Point(475, 12);
            this.cboTableName.Name = "cboTableName";
            this.cboTableName.Size = new System.Drawing.Size(119, 20);
            this.cboTableName.TabIndex = 7;
            this.cboTableName.SelectedIndexChanged += new System.EventHandler(this.cboTableName_SelectedIndexChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Table_Schema";
            this.dataGridViewTextBoxColumn1.HeaderText = "数据库";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Table_Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "表名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Column_Name";
            this.dataGridViewTextBoxColumn3.HeaderText = "列名";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Column_Type";
            this.dataGridViewTextBoxColumn4.HeaderText = "数据类型";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Column_Comment";
            this.dataGridViewTextBoxColumn5.HeaderText = "注释";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "data_type";
            this.dataGridViewTextBoxColumn6.HeaderText = "data_type";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // btnConfirmSel
            // 
            this.btnConfirmSel.Location = new System.Drawing.Point(533, 38);
            this.btnConfirmSel.Name = "btnConfirmSel";
            this.btnConfirmSel.Size = new System.Drawing.Size(61, 23);
            this.btnConfirmSel.TabIndex = 9;
            this.btnConfirmSel.Text = "确认选择";
            this.btnConfirmSel.UseVisualStyleBackColor = true;
            this.btnConfirmSel.Click += new System.EventHandler(this.btnConfirmSel_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(671, 38);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(57, 23);
            this.btnBuild.TabIndex = 10;
            this.btnBuild.Text = "生成";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 374);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(786, 129);
            this.txtResult.TabIndex = 11;
            this.txtResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResult_KeyPress);
            // 
            // btnExportEntity
            // 
            this.btnExportEntity.Location = new System.Drawing.Point(734, 38);
            this.btnExportEntity.Name = "btnExportEntity";
            this.btnExportEntity.Size = new System.Drawing.Size(64, 23);
            this.btnExportEntity.TabIndex = 12;
            this.btnExportEntity.Text = "生成导出";
            this.btnExportEntity.UseVisualStyleBackColor = true;
            this.btnExportEntity.Click += new System.EventHandler(this.btnExportEntity_Click);
            // 
            // txtEntityName
            // 
            this.txtEntityName.Location = new System.Drawing.Point(671, 12);
            this.txtEntityName.Name = "txtEntityName";
            this.txtEntityName.Size = new System.Drawing.Size(127, 21);
            this.txtEntityName.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(600, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "实体类名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "链接串名称";
            // 
            // cboConnName
            // 
            this.cboConnName.DisplayMember = "Name";
            this.cboConnName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnName.FormattingEnabled = true;
            this.cboConnName.Location = new System.Drawing.Point(105, 12);
            this.cboConnName.Name = "cboConnName";
            this.cboConnName.Size = new System.Drawing.Size(138, 20);
            this.cboConnName.TabIndex = 16;
            this.cboConnName.ValueMember = "ConnectionStr";
            this.cboConnName.SelectedIndexChanged += new System.EventHandler(this.cboConnName_SelectedIndexChanged);
            // 
            // FrmBuildEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 612);
            this.Controls.Add(this.cboConnName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEntityName);
            this.Controls.Add(this.btnExportEntity);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnConfirmSel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboTableName);
            this.Controls.Add(this.dg1);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.txtField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboDataBase);
            this.Name = "FrmBuildEntity";
            this.Text = "FrmBuildEntity";
            this.Load += new System.EventHandler(this.FrmBuildEntity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDataBase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtField;
        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.DataGridView dg1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableSchema;
        private System.Windows.Forms.DataGridViewTextBoxColumn TableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Button btnConfirmSel;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnExportEntity;
        private System.Windows.Forms.TextBox txtEntityName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboConnName;
    }
}