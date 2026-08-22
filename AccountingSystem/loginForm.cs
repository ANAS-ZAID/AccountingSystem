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
using Microsoft.EntityFrameworkCore;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
namespace AccountingSystem
{
    public partial class loginForm : Form
    {
        AccountingDbContext dBContext;
       // HomeScereen homeScereen;
        public loginForm()
        {
            InitializeComponent();
             dBContext = new AccountingDbContext();
            dBContext.Employees.ToList();
   //  ChartOfAccount account = new ChartOfAccount() { accountNumber=};
  //          Employee employee = new Employee() { };

           // homeScereen = new HomeScereen();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {  
      Employee employee=dBContext.Employees.Include(i=>i.Permissions).FirstOrDefault(i => i.loginName == userName.Text && i.password == passowrd.Text);
      
            //this.Hide();
            //homeScereen.Show();
            if (employee != null)
            {
                Branch branch = employee.Branch;
                if (branch == null && employee.brancheId.HasValue)
                    branch = dBContext.Branches.FirstOrDefault(x => x.id == employee.brancheId.Value);
                if (branch == null)
                    branch = dBContext.Branches.OrderBy(x => x.id).FirstOrDefault();

                if (employee.status == true)
                {
                    model.LoginData.lodeLoginData(employee,branch);
                    this.Hide();
                   (new HomeScereen() ).Show();
                }
                else AppDialogAleart.showAleartNoPermissions();

            }
            else
            {
                AppDialogAleart.showAleartErrorData("اسم المستخدم أو كلمة المرور خاطئه");
            }

        }
    }
}
