namespace AutoBuildSql
{
    partial class FrmSynData
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
            this.cboConnSource = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSynData = new System.Windows.Forms.Button();
            this.txtSqlText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDbSource = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboDbTarger = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboConnTarger = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboConnSource
            // 
            this.cboConnSource.DisplayMember = "Name";
            this.cboConnSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnSource.FormattingEnabled = true;
            this.cboConnSource.Location = new System.Drawing.Point(89, 26);
            this.cboConnSource.Name = "cboConnSource";
            this.cboConnSource.Size = new System.Drawing.Size(141, 20);
            this.cboConnSource.TabIndex = 18;
            this.cboConnSource.ValueMember = "ConnectionStr";
            this.cboConnSource.SelectedIndexChanged += new System.EventHandler(this.cboConnSource_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "链接串名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "数据库名称";
            // 
            // btnSynData
            // 
            this.btnSynData.Location = new System.Drawing.Point(478, 106);
            this.btnSynData.Name = "btnSynData";
            this.btnSynData.Size = new System.Drawing.Size(75, 23);
            this.btnSynData.TabIndex = 25;
            this.btnSynData.Text = "同步";
            this.btnSynData.UseVisualStyleBackColor = true;
            this.btnSynData.Click += new System.EventHandler(this.btnSynData_Click);
            // 
            // txtSqlText
            // 
            this.txtSqlText.Location = new System.Drawing.Point(12, 135);
            this.txtSqlText.Multiline = true;
            this.txtSqlText.Name = "txtSqlText";
            this.txtSqlText.Size = new System.Drawing.Size(541, 249);
            this.txtSqlText.TabIndex = 26;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDbSource);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboConnSource);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 90);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源";
            // 
            // cboDbSource
            // 
            this.cboDbSource.DisplayMember = "database";
            this.cboDbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDbSource.FormattingEnabled = true;
            this.cboDbSource.Location = new System.Drawing.Point(89, 52);
            this.cboDbSource.Name = "cboDbSource";
            this.cboDbSource.Size = new System.Drawing.Size(141, 20);
            this.cboDbSource.TabIndex = 22;
            this.cboDbSource.ValueMember = "database";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboDbTarger);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboConnTarger);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(296, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 90);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标";
            // 
            // cboDbTarger
            // 
            this.cboDbTarger.DisplayMember = "database";
            this.cboDbTarger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDbTarger.FormattingEnabled = true;
            this.cboDbTarger.Location = new System.Drawing.Point(89, 52);
            this.cboDbTarger.Name = "cboDbTarger";
            this.cboDbTarger.Size = new System.Drawing.Size(141, 20);
            this.cboDbTarger.TabIndex = 23;
            this.cboDbTarger.ValueMember = "database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "链接串名称";
            // 
            // cboConnTarger
            // 
            this.cboConnTarger.DisplayMember = "Name";
            this.cboConnTarger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnTarger.FormattingEnabled = true;
            this.cboConnTarger.Location = new System.Drawing.Point(89, 26);
            this.cboConnTarger.Name = "cboConnTarger";
            this.cboConnTarger.Size = new System.Drawing.Size(141, 20);
            this.cboConnTarger.TabIndex = 18;
            this.cboConnTarger.ValueMember = "ConnectionStr";
            this.cboConnTarger.SelectedIndexChanged += new System.EventHandler(this.cboConnTarger_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "数据库名称";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 390);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(541, 141);
            this.txtResult.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "SQL语句";
            // 
            // FrmSynData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 545);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtSqlText);
            this.Controls.Add(this.btnSynData);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmSynData";
            this.Text = "数据同步";
            this.Load += new System.EventHandler(this.FrmSynData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboConnSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSynData;
        private System.Windows.Forms.TextBox txtSqlText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboConnTarger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDbSource;
        private System.Windows.Forms.ComboBox cboDbTarger;
    }
}