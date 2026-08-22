using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.core.Functions;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.NewModel.RCLDModel;

namespace AccountingSystem.core.shared
{
    public static class ExtensionsFunctions
    {
        //Displayed
        public static string Format(this decimal? price)=> price?.ToString("F2")??"0.0"; 
        public static string Format(this decimal price)=> price.ToString("F2");
        public static string Format(this decimal price, int count)=> price.ToString($"F{count}");
        public static string Format(this DateTime dateTime)=> dateTime.ToString("yyyy-MM-dd");
        public static string Format(this DateTime? dateTime)=> dateTime?.ToString("yyyy-MM-dd");
        public static List<DataSetStocItemsLessThanZero> OrderedByQuantity(this List<DataSetStocItemsLessThanZero> items)
        {
            return items
                .OrderBy(p => p.quantity, new QuantityComparer())
                .ToList();
        }

        public class QuantityComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x < 0 && y >= 0) return -1; // x سالب و y موجب أو صفر
                if (x >= 0 && y < 0) return 1; // x موجب أو صفر و y سالب
                return x.CompareTo(y); // مقارنة قياسية للأعداد
            }
        }
        public static List<ChartOfAccount> GetAccountWithChildren(this List<ChartOfAccount> accounts,int accountId)
        {
            var account = accounts.FirstOrDefault(a => a.id == accountId);

            if (account == null)
            {
                return new List<ChartOfAccount>(); // الحساب غير موجود
            }

            return account.Recursive().ToList();
        }
        public static List<ChartOfAccount> GetAllAccountsWithChildren(this List<ChartOfAccount> accounts,int? rootAccountId = null)
        {
            // إذا لم يتم تحديد حساب جذر، فابحث عن جميع الحسابات الجذرية (أي التي ليس لها حساب أب)
            var query = rootAccountId.HasValue
                ? accounts.Where(a => a.parentId == rootAccountId)
                : accounts.Where(a => a.parentId == null);

            // استخدم توسيع طريقة Recursive() لتنفيذ البحث المتكرر
            return query.SelectMany(a => a.Recursive()).ToList();
        }

        //public static class AccountExtensions
        //{
        public static IEnumerable<ChartOfAccount> Recursive(this ChartOfAccount account)
        {
            yield return account;

            foreach (var child in account.Childrens ?? Enumerable.Empty <ChartOfAccount>())
                {
                    foreach (var descendant in child.Recursive())
                    {
                        yield return descendant;
                    }
                }
            }
        public static List<Classify> GetItemWithChildren(this List<Classify> items, int accountId)
        {
            var item = items.FirstOrDefault(a => a.id == accountId);

            if (item == null)
            {
                return new List<Classify>(); // الحساب غير موجود
            }

            return item.Recursive().ToList();
        }
        public static List<Classify> GetAllItemsWithChildren(this List<Classify> items, int? rootItemId = null)
        {
            // إذا لم يتم تحديد حساب جذر، فابحث عن جميع الحسابات الجذرية (أي التي ليس لها حساب أب)
            var query = rootItemId.HasValue
                ? items.Where(a => a.parentId == rootItemId)
                : items.Where(a => a.parentId == null);
            ////AppDialogAleart.showAleartNoPermissions(query.Count().ToString());
            //// استخدم توسيع طريقة Recursive() لتنفيذ البحث المتكرر
            //var x = ;
            //var l=new HashSet<Classify>(x);
            return query.SelectMany(a => a.Recursive()).ToList();
        }

        public static IEnumerable<Classify> Recursive(this Classify item)
        {
            yield return item;

            foreach (var child in item.Childrens ?? Enumerable.Empty<Classify>())
            {
                foreach (var descendant in child.Recursive())
                {
                    yield return descendant;
                }
            }
        }
    }
  

}


    //}

