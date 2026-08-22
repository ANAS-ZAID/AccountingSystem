using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using AccountingSystem.core.Functions;

namespace AccountingSystem.core.shared
{
    public class TablePagination
    {
        List<string> tableColumnsInAR = new List<string>();
        List<int> indexsColumnsHide = new List<int>();
        public Guna2GroupBox groupBoxTable { get; set; }
        public KryptonDataGridView dataGridView { get; set; }
        public KryptonDataGridView hederGridView { get; set; }
        Guna2Panel footerPage;
        FlowLayoutPanel ceterFooterPage;
        FlowLayoutPanel btnFooterPage;
        public DataTable pagesData;
        //  EventHandler eventHandler;
        private int pageSize = 30; // عدد السجلات في كل صفحة
        private int currentPage = 1;
        private int totalPages = 1;
        private int keyIndex = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="footerPage"></param>
        /// <param name="ceterFooter"></param>
        /// <param name="groupBoxTable"></param>
        /// <param name="min"></param>
        /// 
        public Guna2GroupBox getGuna2GroupBox()
        {
            return groupBoxTable;
        }
        public TablePagination(List<string> tableColumnsInAR)
        {

            this.tableColumnsInAR = tableColumnsInAR;


        }
        public TablePagination(int keyIndex = 0)
        {
            this.keyIndex = keyIndex;
            groupBoxTable = new Guna2GroupBox();
            dataGridView = new KryptonDataGridView();
            footerPage = new Guna2Panel();
            btnFooterPage = new FlowLayoutPanel();
            ceterFooterPage = new FlowLayoutPanel();
            groupBoxTable.Visible = false;

        }
        public TablePagination(List<string> tableColumnsInAR, List<int> indexsColumnsHide)
        {
            this.indexsColumnsHide = indexsColumnsHide;
            this.tableColumnsInAR = tableColumnsInAR;
        }
        // void buildeHederGridView(DataGridViewColumn[] header)
        //{




        //}
        public Guna2GroupBox duildGroupBoxTable()
        {
            //groupBoxTable = new Guna2GroupBox();
            pagesData = new DataTable();

            return groupBoxTable;
        }


        private void duildTable()
        {
            var table = groupBoxTable.Parent;
            table.BeginInvoke(new Action(() =>
            {
                groupBoxTable = BuildControls.buildGroupBoxTable();
                table.Controls.Add(groupBoxTable);
                dataGridView = (KryptonDataGridView)groupBoxTable.Controls[0];
                footerPage = (Guna2Panel)groupBoxTable.Controls[1];
                ceterFooterPage = (FlowLayoutPanel)footerPage.Controls[0];
                groupBoxTable.Visible = true;
                groupBoxTable.BringToFront();
                buildPaginationBtn();
                LinkingEvents();
                pagesDataSource();
            }));
            //}

        }
        BindingSource bindingSource;
        private void guna2DataGridView1_BindingContextChanged(object sender, EventArgs e)
        {

            pagesData = bindingSource.DataSource as DataTable;
            pagesDataSource();
        }
        public void changePageSize(string pageSize)
        {
            if (!String.IsNullOrEmpty(pageSize))
                this.pageSize = int.Parse(pageSize);
            else this.pageSize = 30;
        }
        public void changeReportePageSize(string pageSize)
        {
            if (!String.IsNullOrEmpty(pageSize))
                this.pageSize = int.Parse(pageSize);
            else this.pageSize = int.Parse(!String.IsNullOrEmpty(groupBoxTable.Name) ? groupBoxTable.Name : "100");
        }



        public void fillData(BindingSource bindingSource)
        {

            
            bindingSource.DataSourceChanged += guna2DataGridView1_BindingContextChanged;
            this.bindingSource = bindingSource;
            pagesData = bindingSource.DataSource as DataTable;
            //currentPage = 1;
            Thread thread = new Thread(duildTable);
            thread.Start();



        }

