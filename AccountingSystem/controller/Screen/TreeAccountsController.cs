using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.core.Functions;
using System.Xml.Linq;

namespace AccountingSystem.controller
{
    public class TreeAccountsController
    {

            public BindingSource dataSource;
            public dynamic allData;
            AccountingDbContext dBContext;
            public DataRowCollection dataRowCollection;
            public TreeAccountsController()
            {
                dBContext = new AccountingDbContext();
                dataSource = new BindingSource();
              
            }

            public void loadTableData()
            {
                try
                {

                    allData = dBContext.ChartOfAccounts.Include(x => x.perantAccount).ToList().Select(x => new
                    {
                    number=x.accountNumber,
                    name=x.name,
                    type=x.type,
                    rankk=x.rankk,
                    parentNumber=x.perantAccount?.accountNumber,
                    parent=x.perantAccount?.name,
                    natureOfAccount = x.natureOfAccount,
                    numberColor = x.rankk
                    }
                    ).ToList();

                fillTableData();
                   
                }
                catch 
                {
                    AppDialogAleart.showAleartError();
                }
            }

            void fillTableData()
            {
                    foreach (var item in allData)
                    {
                        dataRowCollection.Add(item.number,item.name, item.type, item.rankk,
                            item.parentNumber, item.parent, item.natureOfAccount,  item.numberColor);
                    }
            }

        }
}
