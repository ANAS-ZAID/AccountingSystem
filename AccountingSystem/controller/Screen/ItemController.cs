using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.model;
using AccountingSystem.Model;
using System.IO;
using System.Drawing;
using System.Data.Entity.Migrations;
using AccountingSystem.view.SupScreens.AreaManagement;
using AccountingSystem.view.SupScreens.ClassifyManagament;
using Krypton.Toolkit;
using System.Drawing.Imaging;
using System.Threading;

namespace AccountingSystem.controller
{
    public class ItemController
    {
        public List<string> columnsNamesInAR = new List<string> { "الرقم", "أسم  الصنف ", "الأسم الإنجليزي", " الصنف الأب", "سعر البيع", "سعر الشراء", "رقم الصنف", "رقم الباركود" ,"النوع","الصوره"};//, "تأريخ التعديل"
        public BindingSource dataSource;
        DataTable dataTable;
        public dynamic allData;
        AccountingDbContext dBContext;

        public List<Classify> supItms { get {
                
                
                return dBContext.Classifies.Where(a => a.type == "فرعي"  ).Include(i => i.TypesClassify).Include(i => i.ClassifyGroup).Include(i => i.Company).Include(i => i.Childrens).Include(i => i.perantItem).Include(i => i.MeasurementsItems).ToList(); } }
        public List<Classify> mainItms { get {
                var s=dBContext.Classifies?.Where(a => a.type == "رئيسي" && (prosessesType != null ? (prosessesType == ProsessesType.update ? a.id != temp.id : true) : true))?.Include(i => i.TypesClassify).Include(i => i.ClassifyGroup).Include(i => i.Company).Include(i => i.Childrens).Include(i => i.perantItem).Include(i => i.MeasurementsItems)?.ToList();
                if(s!=null)
                s.Insert(0, new Classify() { nameAr = "", id = 0 });
                return s; } }
        public List<ClassifyGroup> groups { get {
                var t= dBContext.ClassifyGroups.ToList();
               t. Insert(0, new ClassifyGroup() { name = "", id = 0 });
                return t;  } }
        public List<Unit> units { get { return dBContext.Units.ToList(); } }
        public List<TypesClassify> types { get { return dBContext.TypesClassifies.ToList(); } }
        public List<Company> companies { get { return dBContext.Companies.ToList(); } }
        public Classify temp;
        List<MeasurementsItem> tempMeasurements;
        /// <summary>
        /// items
        /// </summary>
        public MyData purchasePriceTotal;
        public MyData sellingPriceTotal;
       public Dictionary<int, CompositeItem> selectedCompositeItem;
        public CompositeItem tempCompositeItem;
        /// <summary>
        /// Ingredient
        /// </summary>
        /// 
     public   Dictionary<int, MeasurementsItem> selectedMeasurements;
        int numberSelectedMeasurementsItem;
       public string barcodeSelectedMeasurementsItem;
       public string nameItemSelectedMeasurementsItem;
       public string numberItemSelectedMeasurementsItem;

        /// <summary>
        /// MeasurementsItem
        /// </summary>

        public ProsessesType prosessesType { get; set; }

        public ItemController()
        {
            purchasePriceTotal = new MyData();
            sellingPriceTotal = new MyData();
            dBContext = new AccountingDbContext();
            dataSource = new BindingSource();
            temp = new Classify();
            temp.MeasurementsItems = new List<MeasurementsItem>();
            tempCompositeItem= new CompositeItem();
            lodeData();
        }
        public void clearTempDataIngredient()
        {
            purchasePriceTotal.MyProperty="";
            sellingPriceTotal.MyProperty="";
            selectedCompositeItem = new Dictionary<int, CompositeItem>();

        } 
        public void clearTempDataMeasurementsItem()
        {
            clearTempDataIngredient();
            selectedMeasurements = new Dictionary<int, MeasurementsItem>();

        }
        public void clearTempData()
        {
            temp = new Classify();
            temp.ClassifyGroup = null;
            temp .perantItem=null;
            temp.TypesClassify = null;
            temp.Childrens = null;
            temp.Company = null;
            temp.MeasurementsItems =null;
            clearTempDataMeasurementsItem();

        }
        public void lodeData()
        {
           
            clearTempData();

            try
            {

                allData = dBContext.Classifies.AsNoTracking().OrderByDescending(a => a.id).Include(i => i.TypesClassify).Include(i => i.ClassifyGroup).Include(i => i.Company).Include(i => i.Childrens).Include(i => i.perantItem).Include(i => i.MeasurementsItems).ToList()
                    .Select(e => new
                    {
                        id = e.id,
                        nameAr = e.nameAr,
                        nameEn = e.nameEn,
                        perantItem = e.perantItem?.nameAr,
                        sellingPrice =  e.MeasurementsItems?.ElementAtOrDefault(0)?.sellingPrice,
                        purchasePrice =  e.MeasurementsItems?.ElementAtOrDefault(0)?.purchasePrice,
                        itemNumber = e.ClassifyNumber,
                        barcode = e.MeasurementsItems?.ElementAtOrDefault(0)?.barcode,
                        type=e.type,

                    }).ToList();

                fillDataGridView();
            }
            catch 
            {
                AppDialogAleart.showAleartError();
            }
        }
        void fillDataGridView()
        {

             dataTable = new DataTable();
            foreach (string name in columnsNamesInAR)
            {
                dataTable.Columns.Add(name);
            }
            Thread thread = new Thread(new ThreadStart(fillDataTable));
            thread.Start();
        }