        private void LinkingEvents()
        {
            var parent = groupBoxTable.Parent.Parent;
            if (parent is FlowLayoutPanel)
            {
                parent.SizeChanged += FlowLayoutPanel_SizeChanged;
                FlowLayoutPanel_SizeChanged(parent, null);
                foreach (System.Windows.Forms.Control control in parent.Controls)
                {
                    if (control is LinkLabel)
                    {

                        ((LinkLabel)control).Click += LinkLabel_Click;
                    }
                }
            }

        }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            foreach (System.Windows.Forms.Control control in linkLabel.Parent.Controls)
            {
                if (control.Name == "PanelOptionSearchAndPrint")
                {
                    if (control.Visible)
                    {
                        linkLabel.Text = "عرض خيارات البحث و الطباعه";
                        control.Visible = false;
                    }
                    else
                    {
                        linkLabel.Text = "إخفاء خيارات البحث و الطباعه";
                        control.Visible = true;

                    }
                    FlowLayoutPanel_SizeChanged(linkLabel.Parent, null);
                }
            }

        }

        private void FlowLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel flowLayout = (FlowLayoutPanel)sender;
            int heightChaild = 0;
            int indexTable = -1;
            for (int i = 0; i < flowLayout.Controls.Count; i++)
            {
                System.Windows.Forms.Control control = flowLayout.Controls[i];
                if (control.Name != "table")
                {
                    control.Width = flowLayout.Width;
                    if (control.Visible)
                        heightChaild += control.Height;
                }
                else
                {

                    control.Width = flowLayout.Width - 10;
                    indexTable = i;
                }
            }
            if (indexTable > 0)
                flowLayout.Controls[indexTable].Height = flowLayout.Height - heightChaild - 20;
            // ceterFooterPage.BackColor=Color.Red;
            ceterFooterPage.Width = flowLayout.Width / 3 - (int)(flowLayout.Width / 3 * 0.12);
            ceterFooterPage.Left = (flowLayout.Width / 3) + (int)(flowLayout.Width / 3 * 0.12);

