using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.Test;

namespace AccountingSystem
{
    public partial class Test2 : Form
    {

        public Test2()
        {
            InitializeComponent();
          
        }
        ChartOfAccount selectedAccount;
        private void Test2_Load(object sender, EventArgs e)
        {
            
            AccountingDbContext dBContext = new AccountingDbContext();
            CreateTreeView(dBContext.ChartOfAccounts.ToList());

          
        }
        void CreateTreeView(List<ChartOfAccount> accounts) 
        {
            foreach (var account in accounts.Where(x=>x.perantAccount==null).OrderBy(x=>x.rankk))
            {
                TreeNode node = new TreeNode(account.name);
                node.Tag = account;
                AddChildren(node,account.Childrens);
                //treeView1.Nodes.Add(node);
            }

        }
        void AddChildren(TreeNode parentNode, ICollection<ChartOfAccount> children) 
        {
            foreach (var child in children)
            { TreeNode node = new TreeNode(child.name);
                node.Tag = child;
                parentNode.Nodes.Add(node);
                AddChildren(node, child.Childrens);
            }
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {TreeNode node = sender as TreeNode;
            if(e.Button == MouseButtons.Right)
            {
                //selectedAccount= (ChartOfAccount)treeView1.Nodes[treeView1.SelectedNode.Index].Tag;
                //contextMenuStrip1.Show(treeView1, e.X, e.Y);
            }
            else
                selectedAccount=null;
        }

        private void إضافهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedAccount!=null)
            {
                AppDialogAleart.showAleartNoPermissions(selectedAccount.name);
            }
           
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