        private void fillDataTable()
        {
            foreach (var item in allData)
            {
                dataTable.Rows.Add(item.id, item.nameAr,
                item.nameEn, item.perantItem, item.sellingPrice,
                item.purchasePrice, item.itemNumber, item.barcode, item.type, null);
            }
            dataSource.DataSource = dataTable;
        }

        public bool find(int id)
        {
            bool status = true;
            try
            {
                clearTempData();
                temp = dBContext.Classifies.OrderByDescending(a => a.id).Include(x=>x.SaleDetails).Include(x => x.PurchaseDetails).Include(i => i.TypesClassify).Include(i => i.ClassifyGroup).Include(i => i.Company).Include(i => i.Childrens).Include(i => i.perantItem).Include(i => i.MeasurementsItems).FirstOrDefault(i => i.id == id);
                if (temp == null)
                    throw new Exception();
                temp.MeasurementsItems=dBContext.MeasurementsItems.Include(x=>x.Unit).Include(x=>x.CompositeItems).Where(x=>x.itemId==temp.id).ToList();              
            }
            catch
            {
                AppDialogAleart.showAleartError();
                status = false;
            }
            return status;
        }
        public void search(string nameAr,string nameEn,string itemNumber,string barcode)
        {
            if (model.LoginData.permissions["item"].viewPermission.Value)
            {
                string perant = temp.perantItem != null ? temp.perantItem.nameAr : "";
                string group = temp.ClassifyGroup != null ? temp.ClassifyGroup.name : "";
                try
                {
                    allData = dBContext.Classifies.AsNoTracking().Include(i => i.TypesClassify).Include(i => i.ClassifyGroup).Include(i => i.Company).Include(i => i.Childrens).Include(i => i.perantItem).Include(i => i.MeasurementsItems).OrderByDescending(i => i.id).
                    Where(
                            x => DbFunctions.Like(x.nameAr, "%" + nameAr + "%")
                            && DbFunctions.Like(x.nameEn, "%" + nameEn + "%")
                            && DbFunctions.Like(x.ClassifyNumber.Value.ToString(), "%" + itemNumber + "%")
                            && DbFunctions.Like(x.type, "%" + temp.type + "%")
                            && DbFunctions.Like(x.perantItem != null ? x.perantItem.nameAr : "", "%" + perant + "%")
                            && DbFunctions.Like(x.ClassifyGroup != null ? x.ClassifyGroup.name : "", "%" + group + "%")
                            && ((x.MeasurementsItems.Any() && !String.IsNullOrEmpty(barcode) ? (x.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")) != null) : String.IsNullOrEmpty(barcode)
                             ))).
                        Select(e => new
                        {
                            id = e.id,
                            nameAr = e.nameAr,
                            nameEn = e.nameEn,
                            perantItem = (e.perantItem != null ? e.perantItem.nameAr : null),
                            sellingPrice = e.MeasurementsItems.Any() ? (e.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")) != null) ? e.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")).sellingPrice : null : null,
                            purchasePrice = e.MeasurementsItems.Any() ? (e.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")) != null) ? e.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")).purchasePrice : null : null,
                            itemNumber = e.ClassifyNumber,
                            barcode = e.MeasurementsItems.Any() ? (e.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")) != null) ? e.MeasurementsItems.FirstOrDefault(m => DbFunctions.Like(m.barcode.Value.ToString(), "%" + barcode + "%")).barcode : null : null,
                            type = e.type,
                        }).ToList();

                    fillDataGridView();
                }
                catch
                {

                    AppDialogAleart.showAleartError();
                }
            }
        }

