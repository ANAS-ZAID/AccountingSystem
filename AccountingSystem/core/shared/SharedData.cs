using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.core.shared
{
    public static class SharedData
    {//                                                             "العملاء والوكلاء", "الموردين", "الموظفين", "الصناديق", "المخازن" 
        public static Dictionary<string, string> accountLocations = new Dictionary<string, string>()
        {
            { "stores", "المخازن" },
            { "cashiers", "الصناديق" },
            { "employees", "الموظفين" },
            { "s", "الموردين" },
            { "custmoresAndSuplires", "العملاء والوكلاء" },
        };
        public static string pathImages = "AppImages/";
        public static string pathImageBrand = "brand.jpg";
        public static string balanceSheet = "ميزانية عموميه";
        public static string profitLoss = "ارباح وخسائر";

        public static string formatDisplayDate = "yyyy-MM-dd";
        public static string erorrDeleteAccount(string text)
        {
            return text+ "لايمكنك حذف الحساب لأنه مرتبط ";
        }
        public static List<string> paymentTypes = new List<string> { "نقد", "اجل" };
        public static List<string> orderTypes = new List<string> { "محلي", "سفري", "سفري معنا" };
        public static List<string> priceTypes = new List<string> { "تجزئه", "جمله" };
        //public static List<AppColumn> ColumnsInvoceDetailTable
        //{
        //    get
        //    {
        //        int height = 40;
        //        return new List<AppColumn>()
        //     {
        //  new AppColumn(){caption="رقم الصنف",ValueType=typeof(int),ReadOnly=true ,SizeF=new System.Drawing.SizeF(0.1f,height),flex=true},
        // new AppColumn(){caption="الصنف",Type=typeof(Guna2ComboBox),CombBox=new AppTableCombBox(){DisplayMember="nameAr",DataSource=controller.copySupItems ,eventHandler=item_Selection},ValueType=typeof(string), SizeF = new System.Drawing.SizeF(0.15f, height),flex=true},
        // new AppColumn(){caption="الوحده",Type=typeof(Guna2ComboBox),CombBox=new AppTableCombBox(){DataSource=controller.copyUnits},ValueType=typeof(string), SizeF = new System.Drawing.SizeF(.11f, height), flex = true},
        // new AppColumn(){caption="الكميه",ValueType=typeof(decimal),DefaultValue=1,AutoFocus=true ,SizeF = new System.Drawing.SizeF(.1f, height),flex=true},
        // new AppColumn(){caption="سعر الوحده",ValueType=typeof(decimal),SizeF=new System.Drawing.SizeF(.1f,height),flex=true},
        // new AppColumn(){caption="الإجمالي",ValueType=typeof(decimal),ReadOnly=true,SizeF=new System.Drawing.SizeF(0.14f,height), flex = true},
        // new AppColumn(){caption="ملاحضات",ValueType=typeof(string),SizeF=new System.Drawing.SizeF(.14f,height),flex=true},
        //    };
        //    }
        //}
    }
    public enum TransactionType
    {
        سند_قبض,
        سند_صرف,
        قيد_بسيط,
        قيد_مركب,
        رصيد_إفتتاحي,
        بيع_و_شراء_العملات,
        فاتورة_مبيعات,
        فاتورة_مشتريات,
        مرتجع_مشتريات,
        مرتجع_مبيعات,
        قيد_تمتيك,
        مخزون_اول_فتره,
        تحويل_مخزني

    }
    public enum InvoiceType
    {
        مبيعات,
        مشتريات,
        مرتجع_مشتريات,
        مرتجع_مبيعات,
        تحويل_صادر,
        تحويل_وارد
    }
    public enum AccountLocations
    {
        الكل,
        المخازن,
        الصناديق,
        الموظفين,
        الموردين,
        العملاء,

    }
    public enum MeasurementsItemType
    {
        مركب,
        بسيط
    }
    public enum PriceType
    {
        تجزئه,
        جمله
    }
    public enum PaymentType
    {
        نقد,
        اجل
    }


        public class MyData : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            private string _myProperty;
            public bool accreditation = false;
            public string MyProperty
            {
                get { return _myProperty; }
                set
                {
                    if (_myProperty != value)
                    {
                        _myProperty = value;
                        OnPropertyChanged(nameof(MyProperty));
                    }
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
  
    }
}
