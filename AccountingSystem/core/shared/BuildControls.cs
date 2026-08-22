using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;
//using AccountingSystem.core.shared.AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;
using AccountingSystem.Properties;

namespace AccountingSystem.core.shared
{
    internal class BuildControls
    {
        static public Label buildCellTable(string text, Size size, Color backColor, Color foreColor, string name = "cell")
        {
            Label cell = new Label();
            cell.BackColor = backColor;
            //cell.Dock = System.Windows.Forms.DockStyle.Right;
            cell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cell.Font = new System.Drawing.Font("Tahoma", 10F);
            cell.ForeColor = foreColor;
            cell.Location = new System.Drawing.Point(61, 0);
            cell.Name =name;
            cell.Size = size;
            cell.TabIndex = 18;
            cell.Text = text;
            cell.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            return cell;
        }
        static public FlowLayoutPanel buildHeaderTable(AppStyle style, List<AppColumn> columns, BtnsTable btnsTable=null ) {
            int height = ((int)(columns.FirstOrDefault()?.Size.Height ?? 0));
            if (btnsTable == null)
                btnsTable = new BtnsTable();
            int width = columns.Sum(x => x.Size.Width) + ((int)(columns.Sum(x => x.Size.Width) * .1));
            if (btnsTable.AddBtn.Show)
                width += btnsTable.AddBtn.Style.Size.Width;
            if (btnsTable.DeleteBtn.Show)
                width += btnsTable.DeleteBtn.Style.Size.Width;
         
           
            FlowLayoutPanel headerTable = new FlowLayoutPanel();
            headerTable.BackColor = style.BackColor;
            headerTable.FlowDirection=FlowDirection.LeftToRight;
            headerTable.WrapContents = false;
            headerTable.Location = new System.Drawing.Point(0, 0);
            headerTable.Name = "headerTable";
            headerTable.Size = new Size(width,height );
            headerTable.TabIndex = 84;
            Label delete = buildCellTable("حذف", btnsTable.DeleteBtn.Style.Size, btnsTable.DeleteBtn.Style.BackColor, btnsTable.DeleteBtn.Style.ForColor, "name");
             if (btnsTable.DeleteBtn.Show)
            headerTable.Controls.Add(delete);
            foreach (var column in columns)
            {
                Label label = buildCellTable(column.caption, column.Size, style.BackColor, style.ForColor, column.name);
             headerTable.Controls.Add(label);
                //label.BringToFront();
            }
            Label add = buildCellTable("إضافه", btnsTable.AddBtn.Style.Size, btnsTable.AddBtn.Style.BackColor, btnsTable.AddBtn.Style.ForColor, "name");
          if(btnsTable.AddBtn.Show)
            headerTable.Controls.Add(add);
            return headerTable;
          
        }
        static public KryptonDataGridView buildGridViewTable()
        {
            KryptonDataGridView dataGridView = new KryptonDataGridView();
            dataGridView.Visible = true;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridView.DataSourceChanged += DataGridView_DataSourceChanged;
            dataGridView.AutoSize = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView.ColumnHeadersHeight = 36;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.Height = 36;
            dataGridView.Location = new System.Drawing.Point(3, 45);
            dataGridView.Name = "kryptonDataGridView2";
            dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            dataGridView.RowHeadersVisible = false;
            //    dataGridView.RowHeadersWidth = 0;
            dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.RowTemplate.Height = 26;
            dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            // dataGridView.MaximumSize = new System.Drawing.Size(1000, 250);
            //    dataGridView.MinimumSize = new System.Drawing.Size(200, 100);
            dataGridView.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            dataGridView.StateCommon.HeaderColumn.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            dataGridView.StateCommon.HeaderColumn.Content.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            dataGridView.TabIndex = 1;
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            //dataGridView.DpiChangedAfterParent
            return dataGridView;
        }

        private static void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            KryptonDataGridView gridView = (KryptonDataGridView)sender;

