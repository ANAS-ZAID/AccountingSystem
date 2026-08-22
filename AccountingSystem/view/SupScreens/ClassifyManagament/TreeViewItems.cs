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

namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class TreeViewItems : Form
    {
        public TreeViewItems()
        {
            InitializeComponent();
        }
        void CreateTreeViewItems()
        {
            using (AccountingDbContext dBContext = new AccountingDbContext())
            {
                treeView.BeginUpdate();
                treeView.Nodes.Clear();
                foreach (var item in dBContext.Classifies.Where(x => x.perantItem == null).OrderBy(x => x.ClassifyNumber))
                {
                    TreeNode node = new TreeNode((checkBox.Checked ? item.ClassifyNumber + "-" : "") + item.nameAr);
                    node.Tag = item;
                    AddChildren(node, item.Childrens);
                    treeView.Nodes.Add(node);
                }
                treeView.EndUpdate();
            }

        }
        void AddChildren(TreeNode parentNode, ICollection<Classify> children)
        {
            foreach (var child in children)
            {
                TreeNode node = new TreeNode((checkBox.Checked ? child.ClassifyNumber + "-" : "") + child.nameAr);
                node.Tag = child;
                parentNode.Nodes.Add(node);
                AddChildren(node, child.Childrens);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CreateTreeViewItems();
        }

        private void TreeViewItems_Load(object sender, EventArgs e)
        {
            CreateTreeViewItems();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefersh_Click(object sender, EventArgs e)
        {
            CreateTreeViewItems();
        }
    }
}
