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
            this.btnResolve = new System.Windows.Forms.Button();
            this.txtSqlText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDataBase = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.cboConnName = new System.Windows.Forms.ComboBox();
            this.chkAdd = new System.Windows.Forms.CheckBox();
            this.chkDel = new System.Windows.Forms.CheckBox();
            this.chkUpd = new System.Windows.Forms.CheckBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(686, 235);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 1;
            this.btnResolve.Text = "解析";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // txtSqlText
            // 
            this.txtSqlText.Location = new System.Drawing.Point(16, 43);
            this.txtSqlText.Multiline = true;
            this.txtSqlText.Name = "txtSqlText";
            this.txtSqlText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSqlText.Size = new System.Drawing.Size(745, 186);
            this.txtSqlText.TabIndex = 2;
            this.txtSqlText.Text = resources.GetString("txtSqlText.Text");
            this.txtSqlText.WordWrap = false;
            this.txtSqlText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSqlText_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(412, 14);
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
            this.cboDataBase.Location = new System.Drawing.Point(459, 11);
            this.cboDataBase.Name = "cboDataBase";
            this.cboDataBase.Size = new System.Drawing.Size(302, 20);
            this.cboDataBase.TabIndex = 3;
            this.cboDataBase.ValueMember = "database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据库链接名";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 264);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(749, 226);
            this.txtResult.TabIndex = 7;
            this.txtResult.WordWrap = false;
            this.txtResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSqlText_KeyPress);
            // 
            // cboConnName
            // 
            this.cboConnName.DisplayMember = "Name";
            this.cboConnName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConnName.FormattingEnabled = true;
            this.cboConnName.Location = new System.Drawing.Point(97, 11);
            this.cboConnName.Name = "cboConnName";
            this.cboConnName.Size = new System.Drawing.Size(221, 20);
            this.cboConnName.TabIndex = 9;
            this.cboConnName.ValueMember = "ConnectionStr";
            this.cboConnName.SelectedIndexChanged += new System.EventHandler(this.cboConnName_SelectedIndexChanged);
            // 
            // chkAdd
            // 
            this.chkAdd.AutoSize = true;
            this.chkAdd.Location = new System.Drawing.Point(396, 239);
            this.chkAdd.Name = "chkAdd";
            this.chkAdd.Size = new System.Drawing.Size(72, 16);
            this.chkAdd.TabIndex = 10;
            this.chkAdd.Text = "新增语句";
            this.chkAdd.UseVisualStyleBackColor = true;
            // 
            // chkDel
            // 
            this.chkDel.AutoSize = true;
            this.chkDel.Location = new System.Drawing.Point(496, 239);
            this.chkDel.Name = "chkDel";
            this.chkDel.Size = new System.Drawing.Size(72, 16);
            this.chkDel.TabIndex = 11;
            this.chkDel.Text = "删除语句";
            this.chkDel.UseVisualStyleBackColor = true;
            // 
            // chkUpd
            // 
            this.chkUpd.AutoSize = true;
            this.chkUpd.Location = new System.Drawing.Point(596, 239);
            this.chkUpd.Name = "chkUpd";
            this.chkUpd.Size = new System.Drawing.Size(72, 16);
            this.chkUpd.TabIndex = 12;
            this.chkUpd.Text = "更新语句";
            this.chkUpd.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 496);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(749, 158);
            this.txtLog.TabIndex = 13;
            this.txtLog.WordWrap = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 661);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.chkUpd);
            this.Controls.Add(this.chkDel);
            this.Controls.Add(this.chkAdd);
            this.Controls.Add(this.cboConnName);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboDataBase);
            this.Controls.Add(this.txtSqlText);
            this.Controls.Add(this.btnResolve);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmMain";
            this.Text = "生成SQL";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.TextBox txtSqlText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDataBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.ComboBox cboConnName;
        private System.Windows.Forms.CheckBox chkAdd;
        private System.Windows.Forms.CheckBox chkDel;
        private System.Windows.Forms.CheckBox chkUpd;
        private System.Windows.Forms.TextBox txtLog;
    }
}