            int indexColNumColor = -1;
            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns[gridView.Columns.Count - 1].HeaderText == "رقم اللون")
                {
                    indexColNumColor = gridView.Columns.Count - 1;
                }
            }
            if (indexColNumColor >= 0)
            {
                int c = Convert.ToInt32(gridView.Rows[e.RowIndex].Cells[indexColNumColor].Value);
                if (c >= 0)
                {
                    e.CellStyle.BackColor = AppColor.colorsReporte[c % AppColor.colorsReporte.Count][0];
                    e.CellStyle.ForeColor = AppColor.colorsReporte[c % AppColor.colorsReporte.Count][1];
                }
            }
            //  AppDialogAleart.showAleartNoPermissions("DataGridView_CellFormatting="+indexColNumColor);
        }

        private static void DataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            KryptonDataGridView gridView = (KryptonDataGridView)sender;

            //gridView.Columns.col
            if (gridView.DataSource != null)
            {
                DataTable data = gridView.DataSource as DataTable;
                //    Control parent = gridView.Parent;
                gridView.Tag = -1;
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (data.Columns[i].Caption != "رقم اللون")
                    {
                        gridView.Columns[i].Width = Convert.ToInt32(data.Columns[i].DefaultValue);
                        gridView.Columns[i].HeaderText = data.Columns[i].Caption;
                    }
                    else
                    {
                        gridView.Columns[i].Visible = false;
                        gridView.Columns[i].HeaderText = "رقم اللون";
                        gridView.Tag = i;
                    }
                }

            }
        }

        static public DataGridViewTextBoxColumn buildeBoxColumn(float fillWeight, string HeaderText, string name, bool Visible = true, int MinimumWidth = 6)
        {
            DataGridViewTextBoxColumn boxColumn = new DataGridViewTextBoxColumn();
            MinimumWidth = (int)fillWeight;
            boxColumn.FillWeight = fillWeight;
            boxColumn.HeaderText = HeaderText;
            boxColumn.MinimumWidth = MinimumWidth;
            boxColumn.Name = name;
            boxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            boxColumn.Visible = Visible;
            return boxColumn;
        }
        static public KryptonDataGridView buildeHederGridView(DataGridViewColumn[] viewColumns)
        {
            KryptonDataGridView hederTable = new KryptonDataGridView();

            hederTable.AllowUserToAddRows = false;
            hederTable.AllowUserToDeleteRows = false;
            hederTable.AllowUserToResizeColumns = false;
            hederTable.AllowUserToResizeRows = false;
            hederTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            hederTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            hederTable.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            hederTable.ColumnHeadersHeight = 36;
            hederTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            hederTable.Columns.AddRange(viewColumns);
            hederTable.Dock = System.Windows.Forms.DockStyle.Top;
            hederTable.Location = new System.Drawing.Point(0, 136);
            hederTable.Name = "hederTable";
            hederTable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            hederTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            hederTable.RowHeadersVisible = false;
            hederTable.RowHeadersWidth = 51;
            hederTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            hederTable.RowTemplate.Height = 26;
            hederTable.Size = new System.Drawing.Size(1005, 35);
            hederTable.StateCommon.HeaderColumn.Back.Color1 = AppColor.primary;
            hederTable.StateCommon.HeaderColumn.Back.Color2 = AppColor.primary;
            hederTable.StateCommon.HeaderColumn.Border.Color1 = AppColor.third;
            //hederTable.StateCommon.Background.Color1 = Color.Red;
            //hederTable.StateCommon.Background.Color2 = Color.Red;
            hederTable.StateCommon.HeaderColumn.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)(((
          PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));

            hederTable.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            hederTable.StateCommon.HeaderColumn.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            hederTable.StateCommon.HeaderColumn.Content.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            hederTable.StateCommon.HeaderColumn.Content.Color1 = AppColor.third;
            hederTable.StateCommon.HeaderColumn.Content.Color2 = AppColor.third;
            hederTable.StateCommon.HeaderColumn.Content.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            hederTable.TabIndex = 13;
            return hederTable;
        }
        static public Control searchControl(Control.ControlCollection controls, string controlName)
        {

            return (controls.Find(controlName, true))[0];
        }
        static public KryptonComboBox buildComboBox(string text, string name, Size size, Point point, object dataSource, float fontSize = 9F, EventHandler eventHandler = null, string displayMember = "name",bool allEvent=false)
        {

            KryptonComboBox newComboBox = new KryptonComboBox();

            newComboBox.SelectionChangeCommitted += eventHandler;
            if(allEvent)
            {
                newComboBox.SelectedValueChanged += eventHandler;
                newComboBox.SelectedIndexChanged += eventHandler;
            }
            newComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            newComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            newComboBox.CornerRoundingRadius = 20F;
            newComboBox.CueHint.Color1 = Color.FromArgb(193, 200, 207);
            newComboBox.Name = name;
            newComboBox.DropDownWidth = 100;
            newComboBox.IntegralHeight = false;
            newComboBox.DataSource = dataSource;
            if (dataSource != null)
            {
                newComboBox.DisplayMember = displayMember;
                newComboBox.ValueMember = "id";
            }

            //newComboBox.Location = new Point(460, 12);
            newComboBox.Size = size;
            newComboBox.StateCommon.ComboBox.Border.Color1 = Color.FromArgb(24, 56, 84);
            newComboBox.StateCommon.ComboBox.Border.Color2 = Color.FromArgb(24, 56, 84);
            newComboBox.StateCommon.ComboBox.Border.DrawBorders = ((PaletteDrawBorders)((((PaletteDrawBorders.Top | PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            newComboBox.StateCommon.ComboBox.Border.Rounding = 20F;
            newComboBox.StateCommon.ComboBox.Content.Color1 = Color.FromArgb(24, 56, 84);
            newComboBox.StateCommon.ComboBox.Content.Font = new Font("Microsoft Sans Serif", fontSize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            newComboBox.StateCommon.ComboBox.Content.TextH = PaletteRelativeAlign.Near;
            newComboBox.StateCommon.DropBack.Color1 = Color.FromArgb(221, 229, 241);
            newComboBox.StateCommon.DropBack.Color2 = Color.FromArgb(221, 229, 241);
            newComboBox.StateTracking.Item.Back.Color1 = Color.FromArgb(24, 56, 84);
            newComboBox.StateTracking.Item.Back.Color2 = Color.FromArgb(24, 56, 84);
            newComboBox.StateTracking.Item.Border.Color1 = Color.FromArgb(221, 229, 241);
            newComboBox.StateTracking.Item.Border.Color2 = Color.FromArgb(221, 229, 241);
            newComboBox.StateTracking.Item.Border.DrawBorders = ((PaletteDrawBorders)((((PaletteDrawBorders.Top | PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            newComboBox.StateTracking.Item.Content.ShortText.Color1 = Color.FromArgb(221, 229, 241);
            newComboBox.StateTracking.Item.Content.ShortText.Color2 = Color.FromArgb(221, 229, 241);
            newComboBox.TabIndex = 63;
            newComboBox.Location = point;


            // newComboBox.Items.Insert(0, "");

            return newComboBox;
        }



        static public Guna2TextBox buildTextBox(string text, string name, Size size, Point point, bool readOnly = false, bool visible = true)
        {
            Guna2TextBox newTextBox1 = new Guna2TextBox();
            newTextBox1.ReadOnly = readOnly;
            newTextBox1.Visible = visible;
            newTextBox1.BackColor = Color.Transparent;
            newTextBox1.BorderColor = Color.FromArgb(24, 56, 84);
            newTextBox1.BorderRadius = 15;
            newTextBox1.Cursor = Cursors.IBeam;
            newTextBox1.DefaultText = "";
            newTextBox1.Name = name;
            newTextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            newTextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            newTextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            newTextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            newTextBox1.FocusedState.BorderColor = Color.Goldenrod;
            newTextBox1.Font = new Font("Segoe UI", 10F);
            newTextBox1.ForeColor = Color.FromArgb(24, 56, 84);
            newTextBox1.HoverState.BorderColor = Color.Goldenrod;
            newTextBox1.Location = point;
            newTextBox1.Margin = new Padding(4, 6, 4, 6);
            newTextBox1.PasswordChar = '\0';
            newTextBox1.PlaceholderText = text;
            newTextBox1.SelectedText = "";
            newTextBox1.ShadowDecoration.Color = Color.FromArgb(24, 56, 84);
            newTextBox1.Size = size;
            newTextBox1.TabIndex = 62;
            return newTextBox1;
        }

        static public Guna2Button buildButton(string text, string name, Point point, Image image, Image imageHover, EventHandler eventHandler = null)
        {
            Guna2Button button = new Guna2Button();
            button.BackColor = Color.Transparent;
            button.DisabledState.BorderColor = Color.DarkGray;
            button.DisabledState.CustomBorderColor = Color.DarkGray;
            button.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            button.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            button.FillColor = Color.Transparent;
            button.Font = new Font("Segoe UI", 9F);
            button.ForeColor = Color.White;
            button.HoverState.FillColor = Color.Transparent;
            button.PressedColor = Color.Transparent;
            button.HoverState.Image = imageHover;
            button.Image = image;
            button.ImageSize = new Size(15, 15);
            button.ImageAlign = HorizontalAlignment.Center;
            button.Location = point;
            button.Name = name;
            button.Size = new Size(40, 30);
            button.TabIndex = 65;
            button.Click += eventHandler;
            return button;
        }
        static public Guna2Button buildCircularBtn(string text, Size size, DockStyle dockStyle = DockStyle.Right, EventHandler eventHandlerClick = null,string name="")
        {
            Guna2Button button = new Guna2Button();
            button.BackColor = System.Drawing.Color.Transparent;
            button.BorderColor = System.Drawing.Color.Transparent;
            button.BorderRadius = 20;
            button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
           // button.Dock = dockStyle;
            button.FillColor = System.Drawing.Color.Transparent;
            button.Font = new System.Drawing.Font("Segoe UI", 10F);
            button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            button.Location = new System.Drawing.Point(330, 0);
            button.Name = name;
            button.PressedColor = System.Drawing.Color.Goldenrod;
            button.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            button.Size = size;
            button.TabIndex = 2;
            button.Text = text;
            button.Click += eventHandlerClick;
            return button;
        }
        static public Guna2Panel buildFooterPage()
        {
            Guna2Panel footerPage = new Guna2Panel();
            footerPage.BackColor = System.Drawing.Color.Transparent;

            footerPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            footerPage.Location = new System.Drawing.Point(0, 566);
            footerPage.Name = "footerTabel";
            footerPage.Size = new System.Drawing.Size(1000, 34);
            footerPage.TabIndex = 2;
            footerPage.Controls.Add(buildCeterFooter());
            return footerPage;

        }
        static public FlowLayoutPanel buildCeterFooter(string name= "ceterFooter")
        {
            FlowLayoutPanel ceterFooter = new FlowLayoutPanel();
            ceterFooter.BackColor = System.Drawing.Color.Transparent;

            //  ceterFooter.Controls.Add(btnFirastPage);
            ceterFooter.Location = new System.Drawing.Point(300, -1);
            ceterFooter.Name = name;
            ceterFooter.Size = new System.Drawing.Size(384, 33);
            ceterFooter.TabIndex = 3;
            ceterFooter.AutoScroll = true;
            ceterFooter.WrapContents = false;
            ceterFooter.FlowDirection = FlowDirection.RightToLeft;
            ceterFooter.RightToLeft = RightToLeft.Yes;
            return ceterFooter;
        }
        static public Label buildCell(string text, string name, Size size, Point point)
        {
            Label label = new Label();
            label.Dock = DockStyle.Right;
            label.FlatStyle = FlatStyle.Flat;
            label.Font = new Font("Tahoma", 10F);
            label.ForeColor = Color.White;
            label.Location = point;
            label.Name = name;
            label.Size = size;
            label.TabIndex = 1;
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleCenter;
            return label;
        }
        static public Guna2TileButton buildBtn(string text, string name, Size size, Point point)
        {
            Guna2TileButton tileButton = new Guna2TileButton();
            // guna2TileButton1
            // 
            tileButton.BackColor = Color.Transparent;
            tileButton.BorderColor = Color.FromArgb(24, 56, 84);
            tileButton.BorderRadius = 5;
            tileButton.Cursor = Cursors.Hand;
            tileButton.DisabledState.BorderColor = Color.DarkGray;
            tileButton.DisabledState.CustomBorderColor = Color.DarkGray;
            tileButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            tileButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            tileButton.FillColor = Color.White;
            tileButton.Font = new Font("Segoe UI", 12F);
            tileButton.ForeColor = Color.FromArgb(24, 56, 84);
            tileButton.HoverState.FillColor = Color.FromArgb(248, 249, 250);
            tileButton.Anchor = AnchorStyles.None;

            //tileButton.Dock = DockStyle.Right;
            tileButton.ImageOffset = new Point(0, -20);
            tileButton.ImageSize = new Size(80, 80);
            tileButton.Location = point;
            tileButton.Name = name;
            tileButton.PressedColor = Color.FromArgb(221, 229, 241);
            tileButton.ShadowDecoration.Color = Color.FromArgb(24, 56, 84);
            tileButton.ShadowDecoration.Enabled = true;
            tileButton.Size = size;
            tileButton.TabIndex = 70;
            tileButton.Text = text;

            return tileButton;
        }
        static public Guna2TileButton buildBtnGroup(string text, string name, Size size, Point point)
        {
            Guna2TileButton tileButton = buildBtn(text, name, size, point);
            tileButton.HoverState.FillColor = Color.FromArgb(24, 56, 84);
            tileButton.HoverState.ForeColor = Color.Goldenrod;
            tileButton.PressedColor = Color.FromArgb(24, 56, 84);
            tileButton.PressedDepth = 10;
            tileButton.BorderRadius = 6;
            tileButton.ShadowDecoration.Color = Color.FromArgb(234, 234, 234);
            tileButton.ShadowDecoration.BorderRadius = 20;
            tileButton.ShadowDecoration.Shadow = new Padding(5, 7, 10, 10);
            tileButton.ShadowDecoration.Depth = 20;
            tileButton.BorderColor = Color.FromArgb(234, 234, 234);
            tileButton.BorderThickness = 1;
            return tileButton;
        }
        static public Guna2TileButton buildBtnGroupItem(ClassifyGroup group, EventHandler eventHandler)
        {

            Guna2TileButton button = buildBtnGroup(group.name, group.id.ToString(), new Size(group.name.Length * 15, 38), new Point(0, 50));
            button.Click += eventHandler;
            // button.Tag = group;
            return button;

        }
        static public Guna2TileButton buildBtnCard(string text, string name, Size size, Point point)
        {
            Guna2TileButton tileButton = buildBtn(text, name, size, point);
            tileButton.BorderRadius = 20;
            tileButton.ShadowDecoration.Color = Color.Red;
            tileButton.ShadowDecoration.Depth = 0;
            tileButton.ShadowDecoration.Shadow = new Padding(7);
            tileButton.ShadowDecoration.BorderRadius = 20;
            tileButton.ShadowDecoration.CustomizableEdges.TopLeft = false;
            tileButton.FillColor = Color.FromArgb(245, 250, 254);
            tileButton.TextOffset = new Point(0, 35);
            return tileButton;
        }
        static public Guna2Panel buildCardItem(Classify item, EventHandler eventHandler, bool sales = true)
        {
            var measurement = item.MeasurementsItems.FirstOrDefault();
            string price = (sales ? measurement?.sellingPrice : measurement?.purchasePrice)?.ToString("c");
            string name = item.nameAr.Length > 12 ? "..." + item.nameAr.Substring(0, 12) : item.nameAr;
            Image img = Properties.Resources.FluentCameraAdd48Filled;
            if (item.image != null)
            {
                MemoryStream memoryStream = new MemoryStream(item.image);
                // memoryStream.Write(item.image, 0, item.image.Length);
                img = Image.FromStream(memoryStream);
            }
            Guna2Panel panelCard = buildPanel(new Size(155, 178), new Point(175 + 20, 90));
            Guna2HtmlLabel label = buildLabel(price, item.id.ToString(), new Point(0, 95));
            Guna2CirclePictureBox image = buildCirclePictureBox(new Point(20, 10), img);
            Guna2TileButton buttonCard = buildBtnCard(name, item.id.ToString(), new Size(150, 175), new Point(1, 1));
            image.Width = buttonCard.Width / 2;
            image.Height = buttonCard.Width / 2;
            label.Left = (buttonCard.Width - label.Width) / 2;
            label.Top = (buttonCard.Height - label.Height) / 3 * 3 - 10;
            image.Left = (buttonCard.Width - image.Width) / 2;
            panelCard.Controls.Add(label);
            panelCard.Controls.Add(image);
            panelCard.Controls.Add(buttonCard);
            panelCard.ShadowDecoration.Color = Color.FromArgb(234, 234, 234);
            panelCard.ShadowDecoration.BorderRadius = 20;
            panelCard.ShadowDecoration.Shadow = new Padding(5, 7, 10, 10);
            panelCard.ShadowDecoration.Depth = 20;
            panelCard.Tag = item;
            panelCard.Click += eventHandler;
            image.Click += eventHandler;
            label.Click += eventHandler;
            buttonCard.Click += eventHandler;
            return panelCard;

        }
        static public Guna2HtmlLabel buildLabel(string text, string name, Point point)
        {

            Guna2HtmlLabel label = new Guna2HtmlLabel();

            label.AvoidGeometryAntialias = true;
            label.BackColor = Color.White;
            label.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label.ForeColor = Color.FromArgb(24, 56, 84);
            label.Location = point;
            label.Name = name;

            label.Size = new Size(45, 23);
            label.TabIndex = 69;
            label.Text = text;
            label.TextAlignment = ContentAlignment.MiddleCenter;

            return label;
        }
        static public Guna2CirclePictureBox buildCirclePictureBox(Point point, Image image)
        {
            Guna2CirclePictureBox circlePictureBox = new Guna2CirclePictureBox();
            circlePictureBox.BackColor = Color.FromArgb(248, 249, 250);
            circlePictureBox.FillColor = SystemColors.WindowText;
            circlePictureBox.Image = image;
            circlePictureBox.ImageRotate = 0F;
            circlePictureBox.Location = point;
            circlePictureBox.Name = "guna2CirclePictureBox1";
            circlePictureBox.ShadowDecoration.Color = Color.FromArgb(248, 249, 250);
            circlePictureBox.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            circlePictureBox.Size = new Size(80, 80);
            circlePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            circlePictureBox.TabIndex = 71;
            circlePictureBox.TabStop = false;
            return circlePictureBox;
        }
        static public Guna2Panel buildPanel(Size size, Point point)
        {
            Guna2Panel panel = new Guna2Panel();
            panel.BackColor = Color.Transparent;
            panel.Size = size;
            panel.Location = point;
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Depth = 30;

            panel.ShadowDecoration.Color = Color.FromArgb(24, 56, 84);
            panel.ShadowDecoration.Shadow = new Padding(5, 5, 5, 5);

            return panel;
        }
        static public Guna2GroupBox buildGroupBoxTable()
        {
            KryptonDataGridView dataGridView = new KryptonDataGridView();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            Guna2GroupBox groupBoxTable = new Guna2GroupBox();
            groupBoxTable.BackColor = Color.Transparent;
            groupBoxTable.BorderColor = Color.Transparent;
            groupBoxTable.CustomBorderColor = Color.Transparent;
            groupBoxTable.Dock = DockStyle.Fill;
            groupBoxTable.Font = new Font("Segoe UI", 11F);
            groupBoxTable.ForeColor = AppColor.primary;
            groupBoxTable.Location = new Point(0, 68);
            groupBoxTable.Name = "groupBoxTable";
            groupBoxTable.RightToLeft = RightToLeft.Yes;
            groupBoxTable.Size = new Size(1000, 988);
            groupBoxTable.TabIndex = 2;
            groupBoxTable.Text = "";
            groupBoxTable.TextAlign = HorizontalAlignment.Right;
            dataGridView.AllowDrop = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.ColumnHeadersHeight = 36;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(0, 75);
            dataGridView.Name = "dataGridView";
            dataGridView.RightToLeft = RightToLeft.Yes;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.RowTemplate.Height = 26;
            dataGridView.RowTemplate.Resizable = DataGridViewTriState.True;
            dataGridView.ReadOnly = true;
            dataGridView.Size = new Size(1000, 971);
            dataGridView.StateCommon.Background.Color1 = Color.White;
            dataGridView.StateCommon.Background.Color2 = Color.White;
            dataGridView.StateCommon.BackStyle = PaletteBackStyle.GridBackgroundList;
            dataGridView.StateCommon.DataCell.Back.Color1 = Color.White;
            dataGridView.StateCommon.DataCell.Back.Color2 = Color.White;
            dataGridView.StateCommon.DataCell.Border.Color1 = AppColor.primary;
            dataGridView.StateCommon.DataCell.Border.Color2 = AppColor.primary;
            dataGridView.StateCommon.DataCell.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            dataGridView.StateCommon.DataCell.Content.Color1 = AppColor.primary;
            dataGridView.StateCommon.DataCell.Content.Color2 = AppColor.primary;
            dataGridView.StateCommon.DataCell.Content.Font = new Font("Tahoma", 10.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridView.StateCommon.HeaderColumn.Back.Color1 = AppColor.primary;
            dataGridView.StateCommon.HeaderColumn.Back.Color2 = AppColor.primary;
            dataGridView.StateCommon.HeaderColumn.Border.Color1 = AppColor.third;
            dataGridView.StateCommon.HeaderColumn.Border.Color2 = AppColor.third;
            dataGridView.StateCommon.HeaderColumn.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            dataGridView.StateCommon.HeaderColumn.Content.Color1 = AppColor.third;
            dataGridView.StateCommon.HeaderColumn.Content.Color2 = AppColor.third;
            dataGridView.StateCommon.HeaderColumn.Content.Font = new Font("Tahoma", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            dataGridView.StateCommon.HeaderRow.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            dataGridView.StateSelected.DataCell.Back.Color1 = Color.FromArgb(221, 229, 241);
            dataGridView.StateSelected.DataCell.Back.Color2 = Color.FromArgb(221, 229, 241);
            dataGridView.StateSelected.DataCell.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            dataGridView.StateSelected.HeaderRow.Border.Color1 = AppColor.primary;
            dataGridView.StateSelected.HeaderRow.Border.Color2 = AppColor.primary;
            dataGridView.StateSelected.HeaderRow.Back.Color1 = Color.FromArgb(221, 229, 241);
            dataGridView.StateSelected.HeaderRow.Back.Color2 = Color.FromArgb(221, 229, 241);
            //dataGridView.StateCommon.HeaderRow.Back.Color1 = Color.FromArgb(221, 229, 241);
            //dataGridView.StateCommon.HeaderRow.Back.Color2 = Color.FromArgb(221, 229, 241);
            dataGridView.StateSelected.HeaderRow.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | PaletteDrawBorders.Bottom)
            | PaletteDrawBorders.Left)
            | PaletteDrawBorders.Right)));
            dataGridView.TabIndex = 3;
            groupBoxTable.Controls.Add(dataGridView);
            groupBoxTable.Controls.Add(buildFooterPage());

            return groupBoxTable;
        }
        static public Guna2CheckBox buildCheckBox(string text, string name, bool isChecked)
        {
            Guna2CheckBox checkBox = new Guna2CheckBox();
            // 
            checkBox.Checked = isChecked;
            // checkBox.AutoCheck = isChecked;
            checkBox.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            checkBox.CheckedState.BorderRadius = 0;
            checkBox.CheckedState.BorderThickness = 0;
            checkBox.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(56)))), ((int)(((byte)(84)))));
            checkBox.CheckMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox.Dock = System.Windows.Forms.DockStyle.Right;
            checkBox.Font = new System.Drawing.Font("Tahoma", 10F);
            checkBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            checkBox.Location = new System.Drawing.Point(704, 40);
            checkBox.Name = name;
            checkBox.Size = new System.Drawing.Size(153, 45);
            checkBox.TabIndex = 74;
            checkBox.Text = text;
            checkBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            checkBox.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            checkBox.UncheckedState.BorderRadius = 0;
            checkBox.UncheckedState.BorderThickness = 0;
            checkBox.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            return checkBox;

        }
        static public Guna2GroupBox buildGroupBoxPermission(PermissionGUI permission)
        {

            Guna2GroupBox groupBoxPermission = new Guna2GroupBox();

            groupBoxPermission.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            groupBoxPermission.BorderRadius = 20;
            groupBoxPermission.Controls.Add(buildCheckBox("إضافه", "add", permission.addPermission));
            groupBoxPermission.Controls.Add(buildCheckBox("تعديل", "update", permission.updatePermission));
            groupBoxPermission.Controls.Add(buildCheckBox("عرض", "view", permission.viewPermission));
            groupBoxPermission.Controls.Add(buildCheckBox("حذف", "delete", permission.deletePermission));
            groupBoxPermission.CustomBorderColor = System.Drawing.Color.Transparent;
            groupBoxPermission.Dock = System.Windows.Forms.DockStyle.Top;
            groupBoxPermission.FillColor = System.Drawing.Color.Transparent;
            groupBoxPermission.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBoxPermission.ForeColor = System.Drawing.Color.Goldenrod;
            groupBoxPermission.Location = new System.Drawing.Point(0, 0);
            groupBoxPermission.Name = permission.cell.name;
            groupBoxPermission.Size = new System.Drawing.Size(857, 40);
            groupBoxPermission.TabIndex = 0;
            groupBoxPermission.Text = permission.cell.caption;
            groupBoxPermission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            groupBoxPermission.Click += GroupBoxHome_Click;
            void GroupBoxHome_Click(object sender, EventArgs e)
            {
                Guna2GroupBox groupBox = (Guna2GroupBox)sender;
                Guna2Panel perantPanel = (Guna2Panel)groupBox.Parent;
                if (groupBox.Tag == null)
                {
                    perantPanel.Height += 40;
                    groupBox.Height = 85;
                    groupBox.Tag = "open";
                }
                else
                {
                    perantPanel.Height -= 40;
                    groupBox.Height = 40;
                    groupBox.Tag = null;
                }
            }
            return groupBoxPermission;


        }
        static public Label buildePlaceholderText()
        {
            Label label = new Label();
            label.AutoSize = true;
            label.BackColor = System.Drawing.Color.Transparent;
            label.Font = new System.Drawing.Font("Tahoma", 9F);
            label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(229)))), ((int)(((byte)(241)))));
            label.Location = new System.Drawing.Point(186, 29);
            label.Name = "label1";
            label.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            label.Size = new System.Drawing.Size(78, 24);
            label.TabIndex = 64;
            return label;
        }
        static public Guna2Panel buildeCardMeasurementsItem(MeasurementsItem measurementsItem, EventHandler cardMeasurementsItem_Click)
        {
            Guna2Panel cardMeasurementsItem = new Guna2Panel();
            Label price = new Label();
            Label unit = new Label();
            Guna2RadioButton radioBtn = new Guna2RadioButton();
            int width = 660;
            int widthRadioBtn = 20;
            // cardMeasurementsItem
            // 
            cardMeasurementsItem.BorderColor = System.Drawing.Color.Red;
            cardMeasurementsItem.BorderRadius = 10;
            cardMeasurementsItem.BorderThickness = 1;
            cardMeasurementsItem.Controls.Add(price);
            cardMeasurementsItem.Controls.Add(unit);
            cardMeasurementsItem.Controls.Add(radioBtn);
            cardMeasurementsItem.Location = new System.Drawing.Point(3, 3);
            cardMeasurementsItem.Name = "cardMeasurementsItem";
            cardMeasurementsItem.Size = new System.Drawing.Size(width, 50);
            cardMeasurementsItem.TabIndex = 0;
            cardMeasurementsItem.Click += new System.EventHandler(cardMeasurementsItem_Click);
            cardMeasurementsItem.Tag = measurementsItem;
            // 
            // price
            // 
            width -= widthRadioBtn;
            int x = width - cardMeasurementsItem.Width - 10;
            price.BackColor = System.Drawing.Color.Transparent;
            price.Font = new System.Drawing.Font("Tahoma", 10F);
            price.ForeColor = System.Drawing.Color.Red;
            price.Location = new System.Drawing.Point(x, 24);
            // price.BackColor= System.Drawing.Color.Red;
            price.Name = "price";
            price.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            price.Size = new System.Drawing.Size(width, 29);
            price.TabIndex = 3;
            price.Text = "السعر : " + measurementsItem.sellingPrice;
            price.Click += new System.EventHandler(cardMeasurementsItem_Click);
            // 
            // unit
            // 

            unit.AutoEllipsis = true;
            unit.BackColor = System.Drawing.Color.Transparent;
            unit.Font = new System.Drawing.Font("Tahoma", 10F);
            unit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(90)))), ((int)(((byte)(115)))));
            unit.Location = new System.Drawing.Point(x, 5);
            unit.Name = "unit";
            unit.Size = new System.Drawing.Size(width, 24);
            unit.TabIndex = 2;
            unit.Text = "الوحده: " + measurementsItem.Unit.name;
            unit.Click += new System.EventHandler(cardMeasurementsItem_Click);
            // 
            // radioBtn
            // 
            x = cardMeasurementsItem.Width - widthRadioBtn - 10;
            radioBtn.AutoSize = false;
            radioBtn.AutoCheck = false;
            radioBtn.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            radioBtn.CheckedState.BorderThickness = 0;
            radioBtn.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            radioBtn.CheckedState.InnerColor = System.Drawing.Color.White;
            radioBtn.CheckedState.InnerOffset = -4;
            radioBtn.FlatAppearance.BorderSize = 5;
            //   radioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            radioBtn.Location = new System.Drawing.Point(x, 18);
            radioBtn.Name = "radioBtn";
            //   radioBtn.BackColor = System.Drawing.Color.Red;
            radioBtn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            radioBtn.Size = new System.Drawing.Size(widthRadioBtn, widthRadioBtn);
            radioBtn.TabIndex = 1;
            radioBtn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            radioBtn.UncheckedState.BorderThickness = 2;
            radioBtn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            radioBtn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            radioBtn.CheckedChanged += new System.EventHandler(cardMeasurementsItem_Click);
            radioBtn.BringToFront();
            return cardMeasurementsItem;
            // 
        }
    }
    public enum ProsessesType
    {
        add,
        update,
        view

    }
    //public enum FinancialType
    //{
    //    Receipt,
    //    Exspande,
    //}
}
