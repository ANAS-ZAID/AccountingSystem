using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using AccountingSystem.core.Functions;

namespace AccountingSystem.core.shared
{

    public class AppStyle
    {
        public Color BackColor { get; set; }
        public Color ForColor { get; set; }
        public Size Size { get; set; }
        public SizeF SizeF { get; set; }

    }
    public class BtnTable
    {
        public AppStyle Style = new AppStyle() { SizeF = new SizeF(.05f, .05F),Size = new Size(50, 40), BackColor = AppColor.primary, ForColor = AppColor.third };
         public bool Show=false;
    }
    public class BtnsTable
    {
        public BtnTable AddBtn = new BtnTable();
        public BtnTable DeleteBtn = new BtnTable();
    }
    public class AppTableStyle
    {
        public AppStyle HeaderStyle =new AppStyle() { BackColor=AppColor.primary,ForColor = AppColor.third ,Size=new Size(50,40)};
        public AppStyle RowStyle= new AppStyle() { BackColor = AppColor.third,ForColor = AppColor.primary ,Size = new Size(50, 40)};
        public BtnsTable BtnsTable = new BtnsTable();
        public bool flex = false;
        //public int HeaderHeight {  get; set; }
        //public int RowHeight {  get; set; }
    }
    public class AppCell
                {
                    public string id { get; set; }
                    //public string _id { get => id.ToString(); }
                    public string index { get; set; }
                    public string RowIndex { get; set; }
                    public string ColumnIndex { get; set; }
                    public string caption { get; set; }
                    public string name { get; set; }
                    public string value { get; set; }
                   public  AppTableCombBox CombBox = new AppTableCombBox();
       
        //   public dynamic value { get; set; }


    }
    public class AppTableCombBox
    {
        public string DisplayMember = "name";
        public object DataSource =null;
        public object SelectedItem = null;
        public object Tag = null;
        public EventHandler eventHandler =null;

    }
    public class AppTableTextBox
    {
        public string DisplayMember = "name";
        public object DataSource = null;
        public object SelectedItem = null;
        public object Tag = null;
        public EventHandler SelectedItemChanged = null;

    }
    public class AppColumn : AppCell
    {
        [DefaultValue(typeof(Guna2TextBox))]
        public Type Type { get; set; }
        public Type ValueType { get; set; }
        public object DefaultValue { get; set; }

         
        public bool ReadOnly { get; set; }
        public Size Size = new Size(50, 35);
        public SizeF SizeF = new SizeF(0.1f, 1f);
        public bool flex = true;
        public bool AutoFocus = false;
        public List<AppCell> Cells { get; set; }
        public bool IsComboBox { get => (Type == typeof(Guna2ComboBox)); }
        public bool IsTextBox { get => (Type == typeof(Guna2TextBox) || Type == null); }
        public bool IsVString { get => (ValueType == typeof(string)); }
        public bool IsVInt { get => (ValueType == typeof(int) ); }
        public bool IsVDecimal { get => (ValueType == typeof(decimal) ); }
        public AppColumn()
                {
                    Cells = new List<AppCell>();
           
                }
        public void Add(AppCell cell)
        { 
            Cells.Add(cell);
        }
        //public AppColumn this[string key]
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(key))
        //            r
        //        return 
        //    }
        //    set
        //    {

