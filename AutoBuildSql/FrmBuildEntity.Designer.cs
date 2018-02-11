﻿namespace AutoBuildSql
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
            this.label3 = new System.Windows.Forms.Label();
            this.cboTableName = new System.Windows.Forms.ComboBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableSchema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirmSel = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDataBase
            // 
            this.cboDataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBase.FormattingEnabled = true;
            this.cboDataBase.Location = new System.Drawing.Point(107, 12);
            this.cboDataBase.Name = "cboDataBase";
            this.cboDataBase.Size = new System.Drawing.Size(211, 20);
            this.cboDataBase.TabIndex = 0;
            this.cboDataBase.SelectedIndexChanged += new System.EventHandler(this.cboDataBase_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入注释或字段";
            // 
            // txtField
            // 
            this.txtField.Location = new System.Drawing.Point(107, 38);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(489, 21);
            this.txtField.TabIndex = 3;
            this.txtField.TextChanged += new System.EventHandler(this.txtFiled_TextChanged);
            // 
            // checkedListBox
            // 
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(602, 65);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(156, 292);
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
            this.dg1.Location = new System.Drawing.Point(14, 65);
            this.dg1.Name = "dg1";
            this.dg1.RowTemplate.Height = 23;
            this.dg1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg1.Size = new System.Drawing.Size(582, 292);
            this.dg1.TabIndex = 6;
            this.dg1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg1_CellDoubleClick);
            this.dg1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dg1_RowPostPaint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "表";
            // 
            // cboTableName
            // 
            this.cboTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTableName.FormattingEnabled = true;
            this.cboTableName.Location = new System.Drawing.Point(385, 12);
            this.cboTableName.Name = "cboTableName";
            this.cboTableName.Size = new System.Drawing.Size(211, 20);
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
            // btnConfirmSel
            // 
            this.btnConfirmSel.Location = new System.Drawing.Point(602, 37);
            this.btnConfirmSel.Name = "btnConfirmSel";
            this.btnConfirmSel.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmSel.TabIndex = 9;
            this.btnConfirmSel.Text = "确认选择";
            this.btnConfirmSel.UseVisualStyleBackColor = true;
            this.btnConfirmSel.Click += new System.EventHandler(this.btnConfirmSel_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(683, 37);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 10;
            this.btnBuild.Text = "生成";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(14, 373);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(744, 129);
            this.txtResult.TabIndex = 11;
            // 
            // FrmBuildEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 514);
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
    }
}