        public bool dataProcessing(string nameAr, string nameEn, string description, string itemNumber, Image image)
        {

            byte[] img = null;
            if (!ValidatingData.validatingData(nameAr, columnsNamesInAR[1]))
                return false;
            if (!ValidatingData.validatingData(itemNumber, columnsNamesInAR[6]))
                return false;
            if (temp.type == "فرعي")
            {
                if (!tempMeasurements.Any())
                {
                    AppDialogAleart.showAleartErrorData("يجب تعبئة رقم الباركود واختيار الوحدة للصنف وكتابة سعر البيع");
                    return false; 
                }
                        
            }
            if (image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
          
                image.Save(memoryStream, image.RawFormat);
                img = memoryStream.ToArray();
            }
            temp.nameAr = nameAr;
            temp.nameEn = nameEn;
            temp.description = description;
            temp.ClassifyNumber=int.Parse(itemNumber);
            temp.image = img;
          //  temp.type = type;
            if(temp.perantItem!=null)
            {temp.parentId=temp.perantItem.id;
                temp.rankk=temp.perantItem.rankk+1;
            }else
                temp.rankk = 1;
           temp.companyId=temp.Company?.id;
           temp.ClassifyGroupId=temp.ClassifyGroup?.id;
           temp.typeClassifyId=temp.TypesClassify?.id;
            if (prosessesType == ProsessesType.add)
               return add();
            if (prosessesType == ProsessesType.update)
                return update();

            return true;
        }
            public bool add()
              {

            bool status = false;
          
            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {

                    var newItem =dBContext.Classifies.FirstOrDefault(x=>x.nameAr==temp.nameAr);
                    if (newItem!=null)
                    {
                        AppDialogAleart.showAleartPreExistingData("يوجد صنف سابق بهذا الأسم");
                        return false;
                    }
                    newItem = dBContext.Classifies.FirstOrDefault(x => x.ClassifyNumber == temp.ClassifyNumber);
                    if (newItem!=null)
                    {
                        AppDialogAleart.showAleartPreExistingData("يوجد صنف سابق بهذا الرقم");
                        return false;
                    }
                   if(temp.type=="فرعي")
                        foreach (var item in tempMeasurements)
                        {
                            var any = dBContext.MeasurementsItems.FirstOrDefault(x => x.barcode == item.barcode);
                            if (any != null)
                            {
                                AppDialogAleart.showAleartPreExistingData($"الباركود: {any.barcode} موجود مسبقاً في صنف اخر يمنع تكرار الباركود");
                                return false;
                            }
                        }
                    if (temp.type == "فرعي")
                        temp.MeasurementsItems = tempMeasurements;
                    newItem = dBContext.Classifies.Add(temp);
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();

                }
                catch 
                {
                    transaction.Rollback();
                    AppDialogAleart.showAleartError();
                    status = false;
                }
            }

            return status;
        }

