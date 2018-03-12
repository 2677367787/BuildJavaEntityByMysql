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
    public partial class FrmMenu : DockContent
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void 生成SQL语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMain fm = new FrmMain { MdiParent = this };
            fm.Show(dockPanel1); 
        }

        private void 同步数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSynData fm = new FrmSynData { MdiParent = this };
            fm.Show(dockPanel1);
        }

        private void 生成实体对象ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBuildEntity fm = new FrmBuildEntity { MdiParent = this };
            fm.Show(dockPanel1);
        }
    }
}
