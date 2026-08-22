using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.model;
using AccountingSystem.view.ReportPages;
using AccountingSystem.view.Screens;
using AccountingSystem.view.Screens.CustmoreMangement; 
using AccountingSystem.view.Screens.supliresAndCustmoreSystem;
using AccountingSystem.view.SupScreens.ChestManagement;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using AccountingSystem.view.SupScreens.EmployeesManagement;
using AccountingSystem.view.SupScreens.PurchasesSystem;
using AccountingSystem.view.SupScreens.SalesSystem;

namespace AccountingSystem
{
    public partial class HomeScereen : KryptonForm
    {
        HomeScreenController controller;
       
        bool sideBarExpand = true;
        bool sideBarLastExpand = false;
        Form activeForm;
        Button tabActiveButton;
        Button activeButton;
        Color activeButtonColor = Color.FromArgb(70, 95, 118);
        Color mainButtonColor = Color.FromArgb(24, 56, 84);
        string pathPictureUser = "pictureUser" + LoginData.employee.id + ".jpg";
        //+ LoginData.employee.id
        public HomeScereen()
        { 
           // this.Icon=Image.f
            controller=new HomeScreenController();
            //this.Icon = Application.OpenForms[0].Icon;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
         
            var dBContext= new AccountingDbContext();
            InitializeComponent();
            InitializeMyComponent();
            Reset();
            var temp = dBContext.Operations.AsNoTracking().ToList();
           
            totalData();
            // إنشاء سلسلة بيانات جديدة
          
            //   AccountingDbContext dBContext = new AccountingDbContext();

            //   guna2DataGridView2.DataSource = dBContext.AccountsGroups.Select(a => new {a.id, a.name}).ToList();

        }

        private void totalData()
        {
            btnCardCustomer.Text="العملاء :  "+controller.countCustomers;
            btnCardSalse.Text="المبيعات :  "+controller.countSales;
            btnCardPurchases.Text="المشتريات :  "+controller.countCustomers;
            btnCardItems.Text="الأصناف :  "+controller.countItems;
            btnCashier.Text="الصناديق :  "+controller.countCashier;
            btnCardEmpolyee.Text="الموظفين :  "+controller.countEmpolyee;
            picturePersonal.Image=Functions.readImage(pathPictureUser);
            pictureSaidBar.Image= Functions.readImage(SharedData.pathImageBrand);
            labelpicturePersonal.Text="مرحبا: "+LoginData.employee.name;

        }

        public HomeScereen(int x)
        {
        //    InitializeComponent();
        //    InitializeMyComponent();
        //    Reset();

        }
        private void Reset()
        {
            
            labelTitelTopBar.Text = "الرئيسية";
            tabActiveButton = this.btnSystems;
            activeButton = this.btnGoToHome;
            activeButton.BackColor = activeButtonColor;
            changeColoTabActiveButton(this.btnSystems);
            supTitel.Visible = false;
            activeButton.Padding=new Padding(0,0,10,0) ;
            leftBorderBtn.Height=activeButton.Height;
            leftBorderBtn.Location = new Point(activeButton.Width-7, activeButton.Top);
            leftBorderBtn.BackColor = Color.FromArgb(221, 229, 241);
            PanelBtnsSystems.Controls.Add(leftBorderBtn);
            leftBorderBtn.BringToFront();
           
        }
        private void timerOpenOrCloseSaideBar_Tick(object sender, EventArgs e)
        {
            
            if (sideBarExpand)
            {
                saideBar.Width = 0;
                if (saideBar.Width <= 0)
                {
                    timerOpenOrCloseSaideBar.Stop();
                    if (activeForm != null)
                    {
                        for (int i = 0; i < activeForm.Controls.Count; i++)
                        {
                             activeForm.Controls[i].Left += 125; 
                        }
                       
                    }
                    sideBarExpand = false; groupSaideBarTap.Width = saideBar.Width;
                }


            }
            else
            {
                saideBar.Width += 250;
                if (saideBar.Width >= 250)
                {
                    if (activeForm != null)
                    {
                        for (int i = 0; i < activeForm.Controls.Count; i++)
                        {
                            activeForm.Controls[i].Left -= 125;
                        }

                    }
                    timerOpenOrCloseSaideBar.Stop();
                    sideBarExpand = true; groupSaideBarTap.Width = saideBar.Width;
                }

            }
            if (activeForm != null)
            {
                activeForm.Dock = DockStyle.None;
                activeForm.Dock = DockStyle.Fill;
            }
        }
        
