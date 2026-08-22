using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.AccountingGuide
{
    public partial class TreeViewAccounts : Form
    {
        private void TreeViewAccounts_Load(object sender, EventArgs e)
        {
          CreateTreeViewAccounts();
        }
        public TreeViewAccounts()
        {
            InitializeComponent();
        }
        void CreateTreeViewAccounts()
        {   
            using(AccountingDbContext dBContext = new AccountingDbContext())
            {
                treeView.BeginUpdate();
                treeView.Nodes.Clear();
                foreach (var account in dBContext.ChartOfAccounts.Where(x => x.perantAccount == null).OrderBy(x => x.accountNumber))
                {
                    TreeNode node = new TreeNode((checkBox.Checked ? account.accountNumber + "-":"")+account.name);
                    node.Tag = account;
                    AddChildren(node, account.Childrens);
                    treeView.Nodes.Add(node);
                }
               treeView.EndUpdate();
            }

        }
        void AddChildren(TreeNode parentNode, ICollection<ChartOfAccount> children)
        {
            foreach (var child in children)
            {
                TreeNode node = new TreeNode((checkBox.Checked ? child.accountNumber + "-" : "") + child.name);
                node.Tag = child;
                parentNode.Nodes.Add(node);
                AddChildren(node, child.Childrens);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
        CreateTreeViewAccounts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            CreateTreeViewAccounts();
        }
    }
}