        //    }
        //}
    }
                public class AppRow
                {
                    public int? id;

                    public string Key { get; set; }
                    static int _newCellId = 0;
                  public  static string newCellId { get { return _newCellId++.ToString(); } }
                    public List<AppCell> Cells { get; set; }
        public void Add(AppCell cell)
        {
            cell.index=newCellId;
            cell.RowIndex=Key;
            Cells.Add(cell);
        }
        public AppRow() { 

        Cells = new List<AppCell>();

        }
                }
  public  enum Operation { Add, Subtract, Multiply, Divide }
  public  enum Condition { NotNullAndZero}
    public class AppTableOperationField
    {
       public Operation operation=Operation.Multiply;
       public Condition condition = Condition.NotNullAndZero;
         List<string> _fields = new List<string>();
        public List<string> fields { get => _fields; set => _fields = value; }
        public string keyFieldTotal = "الإجمالي";
       public List<decimal> values = new List<decimal>();
        public bool IsFieldInOperation(string fieldKey)=>fields.Contains(fieldKey);
        public decimal value { get
            {
                decimal value = 1;
                switch (operation) 
                {
                    case Operation.Add:
                        break; case Operation.Subtract:break;
                    case Operation.Multiply:

                        foreach (var val in values)
                        {
                            value *= val;
                        }
                        break;
                }
                return value;
            } }
        public string StrValue
        {
            get
            {
                return value.Format();
            }
        }
        public List<string> this[string []fields] { get {

                return _fields; }set 
            {
                  _fields = value;
            } }
    }
                public class AppTable
                {
                    static public int countRow = 0;
                    static  int _newRowIndex = 0;
                    AppTableOperationField _operationFields = new AppTableOperationField();
                   public AppTableOperationField operationFields 
                            {
            get { return _operationFields; }
            set
            {
                if (value != null)
                {   if( value.fields.Any())
                    {
                      
                        foreach (var field in value.fields)
                        {
                           if(!IsFieldInTotal(field))
                                return;
                        }
                        //string fieldTo = "";
                        if (!IsFieldInTotal(value.keyFieldTotal))
                            return;
                        _operationFields = value;
                    }
              
                }
                
            }
   }
        public bool IsFieldInTotal(string field)
        {
            var ColumnsName = Columns.Select(x => x.name).ToList();
            var ColumnsId = Columns.Select(x => x.id).ToList();
            var ColumnsIndex = Columns.Select(x => x.index).ToList();
            var ColumnsCaption = Columns.Select(x => x.caption).ToList();
            return  (ColumnsName.Contains(field) || ColumnsIndex.Contains(field) || ColumnsId.Contains(field)|| ColumnsCaption.Contains(field));
        }
        public bool IsFieldInOperation(string field)
        {
            var column = this[field];
            return operationFields.IsFieldInOperation(column.caption)|| operationFields.IsFieldInOperation(column.name)|| operationFields.IsFieldInOperation(column.id);
        }

        public string Total {  set { } 
            get {
                var column = operationFields.keyFieldTotal;
                decimal value = 0;
                if (column != null)
                foreach (var row in Rows)
                {
                   var cell = this[row.Key, column];
                        value += cell?.value.ToDecimal()??0;
                        //AppDialogAleart.showAleartNoPermissions(column+"="+(cell?.value.ToDecimal() ?? 0).ToString());
                }
                return value.Format();
            } }
                    public string TotalColumns {  set { } }
                    static  string newRowIndex {get { return _newRowIndex++.ToString(); } }
                    public List<AppRow> Rows { get; set; }
                      List<AppColumn> _Columns {  get; set; }
                    public List<AppColumn> Columns { get => _Columns; set {
                int index = 0;
                foreach (var item in value)
                {
                    item.index = index++.ToString();
                }
                _Columns = value;
            } }
                     public void AddRow(AppRow row) 
                       {
           
                        //row.Key=newRowIndex;
                        Rows.Add(row);
                        }
                     public void AddColumn(AppColumn column)=>Columns.Add(column);
        public AppColumn this[string key]
        {
            get
            {
                if (String.IsNullOrEmpty(key))
                    return null;
                AppColumn column = null;
                int ColumnIndex = 0;

                if (!int.TryParse(key, out ColumnIndex))
                {
                    column = Columns.FirstOrDefault(x => x.id==key || x.name == key || x.caption == key || x.index == key);
                    if (column == null) return null;
                    ColumnIndex = Columns.IndexOf(column);
                }
                if (ColumnIndex < 0 || ColumnIndex >= Columns.Count) return null;
                column= Columns[ColumnIndex];
                column.Cells=new List<AppCell>();
                for (int i =0; i < Rows.Count; i++)
                {
                    var cell = Rows[i].Cells[ColumnIndex];
                    column.Add(cell);//new AppCell() { value =cell.value}
                }
                return column;
            }
            set
            {

            }
        }
        public virtual AppRow this[int key]
        {
            get
            {
                if (key < 0 ) return null;
                //int _RowIndex = 0;
                    AppRow row = null;
                    //row = Rows.FirstOrDefault(x => x.id == key.ToString() || x.Key == key.ToString());
                    row = Rows.FirstOrDefault(x =>  x.Key == key.ToString());
                    if (row == null) return null;
                //    _RowIndex = Rows.IndexOf(row);
                //if (_RowIndex < 0 || _RowIndex >= Rows.Count) return null;
                //row = Rows[_RowIndex];
                return row;
            }set { }
        }
       
        public AppCell this[string RowKey, string ColumnId]
        {
            get =>Cell(RowKey, ColumnId);
            set
            {
                var cell = Cell(RowKey, ColumnId);
                if(value != null)
                    this[RowKey, ColumnId].value = value.value;
            }
        }
        //public AppCell this[string RowKey, string ColumnName, string v]
        //{
        //    get => Cell(RowKey, ColumnName);
        //    set
        //    {
        //        var cell = Cell(RowKey, ColumnName);
        //        if (value != null)
        //            this[RowKey, ColumnName].value = v;
        //    }

        //}
        AppCell Cell(string RowKey, string ColumnId)
        {
            if (String.IsNullOrEmpty(RowKey) || String.IsNullOrEmpty(ColumnId))
                return null;
            //int _RowIndex = -1;
            AppRow row = null;
            //if (!int.TryParse(RowKey, out _RowIndex))
            //{
                //row = Rows.FirstOrDefault(x => x.id == RowKey || x.Key == RowKey);
                row = Rows.FirstOrDefault(x =>  x.Key == RowKey);
                if (row == null) return null;
            //_RowIndex = Rows.IndexOf(row);
            //}

            //if (_RowIndex < 0) return null;
            //row = Rows[_RowIndex];

            int ColumnIndex = 0;

            if (!int.TryParse(ColumnId, out ColumnIndex))
            {
                var column = Columns.FirstOrDefault(x => x.name == ColumnId || x.caption == ColumnId || x.index == ColumnId);
                if (column == null) return null;
                ColumnIndex = Columns.IndexOf(column);
            }

            if (ColumnIndex < 0 || ColumnIndex >= Columns.Count) return null;
            return row.Cells[ColumnIndex];
        }

        internal void Clear()
        {
            Rows.Clear();
        }

        public AppCell this[int RowIndex, string ColumnName]
        {
            get
            {

                if (String.IsNullOrEmpty(ColumnName))
                    return null;

                if (RowIndex < 0 || RowIndex >= Rows.Count) return null;
                int ColumnIndex = 0;
                if (!int.TryParse(ColumnName, out ColumnIndex))
                {
                    var column = Columns.FirstOrDefault(x => x.name == ColumnName || x.caption == ColumnName || x.id == ColumnName || x.index == ColumnName);
                    if (column == null) return null;
                    ColumnIndex = Columns.IndexOf(column);
                }
                var row = Rows[RowIndex];
                if (ColumnIndex < 0 || ColumnIndex >= Columns.Count || ColumnIndex >= row.Cells.Count) return null;

                return row.Cells[ColumnIndex];

            }

        }
        //public BindingSource bindingSource;

    }
                public class AppTableGUI
                {
                     static public int countRow = 0;
                    public AppTableStyle style { get; set; }
                    public BindingSource bindingSource;

                    AppTable data { get; set; }
                    //public List<AppColumn> columns { get; set; }


                    public FlowLayoutPanel table { get; set; }
                    //FlowLayoutPanel table { get { return table; } set { table = value; } }
                    public AppTableGUI(BindingSource bindingSource,AppTableStyle style)
                    {
                        this.style = style;
                        //bindingSource.DataSourceChanged += bindingSource_BindingContextChanged;
                        this.bindingSource = bindingSource;
                        table = new FlowLayoutPanel();
                        table.FlowDirection = FlowDirection.TopDown;
                        table.WrapContents = false;
                        table.AutoScroll = true;
                        table.Dock = DockStyle.Fill;
                        //table.BackColor = SystemColors.ControlDarkDark;
                        Thread threadHeader = new Thread(buildHeader);
                        threadHeader.Start();
                        data = bindingSource.DataSource as AppTable;
                        buildRows();
                    }
            //        protected void bindingSource_BindingContextChanged(object sender, EventArgs e)
            //        {

            //            data =bindingSource.DataSource as AppTable;
            ////AppDialogAleart.showAleartNoPermissions("data" + data.Rows.Count.ToString());

            //////AppDialogAleart.showAleartNoPermissions(data.Rows.Count.ToString());
            //        Thread thread = new Thread(buildRows);
            //            thread.IsBackground = true;
            //            thread.Start();
            //        }
                    private void buildHeader()
                    {
                        Control cellHeader = BuildControls.buildHeaderTable(style.HeaderStyle, data.Columns);
                        if (table.InvokeRequired)
                            table.Invoke(new Action(() => table.Controls.Add(cellHeader)));
                        else table.Controls.Add(cellHeader);
                    }
                    private void buildRows()
                    {
                        if (table.InvokeRequired)
                            table.Invoke(new Action(() => {
                                table.SuspendLayout();
                                if (table.Controls.Count > 1)
                                    table.Controls.Clear();}));
                        else if (table.Controls.Count > 1)
                                 table.Controls.Clear();
                        foreach (AppRow row in data.Rows)
                                        buildeRow(row);
                      if (table.InvokeRequired)
                         table.Invoke(new Action(() => table.ResumeLayout()));
                              }



                    void buildeRow(AppRow row)
                    {
                        FlowLayoutPanel newPanel = new FlowLayoutPanel();
                        newPanel.Name = (countRow++).ToString();
                       int hieght =style.RowStyle.Size.Height;
                        hieght += (int)(hieght * 0.3);
                        newPanel.Size = new Size(data.Columns.Sum(x => x.Size.Width) + ((int)(data.Columns.Sum(x => x.Size.Width) * .1)), hieght);
                        newPanel.BackColor = Color.Transparent;
                        newPanel.WrapContents = false;

                        for (global::System.Int32 i = 0; i < data.Columns.Count; i++)
                        {
                            var column = data.Columns[i];
                            Control cell = new Control();

                            if (column.IsTextBox)
                            {
                                cell = BuildControls.buildTextBox(row.Cells[i].caption+column.caption, row.Cells[i].id, column.Size, new Point(0, 0), column.ReadOnly);
                                
                                Panel panel = new Panel();
                                panel.Width = column.Size.Width;
                                panel.Height = style.RowStyle.Size.Height;
                                panel.Controls.Add(cell);
                                newPanel.Controls.Add(panel);
                                if (column.IsVString)
                                      cell.TextOnly(); 
                                else if (column.IsVDecimal)
                                    cell.PriceOnly();
                                else if (column.IsVInt)
                                    cell.NumberOnly();

                              cell.Text = row.Cells[i].value?.ToString() ?? null;
                             }


                        }
                        if (table.InvokeRequired)
                            table.Invoke(new Action(() => table.Controls.Add(newPanel)));
                        else table.Controls.Add(newPanel);

                    }
              
                    public AppColumn[] dataColumns()
                    {
                        int countRows = table.Controls.Count;
                        int countColumns = data.Columns.Count;
                        AppColumn[] valueColumns = new AppColumn[data.Columns.Count];
                        data.Columns.CopyTo(valueColumns, 0);
                        //foreach (var column in columns)
                        //    valueColumns.Add(new AppColumn() { id=column.id,name=column.,caption= column.caption });



                        for (global::System.Int32 i = 1; i < countRows; i++)
                        {
                            var row = table.Controls[i];
                            for (global::System.Int32 j = 0; j < countColumns; j++)
                            {
                                var field = row.Controls[j].Controls[1];
                                //var fieldValue = 
                                valueColumns[j].Cells.Add(new AppCell() { id = field.Name, value = field.Text });
                                //fieldValue.Value[i-1] = field.Text;
                            }
                        }
                        return valueColumns;

                    }
                    public List<Dictionary<string, string>> dataRows()
                    {

                        List<Dictionary<string, string>> dataRows = new List<Dictionary<string, string>>();
                        for (int i = 1; i < table.Controls.Count; i++)
                        {
                            var row = table.Controls[i];
                            var dataRow = new Dictionary<string, string>() { };
                            for (global::System.Int32 j = 0; j < data.Columns.Count; j++)
                            {
                                var field = row.Controls[j].Controls[1];
                                dataRow.Add(data.Columns[j].caption, field.Text);

                            }
                            dataRows.Add(dataRow);
                        }

                        return dataRows;
                    }


                }

            }

        