        private void btnOpenOrCloseSaideBarr_Click(object sender, EventArgs e)
        {
            timerOpenOrCloseSaideBar.Start();
           
        }

        
        public void openChildForm(Form childForm, object sender=null, bool changeColorActivBtn=true)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            this.panelMainBody.Controls.Add(childForm);
            this.labelTitelTopBar.Text = childForm.Text;
            childForm.BringToFront();
            childForm.Show();

           if(changeColorActivBtn)
           changeColorActiveButton(sender);
            activeForm.SizeChanged += ActiveForm_SizeChanged;

        }

        private void ActiveForm_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
           
            //control.Width=control.Width+1;
        }

        private void changeViewRoot()
        {
            //Program.asingeListviewRoot(tabActiveButton.Text);
        //    Program.addToListviewRoot(activeForm.Text);
            this.labelViewRoot.Text = String.Empty;
            //List<string> listviewRoot = Program.getListviewRoot();
           // for (int i = 0; i < listviewRoot.Count(); i++)
           // {
           //     this.labelViewRoot.Text += listviewRoot[i] + ">";
          //  }
            this.supTitel.Visible = true;
        }

        private void changeColoTabActiveButton(Object sender)
        {
            if (sender != null)
            {
             //  leftBorderBtn.Tag= tabActiveButton.Name;
               
                tabActiveButton = (Button)sender;
                foreach (Control item in panelGroupTapBtns.Controls)
                {
                   
                    if (item.GetType() == typeof(Guna2Panel))
                    {
                        if (item.Tag.ToString() == tabActiveButton.Name)
                        {
                            item.Visible = true;

                          
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }
                }
                foreach (Control item in groupSaideBarTap.Controls)
                {
                    if (item.GetType() == typeof(Guna2Panel))
                    {
                        if (item.Tag.ToString() == tabActiveButton.Name)
                        {
                            item.Visible = true;
                            if (item.Controls.Contains(activeButton))
                            {
                                leftBorderBtn.Show();

                            }
                            else
                            {
                                leftBorderBtn.Hide();
                            }
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }
                }
            }
        }
        private void changeColorActiveButton(Object sender)
        {
            if (activeButton != null)
              { activeButton.BackColor = mainButtonColor;
                activeButton.Padding = new Padding(0, 0, 3, 0);
            }

            if (sender != null)
            {
                activeButton = (Button)sender;
                activeButton.BackColor = activeButtonColor;
                activeButton.Padding = new Padding(0, 0, 7, 0);
                leftBorderBtn.Top = activeButton.Top;
                leftBorderBtn.Show();
                guna2PictureBox1.Image=activeButton.Image;

            }
            changeViewRoot();
        }
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnSystems_Click(object sender, EventArgs e)
        {
            changeColoTabActiveButton(sender);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            changeColoTabActiveButton(sender);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            changeColoTabActiveButton(sender);
        }

        public void btnGoToHome_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            if (activeButton != null)
                activeButton.BackColor = mainButtonColor;
            Reset();
        }

        private void btnGoToCustomerAndSupplierSystem_Click(object sender, EventArgs e)
        {
            openChildForm(new supliresAndCustmoreSystemScreen(), sender);
        }
        private void btnGoToInformationGuide_Click(object sender, EventArgs e)
        {
            openChildForm(new InformationGuide(), sender);
        }

        private void btnGoFinancialSecurities_Click(object sender, EventArgs e)
        {
            openChildForm(new financialSecuritiesSystem(), sender);
        }

        private void btnGotoAccountingSystemScereen_Click(object sender, EventArgs e)
        {  //AccountingSystem
            openChildForm(new AccountingSystem.view.Screens.AccountingSystem(), sender);
        }

        private void btnGoItemsAndWarehouses_Click(object sender, EventArgs e)
        {
            openChildForm(new itemsAndWarehousesSystem(), sender);
        }

        private void btnGoPurchasingAndSalesSystem_Click(object sender, EventArgs e)
        {
            openChildForm(new PurchasingAndSalesSystem(), sender);
        }

        private void btnGoAccountStatement_Click(object sender, EventArgs e)
        {
            openChildForm(new AccountStatementWithNumberHours(), sender);

        }

        private void btnGoTreeAccounts_Click(object sender, EventArgs e)
        {
            openChildForm(new TreeAccounts(), sender);
        }

        private void btnGoScreensReportsGeneralProfessor_Click(object sender, EventArgs e)
        {
            openChildForm(new ScreensReportsGeneralProfessor(), sender);
        }

        private void btnGoReportsInvoicesAndStores_Click(object sender, EventArgs e)
        {
            openChildForm(new InvoicesAndStores(), sender);
        }

        private void btnCardItems_Click(object sender, EventArgs e)
        {
            openChildForm(new ClassifyManagamentScreen(false),btnGoItemsAndWarehouses);
        }

        private void btnCardSalse_Click(object sender, EventArgs e)
        {
            openChildForm(new SalesSystemScreen(), btnGoPurchasingAndSalesSystem);
        }

        private void btnCardPurchases_Click(object sender, EventArgs e)
        {
            openChildForm(new PurchasesSystemScreen(),btnGoPurchasingAndSalesSystem);
        }

        private void btnCardCustomer_Click(object sender, EventArgs e)
        {
            openChildForm(new CustmoreMangementScreen(),btnGoToCustomerAndSupplierSystem);
        }

        private void HomeScereen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            try { Application.Exit(); } catch { }
        }
        private void btnCashier_Click(object sender, EventArgs e)
        {
            openChildForm(new ChestManagementScreen (), btnGotoAccountingSystemScereen);
        }

        private void btnCardEmpolyee_Click(object sender, EventArgs e)
        {
            openChildForm(new EmployeeManagementScreen(),btnGotoAccountingSystemScereen);

        }
        private void image_Click(object sender, EventArgs e)
        {
            if (pictureSaidBar.Image!=null)
            pictureSaidBar.Image.Dispose();
            pictureSaidBar.Image = Functions.saveImage(SharedData.pathImageBrand);

        }

        private void picturePersonal_Click(object sender, EventArgs e)
        {
            if (picturePersonal.Image != null)
                picturePersonal.Image.Dispose();
            picturePersonal.Image = Functions.saveImage(pathPictureUser);
            if (picturePersonal.Image == null)
                picturePersonal.Image = Properties.Resources.SolarUserCircleBold;
        }

        private void panelMainBody_SizeChanged(object sender, EventArgs e)
        {
            //Control control=(Control)sender;
            //foreach (Control item in control.Controls)
            //{
            //    item.Width = control.Width;
            //}
            
           
          
        }

        private void flowMainBody_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            foreach (Control item in control.Controls)
            {
                item.Width = control.Width;
            }
        }

        private void HomeScereen_SizeChanged(object sender, EventArgs e)
        {
            if (sideBarExpand&&Width<Screen.PrimaryScreen.WorkingArea.Width/2)
            {
                sideBarLastExpand=true;
              timerOpenOrCloseSaideBar.Start();
            }
            else if(sideBarLastExpand && Width >= Screen.PrimaryScreen.WorkingArea.Width / 2&&!sideBarExpand)
            {
                timerOpenOrCloseSaideBar.Start();
                sideBarLastExpand = false;
            }
        }
    }
}

