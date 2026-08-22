using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.Database.Seed;
using AccountingSystem.core.shared;
using AccountingSystem.model;
using AccountingSystem.view.ReportPages;
using AccountingSystem.view.Screens.BranchManagement;
using AccountingSystem.view.Screens.CurrencyManagement;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using AccountingSystem.view.SupScreens.CompoundJournalEntries;
using AccountingSystem.view.SupScreens.InventoryTransferManagament;
using AccountingSystem.view.SupScreens.SalesSystem;
using AccountingSystem.view.SupScreens.SimpleJournalEntries;

namespace AccountingSystem
{
    internal static class Program
    {
       public static HomeScereen homeScereen()
        {
            return (HomeScereen)Application.OpenForms[2];
        }
        static private List<string> viewRoot = new List<string> { };
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //LoginData.lodeLoginData();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                DatabaseSeedResult seedResult = DatabaseSeeder.SeedIfRequired();
                if (seedResult.Seeded)
                {
                    using (var credentialsDialog = new SeedCredentialsDialog(
                        seedResult.AdminLoginName,
                        seedResult.AdminPassword))
                    {
                        credentialsDialog.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "تعذر تهيئة قاعدة البيانات.\n\n" + ex.GetBaseException().Message,
                    "خطأ في تهيئة النظام",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new loginForm());
        }
        static public void asingeListviewRoot(string titel)
        {
            viewRoot = new List<string> { titel };
        }
        static public List<string> getListviewRoot()
        {
            return viewRoot;
        }
        static public void addToListviewRoot(string titel)
        {
            viewRoot.Add(titel);
        }
        static public void removeFromListviewRoot(string titel)
        {
            viewRoot.Remove(titel);
        }
        static public void clearListviewRoot()
        {
            viewRoot.Clear();
        }
    }
/*    view.Screens.CustmoreMangement.DialogAddCustmore
*/}
