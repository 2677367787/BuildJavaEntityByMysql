namespace AutoBuildSql
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmBuildData = new System.Windows.Forms.ToolStripMenuItem();
            this.生产实体对象ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResolve = new System.Windows.Forms.Button();
            this.txtSqlText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDataBase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cboConnName = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBuildData,
            this.生产实体对象ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(773, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmBuildData
            // 
            this.tsmBuildData.Name = "tsmBuildData";
            this.tsmBuildData.Size = new System.Drawing.Size(91, 21);
            this.tsmBuildData.Text = "生成SQL语句";
            this.tsmBuildData.Click += new System.EventHandler(this.tsmBuildData_Click);
            // 
            // 生产实体对象ToolStripMenuItem
            // 
            this.生产实体对象ToolStripMenuItem.Name = "生产实体对象ToolStripMenuItem";
            this.生产实体对象ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.生产实体对象ToolStripMenuItem.Text = "生产实体对象";
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(576, 264);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 1;
            this.btnResolve.Text = "解析";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSqlText
            // 
            this.txtSqlText.Location = new System.Drawing.Point(16, 67);
            this.txtSqlText.Multiline = true;
            this.txtSqlText.Name = "txtSqlText";
            this.txtSqlText.Size = new System.Drawing.Size(745, 186);
            this.txtSqlText.TabIndex = 2;
            this.txtSqlText.Text = resources.GetString("txtSqlText.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(412, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "数据库";
            // 
            // cboDataBase
            // 
            this.cboDataBase.DisplayMember = "database";
            this.cboDataBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBase.FormattingEnabled = true;
            this.cboDataBase.Location = new System.Drawing.Point(459, 35);
            this.cboDataBase.Name = "cboDataBase";
            this.cboDataBase.Size = new System.Drawing.Size(302, 20);
            this.cboDataBase.TabIndex = 3;
            this.cboDataBase.ValueMember = "database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据库链接名";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 293);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(749, 197);
            this.txtResult.TabIndex = 7;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(686, 264);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cboConnName
            // 
            this.cboConnName.DisplayMember = "Name";
            this.cboConnName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnName.FormattingEnabled = true;
            this.cboConnName.Location = new System.Drawing.Point(97, 35);
            this.cboConnName.Name = "cboConnName";
            this.cboConnName.Size = new System.Drawing.Size(221, 20);
            this.cboConnName.TabIndex = 9;
            this.cboConnName.ValueMember = "ConnectionStr";
            this.cboConnName.SelectedIndexChanged += new System.EventHandler(this.cboConnName_SelectedIndexChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 513);
            this.Controls.Add(this.cboConnName);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboDataBase);
            this.Controls.Add(this.txtSqlText);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmBuildData;
        private System.Windows.Forms.ToolStripMenuItem 生产实体对象ToolStripMenuItem;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.TextBox txtSqlText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDataBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cboConnName;
    }
}