        public bool update()
        {

            bool status = false;

            using (var transaction = dBContext.Database.BeginTransaction())
            {

                try
                {

                    var newItem = dBContext.Classifies.FirstOrDefault(x => (x.nameAr == temp.nameAr ) && x.id!=temp.id);
                    if (newItem != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("يوجد صنف سابق بهذا الأسم");
                        return false;
                    }
                    newItem = dBContext.Classifies.FirstOrDefault(x => x.ClassifyNumber == temp.ClassifyNumber && x.id != temp.id);
                    if (newItem != null)
                    {
                        AppDialogAleart.showAleartPreExistingData("يوجد صنف سابق بهذا الرقم");
                        return false;
                    }
                    if (temp.type == "فرعي")
                    {
                        foreach (var item in tempMeasurements)
                        {
                            var any = dBContext.MeasurementsItems.FirstOrDefault(x => x.barcode == item.barcode && x.itemId != temp.id);
                            if (any != null)
                            {
                                AppDialogAleart.showAleartPreExistingData($"الباركود: {any.barcode} موجود مسبقاً في صنف اخر يمنع تكرار الباركود");
                                return false;
                            }
                        }
                        temp.MeasurementsItems.ToList().ForEach(x => {
                            var any = tempMeasurements.FirstOrDefault(a => a.id == x.id);
                            if (any == null)
                            {
                                dBContext.CompositeItems.RemoveRange(x.CompositeItems);
                                dBContext.MeasurementsItems.Remove(x);
                                dBContext.SaveChanges();
                            }
                            //else
                            //{
                            //    dBContext.CompositeItems.AddOrUpdate(x.CompositeItems.ToArray());
                            //    dBContext.SaveChanges();
                            //}
                        });
                        //foreach (var item in temp.MeasurementsItems)
                        //{
                        //dBContext.CompositeItems.AddOrUpdate(item.CompositeItems.ToArray());
                        //dBContext.SaveChanges();
                        //}
                        temp.MeasurementsItems = tempMeasurements.ToArray();
                        dBContext.MeasurementsItems.AddOrUpdate(temp.MeasurementsItems.ToArray());
                    }
                    dBContext.SaveChanges();
                    transaction.Commit();
                    status = true;
                    AppDialogAleart.showAleartSuccess();
                    lodeData();

                }
                catch 
                {

                    transaction.Rollback();
                    AppDialogAleart.showAleartError();
                    status = false;

                }
            }

            return status;
        }


        public bool delete(List<int> keys)
        {
            bool status = false;

            if (model.LoginData.permissions["item"].deletePermission.Value)
            {
                if (keys.Count > 0)
                {
                    if (AppDialogAleart.showAleartConfirmation("هل أنت متأكد انك ترغب في حذف البيانات المحدده وعددها: " + keys.Count) != DialogResult.OK)
                        return false;
                    using (var transaction = dBContext.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (var id in keys)
                            {
                                if (!find(id))
                                    throw new Exception("حدث خطأ ما في العمليه ");
                                if (temp.SaleDetails.Any() || temp.PurchaseDetails.Any())
                                {
                                    transaction.Rollback();
                                    AppDialogAleart.showAleartError("لم تتم عملية الحذف لأن الصنف مرتبط بفاتورة ");
                                    return false;
                                }
                                temp.MeasurementsItems.ToList().ForEach(x => {
                                    dBContext.CompositeItems.RemoveRange(x.CompositeItems);
                                    dBContext.MeasurementsItems.Remove(x);
                                    dBContext.SaveChanges();
                                });

                                dBContext.Classifies.Remove(temp);
                            }
                            status = true;
                            AppDialogAleart.showAleartSuccess();
                            dBContext.SaveChanges();
                            transaction.Commit();
                            lodeData();
                        }
                        catch
                        {
                            transaction.Rollback();
                            AppDialogAleart.showAleartError();

                            status = false;
                        }
                    }
                }
                else { AppDialogAleart.showAleart("للحذف",MessageType.NoDataSpecified); }
            }
            else AppDialogAleart.showAleartNoPermissions();