            //    btnFooterPage.Left = (ceterFooterPage.Width / 3);
            //refereshFooterBtn();

        }
        public void pagesDataSource()
        {
            int totalRecords = pagesData?.Rows?.Count ?? 0;
            if (groupBoxTable.InvokeRequired)
                groupBoxTable.BeginInvoke(new Action(() => groupBoxTable.Text = "العدد : " + totalRecords));
            else
                groupBoxTable.Text = "العدد : " + totalRecords;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            viewPage();
        }
        public void viewPage()
        {

            Thread thread = new Thread(setData);
            thread.Start();
            refereshFooterBtn();
        }

        private void setData()
        {
            //DataTable pageData = Functions.getPagedDataTable(pagesData, currentPage, pageSize);
            if (currentPage < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(currentPage), "Page number must be greater than 0.");
            }

            // التحقق من أن جدول البيانات ليس فارغًا
            if (pagesData == null)
                return; // أو يمكنك إرجاع جدول بيانات فارغ جديد
            else if (pagesData.Rows.Count == 0)
                if (dataGridView.InvokeRequired)
                    dataGridView.Invoke(new Action(() => dataGridView.DataSource = pagesData?.Clone()));
                else
                    dataGridView.DataSource = pagesData?.Clone();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, pagesData.Rows.Count);

            // استخدام Select لانتقاء الصفوف المطلوبة
            var selectedRows = pagesData.Rows.Cast<DataRow>()
                                            .Skip(startIndex)
                                            .Take(endIndex - startIndex);

            // إنشاء جدول بيانات جديد لاحتواء الصفوف المحددة
            DataTable pagedDataTable = pagesData.Clone();
            // for (int i = 0; i < 50; i++)
            foreach (var row in selectedRows)
            {
                pagedDataTable.Rows.Add(row.ItemArray);
            }
            //if (pagedDataTable.Rows.Count < 1 && pageNumber - 1 > 0)
            //    pagedDataTable = getPagedDataTable(dataTable, pageNumber - 1, pageSize);
            dataGridView.BeginInvoke(new Action(() => dataGridView.DataSource = pagedDataTable));

        }


        public int getCurrentPage() => currentPage;
        public DataGridViewRow getCurrentSelectedRow() => dataGridView.CurrentRow;
        public int getIndexCurrentSelectedRow() => dataGridView.CurrentRow.Index;
        public int getKeyCurrentSelectedRow()
        {
            if (getCurrentSelectedRow() != null)
                return Convert.ToInt32(dataGridView.CurrentRow.Cells[keyIndex].Value);
            else return 0;
        }
        public List<int> getIndexCurrentSelectedRows()
        {
            List<int> selectedRows = new List<int>();
            foreach (DataGridViewRow item in dataGridView.SelectedRows)
            {
                selectedRows.Add(item.Index);
            }
            return selectedRows;
        }
        public List<int> getKeysSelectedRows()
        {
            List<int> selectedRows = new List<int>();
            foreach (DataGridViewRow item in dataGridView.SelectedRows)
            {
                selectedRows.Add(Convert.ToInt32(item.Cells[keyIndex].Value));
            }
            return selectedRows;
        }
        public DataGridViewSelectedRowCollection getSelectedRows() => dataGridView.SelectedRows;
        void moveScrollToCurrentBtnPage()
        {
            var btn = btnFooterPage.Controls.OfType<Guna2Button>();
            var btnMin = btn.Where(x => int.Parse(x.Name) > currentPage);
            int visibleWidth = btnFooterPage.Bounds.Width;
            int totalWidth = btn.OfType<Guna2Button>().Sum(x => x.Bounds.Width);
            double scrollBarRatio = (double)visibleWidth / totalWidth;
            int scrollBarWidth = visibleWidth;
            int scrollBarThumbSize = (int)(scrollBarWidth * scrollBarRatio);
            btnFooterPage.AutoScrollPosition = new Point(btnMin.Sum(x => x.Bounds.Width) - (int)(scrollBarThumbSize * 0.63), 0);

        }
        private void btnChangePage_Click(object sender, EventArgs e)
        {

            Guna2Button button = (Guna2Button)sender;
            int pageNumber = int.Parse(button.Text);

            if ((pageNumber != currentPage))
            {
                currentPage = pageNumber;
                viewPage();
            }
            //btnFooterPage.AutoScrollPosition = new Point(btn.Sum(x => x.Width) - btn.Where(x => int.Parse(x.Name) < pageNumber).Sum(x => x.Width) - (button.Width * 2+button.Width/2), button.Location.X);
            moveScrollToCurrentBtnPage();

        }


        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                viewPage();

            }
            moveScrollToCurrentBtnPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                viewPage();
            }
            moveScrollToCurrentBtnPage();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                viewPage();
            }
            moveScrollToCurrentBtnPage();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (currentPage != totalPages && totalPages > 0)
            {
                currentPage = totalPages;
                viewPage();
            }
            moveScrollToCurrentBtnPage();
        }
        int widthBtn = 40;
        public void refereshFooterBtn()
        {
            //int widthBtn = 40;
            if (btnFooterPage.InvokeRequired)
            {
                btnFooterPage.BeginInvoke(new Action(() =>
                {
                    btnFooterPage.SuspendLayout();
                    btnFooterPage.Controls.Clear();
                }));
            }
            else
            {
                btnFooterPage.SuspendLayout();
                btnFooterPage.Controls.Clear();
            }
            int oldeFooterPageHeight = 50;
            int newFooterPageHeight = 68;
            if (totalPages > 3)
            {
                widthBtn = 45;
                if (btnFooterPage.InvokeRequired)
                    btnFooterPage.BeginInvoke(new Action(() => {
                        btnFooterPage.Height = newFooterPageHeight;
                        ceterFooterPage.Height = newFooterPageHeight;
                        footerPage.Height = newFooterPageHeight;
                    }));
                else
                {
                    btnFooterPage.Height = newFooterPageHeight;
                    ceterFooterPage.Height = newFooterPageHeight;
                    footerPage.Height = newFooterPageHeight;
                }
            }
            else
            {
                if (btnFooterPage.InvokeRequired)
                    btnFooterPage.BeginInvoke(new Action(() =>
                    {
                        btnFooterPage.Height = oldeFooterPageHeight;
                        ceterFooterPage.Height = oldeFooterPageHeight;
                        footerPage.Height = oldeFooterPageHeight;
                    }));
                else
                {
                    btnFooterPage.Height = oldeFooterPageHeight;
                    ceterFooterPage.Height = oldeFooterPageHeight;
                    footerPage.Height = oldeFooterPageHeight;
                }
            }

            Thread thread = new Thread(buildBtns);
            thread.Start();

        }

        private void buildBtns()
        {
            for (int i = 0; i < totalPages; i++)
            {
                int key = totalPages - i;

                Guna2Button button = BuildControls.buildCircularBtn((totalPages - i).ToString(), new System.Drawing.Size(widthBtn, widthBtn), eventHandlerClick: btnChangePage_Click, name: (totalPages - i).ToString());
                if (currentPage == totalPages - i)
                {
                    button.FillColor = AppColor.primary;
                    button.ForeColor = AppColor.third;
                }
                if (btnFooterPage.InvokeRequired)
                    btnFooterPage.BeginInvoke(new Action(() => btnFooterPage.Controls.Add(button)));
                else
                    btnFooterPage.Controls.Add(button);
            }
            if (btnFooterPage.InvokeRequired)
                btnFooterPage.BeginInvoke(new Action(() => {
                    btnFooterPage.ResumeLayout();
                    CeterFooterPage_SizeChanged(null, null);
                }));
            else
            {
                btnFooterPage.ResumeLayout();
                CeterFooterPage_SizeChanged(null, null);
            }

        }

        private void buildPaginationBtn()
        {
            int widthBtn = 40;
            ceterFooterPage.SizeChanged += CeterFooterPage_SizeChanged;
            ceterFooterPage.AutoScroll = false;
            ceterFooterPage.Controls.Add(BuildControls.buildCircularBtn(">>", new System.Drawing.Size(widthBtn, 40), DockStyle.Left, btnLast_Click, name: "btnLast"));
            ceterFooterPage.Controls.Add(BuildControls.buildCircularBtn(">", new System.Drawing.Size(widthBtn, 40), DockStyle.Left, btnNext_Click));
            btnFooterPage = BuildControls.buildCeterFooter("btnFooterPage");
            ceterFooterPage.Controls.Add(btnFooterPage);
            ceterFooterPage.Controls.Add(BuildControls.buildCircularBtn("<", new System.Drawing.Size(widthBtn, 40), eventHandlerClick: btnPrevious_Click));
            ceterFooterPage.Controls.Add(BuildControls.buildCircularBtn("<<", new System.Drawing.Size(widthBtn, 40), eventHandlerClick: btnFirst_Click, name: "btnFirst"));
        }

        private void CeterFooterPage_SizeChanged(object sender, EventArgs e)
        {

            int w = ceterFooterPage.Width;
            int countBtn = 0;

            if (btnFooterPage != null)
            {
                countBtn = btnFooterPage.Controls.Count;
                if (countBtn > 1)
                { btnFooterPage.Width = ((int)(w / 5.9)) * 2; btnFooterPage.AutoScroll = true; }
                else
                { btnFooterPage.Width = 60; btnFooterPage.AutoScroll = false; }

            }
            int wBtn = (w - btnFooterPage.Width) / 5;
            bool smoll = wBtn < 50;
            foreach (System.Windows.Forms.Control item in ceterFooterPage.Controls)
            {
                if (!(item is FlowLayoutPanel))
                {
                    if(item.InvokeRequired)
                    {
                        item.BeginInvoke(new Action(() =>
                        {
                            if ((item.Name == "btnLast" || item.Name == "btnFirst") && smoll)
                                item.Visible = false;
                            else if (smoll)
                            {
                                item.Width = (w - w / 5) / 3;
                            }
                            else
                            {
                                item.Width = wBtn;
                                item.Visible = true;
                            }
                        }));
                    }
                    else
                    {
                        if ((item.Name == "btnLast" || item.Name == "btnFirst") && smoll)
                            item.Visible = false;
                        else if (smoll)
                        {
                            item.Width = (w - w / 5) / 3;
                        }
                        else
                        {
                            item.Width = wBtn;
                            item.Visible = true;
                        }
                    }
                }


            }
        }


    }
}
