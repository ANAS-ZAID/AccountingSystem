using Guna.UI2.WinForms;
using Krypton.Toolkit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media.Media3D;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.core.CustomControl
{
  static class ToolTable
    {
        //static public Control CellSister(this Control control, string rowKey)
        //{

        //}
    }
    public partial class CustomTable : FlowLayoutPanel
    {
        static public string RowId { get => Id++.ToString(); }
        static public int Id = 0;
         public int childrenWidth { get => Width - ((int)(Width * .01)); }

        public AppTableStyle style { get; set; }
        public BindingSource bindingSource;
        public TextBox TextBoxTotal;
        public string Total { get {  return _newData. Total; } }
        AppTable data { get; set; }
        AppTable _newData { get; set; }
       public AppTable newData { get => _newData; }
        public Control CellSister(Control control, string columnKey)
        {
            Control cell = null;
            try {
                if (control?.Parent?.Parent == null) return null;
                string rowKey = control.Parent.Parent.Name;
                Control row = control.Parent.Parent;//Controls[rowKey];
                 if (row == null) return null;   
                var column = _newData[columnKey];
                if (column == null) return null;
                int columnIndex = int.Parse(column.index) + 1;
                
                if (columnIndex < 0 || columnIndex >= row.Controls.Count) return null;

                if (row.Controls[columnIndex].Controls.Count > 1)
                    cell = row.Controls[columnIndex].Controls[1];
                else if (row.Controls[columnIndex].Controls.Count > 0)
                    cell = row.Controls[columnIndex].Controls[0];
            }
            catch { }


            return cell;
        }
        // public Control CellSister(Control control, string columnKey)
        //{
        //    Control cell = null;
        //    try {
        //        if (control?.Parent?.Parent == null) return null;
        //        string rowKey = control.Parent.Parent.Name;
        //        Control row = Controls[rowKey];
        //         if(row == null) return null;   
        //        var column = _newData[columnKey];
        //        if (column == null) return null;
        //        int columnIndex = int.Parse(column.Key) + 1;
                
        //        if (columnIndex < 0 || columnIndex >= row.Controls.Count) return null;

        //        if (row.Controls[columnIndex].Controls.Count > 1)
        //            cell = row.Controls[columnIndex].Controls[1];
        //        else if (row.Controls[columnIndex].Controls.Count > 0)
        //            cell = row.Controls[columnIndex].Controls[0];
        //    }
        //    catch { }


        //    return cell;
        //}
        static void a() { }
        public CustomTable()
        {
            InitializeComponent();
            SizeChanged += Table_SizeChanged;
            _newData = new AppTable();
            TextBoxTotal=new TextBox() { Visible=false,};
           
        }
        
    private void Parent_SizeChanged(object sender, EventArgs e)
        {

            Control control = sender as Control;
            int childeHeight = 0;
            int index = -1;
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control item = control.Controls[i];
                if (InvokeRequired)
                    Invoke(new Action(() => item.Width = control.Width - 20));
                else item.Width = control.Width - 20;
                if (item.Name != Name)
                    childeHeight += item.Height;

                else
                    index = i;
            }
            if (index >= 0)
                if (InvokeRequired)
                    Invoke(new Action(() => Height = control.Height - childeHeight - 20));
                else
                    Height = control.Height - childeHeight - 20;

        }
       
        public void build(BindingSource bindingSource, AppTableStyle style)
        {
            //BackColor = Color.Red;
            this.style = style;
           
            this.bindingSource = bindingSource;
          
            //bindingSource.DataSourceChanged += bindingSource_BindingContextChanged;
            this.bindingSource = bindingSource;
         
               FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            AutoScroll = true;
            Dock = DockStyle.Fill;
            //table.BackColor = SystemColors.ControlDarkDark;
            //Thread threadHeader = new Thread(buildHeader);
            //threadHeader.Start();
            data = bindingSource.DataSource as AppTable;
            _newData = new AppTable() { Columns = data.Columns, Rows = new List<AppRow>() };
            buildHeader();
            buildRows();
            if (style.flex)
            {
                //this.SizeChanged += Table_SizeChanged;
                Parent.SizeChanged += Parent_SizeChanged;
                Parent_SizeChanged(Parent, null);
            }
          
        }

        private void Table_SizeChanged(object sender, EventArgs e)
        {
            //AppDialogAleart.showAleartNoPermissions("t");
            for (int i = 0; i < Controls.Count; i++)
            {
                if (InvokeRequired)
                    Invoke(new Action(() => Controls[i].Width = childrenWidth));
                else Controls[i].Width = childrenWidth;


            }
        }

        private void buildHeader()
        {
            Control cellHeader = BuildControls.buildHeaderTable(style.HeaderStyle, data.Columns, style.BtnsTable);
            cellHeader.Width = childrenWidth;
            if (InvokeRequired)
                Invoke(new Action(() => Controls.Add(cellHeader)));
            else Controls.Add(cellHeader);
            if (style.flex)
            {
               
                cellHeader.SizeChanged += Row_SizeChanged;
                Row_SizeChanged(cellHeader, null);
            }
        }
        private void buildRows()
        {
            if (InvokeRequired)
                Invoke(new Action(() => {
                    SuspendLayout();
                    if (Controls.Count > 1)
                        Controls.Clear();
                }));
            else if (Controls.Count > 1)
                Controls.Clear();
            foreach (AppRow row in data.Rows)
                buildRow(row);
            if (InvokeRequired)
                Invoke(new Action(() => ResumeLayout())); showOrHideAddBtn();
        }



       public void buildRow(AppRow row)
        {
               
            Guna2Button addBtn = BuildControls.buildButton("AddButton", "AddButton", new Point(0, 12), Properties.Resources.MaterialSymbolsAddCircleOutlineRounded__1_, Properties.Resources.MaterialSymbolsAddCircleOutlineRounded, AddButton_Click);
            Guna2Button deleteBtn = BuildControls.buildButton("deleteBtn", "deleteBtn", new Point(0, 12), Properties.Resources.MaterialSymbolsCancelOutlineRounded, Properties.Resources.MaterialSymbolsCancelOutlineRounded__1_, deleteButtonClick);
            addBtn.Visible = false;
            deleteBtn.Visible = style.BtnsTable.DeleteBtn.Show;
            FlowLayoutPanel newPanel = new FlowLayoutPanel();
            newPanel.Name = RowId;
            row.Key = newPanel.Name;
            _newData.AddRow(row);
            int hieght = style.RowStyle.Size.Height;
            int width = childrenWidth;
            hieght += (int)(hieght * 0.3);
            newPanel.Size = new Size(width, hieght);
            newPanel.BackColor = Color.Transparent;
            newPanel.WrapContents = false;
            newPanel.FlowDirection = FlowDirection.LeftToRight;
            newPanel.Controls.Add(deleteBtn);
            for (global::System.Int32 i = 1; i < data.Columns.Count+1; i++)
            {
                var column = data.Columns[i-1];
            
                int index = i - 1;
                _newData.Columns[index].index = index.ToString();
                var cell = row.Cells[index];
                Control field = new Control();

                if (column.IsTextBox)
                {
                    field = BuildControls.buildTextBox(cell.caption + column.caption, cell.id, column.Size, new Point(0, 0), column.ReadOnly);
                  
                    field.TextChanged += Cell_TextChanged;
                    field.TextChanged += column.CombBox?.eventHandler;
                }
                else if (column.IsComboBox)
                {

                    KryptonComboBox comboBox = BuildControls.buildComboBox(cell.caption + column.caption, cell.id, column.Size, new Point(0, 0), cell.CombBox.DataSource, 10F, item_SelectionChangeCommitted, column.CombBox.DisplayMember, true);
                    //comboBox.SelectedIndexChanged += column.CombBox.eventHandler;
                    comboBox.SelectedValueChanged += column.CombBox.eventHandler;
                    //comboBox.SelectionChangeCommitted += column.CombBox.eventHandler;
                    field = comboBox;
                }
              
                Panel panel = new Panel() { Name= index.ToString(),Size=new Size(column.Size.Width, style.RowStyle.Size.Height) };
              
                panel.Controls.Add(field);
                newPanel.Controls.Add(panel);
                if (column.IsVString)
                    field.TextOnly(column.CombBox.DisplayMember);

                else if (column.IsVDecimal)
                    field.PriceOnly();
                else if (column.IsVInt)
                    field.NumberOnly();
                if (column.IsTextBox)
                    field.Text = row.Cells[index].value?.ToString() ?? column.DefaultValue?.ToString();
                else
                    ((KryptonComboBox)field).SelectedItem = column.CombBox.SelectedItem;
                if (column.AutoFocus)
                {
                    
                    field.Focus();field.Select();
                }
                

            }
            
            newPanel.Controls.Add(addBtn);
            if (style.flex)
            {
                newPanel.SizeChanged += Row_SizeChanged;
               
            }
           
            if (InvokeRequired)
                Invoke((Action)(() => { Controls.Add(newPanel); Row_SizeChanged(newPanel, null); }));
            else
            { Controls.Add(newPanel); Row_SizeChanged(newPanel, null); }
            showOrHideAddBtn();
           //Refresh();
        }
        public bool IsFieldInOperation(Control control)
        {
            return data.IsFieldInOperation(control.Parent?.Name);
        }
            private void Cell_TextChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;

            var cell = Cell(control);
            if(cell != null)
                cell.value = control.Text;

            //AppDialogAleart.showAleartNoPermissions(data[control.Parent.Name].caption+"="+ IsFieldInOperation(control) );
            TotalCalculation(control);
          
        }

        void TotalCalculation(Control control)
        {
           if (IsFieldInOperation(control))
            {
                data.operationFields.values = new List<decimal>();
                bool foundItem= false;
                var item = CellSister(control, "الصنف");
                if (item != null)
                {
                    KryptonComboBox i = (KryptonComboBox)item;
                    if (i.SelectedItem != null)
                    {
                        Classify ite = (Classify)i.SelectedItem;
                        if (ite.id != 0)
                            foreach (var filed in data.operationFields.fields)
                                data.operationFields.values.Add(decimalValueCellSister(control, filed));
                        else
                            foundItem = true;

                    }
                }
                else
                {
                    foreach (var filed in data.operationFields.fields)
                        data.operationFields.values.Add(decimalValueCellSister(control, filed));
                }

                var filedTotal= CellSister(control, data.operationFields.keyFieldTotal);
                if (filedTotal!=null)
                    //filedTotal.Text= data.operationFields.StrValue;
                    filedTotal.Text=foundItem?"0": data.operationFields.StrValue;
                TextBoxTotal.Text = Total.ToDecimal().ToString();
                //AppDialogAleart.showAleartNoPermissions(Total);
            }
        }
       
        public decimal decimalValueCellSister(Control control, string columnKey)
        {
            decimal value = 0;
            var cell = CellSister(control, columnKey);
            if (cell != null)
                value= cell.Text.ToDecimal();
            return value;
        }
            public AppCell Cell(Control field)
        {
            AppCell cell= null;
            if (field.Parent != null)
            {
                Control parent = (Control)field.Parent;
                //AppDialogAleart.showAleartNoPermissions("parent=" + parent.Name);
                string columnId = parent.Name;
                string rowId = parent.Parent.Name;
                 cell = _newData[rowId, columnId];
            }
            return cell;
            }
        void item_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            var cell = Cell(comboBox);
            if (cell != null)
            {
                cell.CombBox.SelectedItem = comboBox.SelectedItem;
            }

        }
        void unit_SelectionChangeCommitted(object sender, EventArgs e)
        {
            KryptonComboBox comboBox = (KryptonComboBox)sender;
            Control pearant = (Control)comboBox.Parent;

            int numberPerant = int.Parse(pearant.Name);
            Unit unit = (Unit)comboBox.SelectedItem;

        }
        private void Row_SizeChanged(object sender, EventArgs e)
        {
            Control row= sender as Control;

            //row.BackColor = Color.Red;
            //AppDialogAleart.showAleartNoPermissions(row.Name);
            int width=row. Width;
            if (InvokeRequired)
                Invoke(new Action(() =>
                {
                    row.Controls[0].Width = (int)(style.BtnsTable.AddBtn.Style.SizeF.Width * width);
                    row.Controls[data.Columns.Count+1 ].Width = (int)(style.BtnsTable.DeleteBtn.Style.SizeF.Width * width);
                }));
            else {
                row.Controls[0].Width = (int)(style.BtnsTable.AddBtn.Style.SizeF.Width * width);
                row.Controls[data.Columns.Count+1 ].Width = (int)(style.BtnsTable.DeleteBtn.Style.SizeF.Width * width);
            }
            for (int i = 1; i <= data.Columns.Count; i++)
            {
                var column= data.Columns[i-1];
                if(column.flex)
                {
                    if(InvokeRequired)
                    Invoke(new Action(() =>
                    {
                        if ((row.Controls[i] is Label))
                            row.Controls[i].Width = (int)(column.SizeF.Width * width);
                        else
                            row.Controls[i].Controls[1].Width = (int)(column.SizeF.Width * width);
                    }));
                    else
                    {
                        if ( (row.Controls[i] is Label))
                            row.Controls[i].Width = (int)(column.SizeF.Width * width);
                        else
                        {
                            row.Controls[i].Width = (int)(column.SizeF.Width * width);
                            //AppDialogAleart.showAleartNoPermissions(row.Controls[i].Controls[1].Name+";t=" + row.Controls[i].Controls[1].GetType());
                            row.Controls[i].Controls[1].Width = (int)(column.SizeF.Width * width);
                        }
                    }
                }
            }
        }

        public void showOrHideAddBtn()
        {
            int countRecords = Controls.Count;
            if (countRecords > 1&& style.BtnsTable.AddBtn.Show) Invoke(new Action(() => Controls[1].Controls[Controls[0].Controls.Count - 1].Visible = true));
          
        }
        private void deleteButtonClick(object sender, EventArgs e)
        {
            Control row =((Control)sender).Parent;
            //row.Controls[6].Text = "0";
            //controller.removeSelectedDetailAt(int.Parse(row.Name));
                 Controls.RemoveByKey(row.Name);
                    RemoveRowByKey(row.Name);

            //sortRecordeTable();
            //totalCalculation();

        }

        private void RemoveRowByKey(string key)
        {
            //var row= _newData.Rows.FirstOrDefault(x => x.id == key.ToString() || x.Key == key.ToString());
            var row= _newData.Rows.FirstOrDefault(x =>  x.Key == key.ToString());
            bool s=  _newData.Rows.Remove(row);
            TextBoxTotal.Text = Total.ToDecimal().ToString();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddNewRowToTable();
            showOrHideAddBtn();

        }

        public void AddNewRowToTable()
        {
            AppRow row = new AppRow();
            foreach (var column in data.Columns)
            {
                row.Add(new AppCell() { CombBox = new AppTableCombBox() { DataSource= column.CombBox?.DataSource },value=column.DefaultValue?.ToString() });
            }
            buildRow(row);
        }

        public AppColumn[] dataColumns()
        {
            int countRows = Controls.Count;
            int countColumns = data.Columns.Count;
            AppColumn[] valueColumns = new AppColumn[data.Columns.Count];
            data.Columns.CopyTo(valueColumns, 0);
            //foreach (var column in columns)
            //    valueColumns.Add(new AppColumn() { id=column.id,name=column.,caption= column.caption });



            for (global::System.Int32 i = 1; i < countRows; i++)
            {
                var row = Controls[i];
                for (global::System.Int32 j = 1; j < countColumns-1; j++)
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
            for (int i = 1; i < Controls.Count; i++)
            {
                var row = Controls[i];
                var dataRow = new Dictionary<string, string>() { };
                for (global::System.Int32 j = 1; j < data.Columns.Count-1; j++)
                {
                    var field = row.Controls[j].Controls[1];
                    dataRow.Add(data.Columns[j].caption, field.Text);

                }
                dataRows.Add(dataRow);
            }

            return dataRows;
        }
        public void Clear()
        {
            SuspendLayout();
            //for (int i = 0; i < Controls.Count; i++)
            //{
            //    Controls.RemoveAt(1);
            //}
            Controls.Clear();
            buildHeader();
            data.Clear();
            newData.Clear();
            AddNewRowToTable();
            ResumeLayout();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