            return status;
        }

        public void selectedCompany(object company)
        {
            temp.Company = (Company)company ?? null;
        }
        public void selectedTypeItem(object type)
        {
            temp.TypesClassify = (TypesClassify)type ?? null;
        }
        public void selectedClassifyGroup(object classifyGroup)
        {
            var g = (ClassifyGroup)classifyGroup ?? null;
            temp.ClassifyGroup =g.id!=0?g:null ;
        }
        public string selectedPerantItem(object perantItem)
        {
            var p= (Classify)perantItem ?? null;
            temp.perantItem = p.id!=0?p:null ;
            return p.id != 0 ? AppDBFunctions.getNewItemNumByParentId(temp.perantItem.id).ToString():"";
        } 
        public void selecteCompositeItem(object value, int numberPerant)
        {
          Classify tempItem = (Classify)value ?? null;
            if (selectedCompositeItem.ContainsKey(numberPerant))
                selectedCompositeItem.Remove(numberPerant);
            tempCompositeItem = new CompositeItem() { ComponentItem = new MeasurementsItem() { sellingPrice = null, purchasePrice = null, Unit = null, item = null } };
            if (tempItem.id != 0)
            {
                if (tempItem.MeasurementsItems.Count > 1)
                {
                    DialogSelecteMeasurementsItem dialogSelecteMeasurementsItem = new DialogSelecteMeasurementsItem(tempItem.MeasurementsItems.ToList());
                    dialogSelecteMeasurementsItem.ShowDialog();
                }
                else
                {
                    DialogSelecteMeasurementsItem.selectedMeasurementsItem = tempItem.MeasurementsItems.FirstOrDefault();
                }
                tempCompositeItem = new CompositeItem() { ComponentItem = DialogSelecteMeasurementsItem.selectedMeasurementsItem };

                if (!selectedCompositeItem.ContainsKey(numberPerant))
                {
                    selectedCompositeItem.Add(numberPerant, tempCompositeItem);
                }
            }
            else
            {
               
            }
            tempCompositeItem.purchasePrice = tempCompositeItem.ComponentItem.purchasePrice;
            tempCompositeItem.sellingPrice = tempCompositeItem.ComponentItem.sellingPrice;
        }
        public ICollection<CompositeItem> compositeItemsForSelectedMeasurement()
        {
         return selectedMeasurements.ContainsKey(numberSelectedMeasurementsItem)? selectedMeasurements[numberSelectedMeasurementsItem].CompositeItems: new List<CompositeItem>();
        }
        public MeasurementsItem selectedMeasurement()
        {
         return selectedMeasurements[numberSelectedMeasurementsItem];
        }
        public void showDialogSelecteIngredient(int numberPerant,string barcode, ToolTip toolTip)
        {
            numberSelectedMeasurementsItem = numberPerant;
            barcodeSelectedMeasurementsItem = barcode;
          
            DialogSelecteIngredient dialogSelecteIngredient = new DialogSelecteIngredient(this, toolTip);
            dialogSelecteIngredient.ShowDialog();
        }
        public void slectedUnit(object value,int numberPerant)
        {
            Unit unit = (Unit)value;
            // AppDialogAleart.showAleartNoPermissions(pearant.Controls[0].Text+ pearant.Controls[1].Text+ pearant.Controls[2].Name+ pearant.Controls[3].Name);
            if (unit.id != 0)
            {
                if (selectedMeasurements.ContainsKey(numberPerant))
                {
                    selectedMeasurements[numberPerant].Unit = unit;
                    selectedMeasurements[numberPerant].UnitId = unit.id;
                }
            }
            else if (selectedMeasurements.ContainsKey(numberPerant))
            {
                selectedMeasurements[numberPerant].Unit = null;
                selectedMeasurements[numberPerant].UnitId = null;
            }
        }
        public void fillCompositeItemsForSelectedMeasurement()
        {
           // AppDialogAleart.showAleartNoPermissions(selectedMeasurements[numberSelectedMeasurementsItem].CompositeItems.Count.ToString());
            selectedMeasurements[numberSelectedMeasurementsItem].CompositeItems = selectedCompositeItem.Values.Where(x=> (x.purchasePrice > 0 || x.sellingPrice > 0) && x.quantity > 0).ToList();
            //AppDialogAleart.showAleartNoPermissions(selectedCompositeItem.Values.Count.ToString());

        }
        public void fillMeasurementForItem()
        {
            tempMeasurements = new List<MeasurementsItem>();
            foreach (var measurementsItem in selectedMeasurements)
            {
              if(measurementsItem.Value.Unit != null && measurementsItem.Value.barcode >= 0)
                {
                    tempMeasurements.Add(measurementsItem.Value);
                }

            }
        }
        public void showDialogUpdate(int id)
        {
            if (model.LoginData.permissions["item"].updatePermission.Value)
            {
                if (id != 0)
                {

                    prosessesType = ProsessesType.update;
                    find(id);
                    DialogAddAndUpdateClassify dialog = new DialogAddAndUpdateClassify(this);
                    dialog.ShowDialog();
                }
                else AppDialogAleart.showAleartError("لم تقم بتحديد أي بيانات لتعديلها");

            }
            else AppDialogAleart.showAleartNoPermissions();
        }
        public void showDialogAdd()
        {
            if (model.LoginData.permissions["item"].addPermission.Value)
            {
                clearTempData();
                prosessesType = ProsessesType.add;
                DialogAddAndUpdateClassify dialog = new DialogAddAndUpdateClassify(this);
                dialog.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();

        }
        public void showDialogView(DataGridViewRow row)
        {
            if (model.LoginData.permissions["item"].viewPermission.Value)
            {
                DialogShowDetailsRecorde dialogShow = new DialogShowDetailsRecorde(columnsNamesInAR, row);
                dialogShow.ShowDialog();
            }
            else AppDialogAleart.showAleartNoPermissions();
        }
    }
}
