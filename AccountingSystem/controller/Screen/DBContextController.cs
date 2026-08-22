using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.shared;
using AccountingSystem.model;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.controller.Screen
{
    public abstract class DBContextController
    {
        public List<string> columnsNamesInAR;
        protected AccountingDbContext dBContext = new AccountingDbContext();
        public BindingSource dataSource = new BindingSource();
        public Permission permissions { get; set; }
        protected DataTable dataTable;
        protected DataTable dataAppTable;
        public string transactionType { get; set; }
        public int? EmployId => LoginData.employee?.id;
        public int? BranchId => LoginData.branch?.id;
        public bool HasDataProcessed = false;
        public bool HasHomeScreenDataProcessed;
        public bool HasAddAndUpdateScreenDataProcessed;
        public ProsessesType prosessesType;
        public List<dynamic> tempData;

        abstract public void showDialogView(DataGridViewRow row);
        abstract public void showDialogAdd();
        abstract public void showDialogUpdate(int id);
        abstract public bool delete(List<int> keys);
        //abstract public bool dataProcessing();
        abstract public bool find(int id);
        abstract public bool add();
        abstract public bool update();
        abstract protected void fillTableData();
        abstract protected void fillDataGridView();
        abstract public bool lodeData(bool shearch = false, dynamic additional = null);
        abstract public void clearTempData();
        public void startHSDP() { HasHomeScreenDataProcessed = false; }
        public void startADSDP() { HasAddAndUpdateScreenDataProcessed = false; }
        public void startHADSDP() { startHSDP();startADSDP(); }
        public void endHSDP() { HasHomeScreenDataProcessed = true; }
        public void endADSDP() { HasAddAndUpdateScreenDataProcessed = true; }
        public void endHADSDP() { endHSDP();startADSDP(); }
       
        public bool IsADSDPOrHADSDP { get { return HasAddAndUpdateScreenDataProcessed || HasHomeScreenDataProcessed; } }
        public bool IsAdd { get { return prosessesType == ProsessesType.add; } }
        public bool IsUpdate { get { return prosessesType == ProsessesType.update; } }
    }
}
