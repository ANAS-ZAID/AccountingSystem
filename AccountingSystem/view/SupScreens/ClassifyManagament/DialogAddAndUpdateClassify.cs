using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.controller;
using AccountingSystem.core.Functions;
using AccountingSystem.core.shared;
using AccountingSystem.NewModel.EFModel;

namespace AccountingSystem.view.SupScreens.ClassifyManagament
{
    public partial class DialogAddAndUpdateClassify : Form
    {

        ItemController controller;
      
        ItemsWidget itemsWidget;
        public DialogAddAndUpdateClassify(ItemController controller)
        {
            InitializeComponent();
            this.controller = controller;


        }
       

        private void btnMainOrSup_Click(object sender, EventArgs e)
        {
            Guna2Button button = (Guna2Button)sender;
            if (button.Tag == null)
            {
                FunctionsGUI.reChangeColorActiveBtn(btnMain);
                FunctionsGUI.reChangeColorActiveBtn(btnSup);
                FunctionsGUI.changeColorActiveBtn(button);
                 controller.temp.type = button.Text;
                panelFialdMainData.Visible = true;
                nameAr.Visible= true;
                nameEn.Visible= true;
                description.Visible= true;
                panelImage.Visible= true;
                if (button.Text == "فرعي")
                {
                    panelSettingItem.Visible = true;
                    tableMeasurements.Visible = true;
                }
                else
                {
                    panelSettingItem.Visible = false;
                    tableMeasurements.Visible = false;
                }

            }
        }

        private void image_Click(object sender, EventArgs e)
        {
            image.Image = Functions.choseImage();
        }

        private void nameAr_TextChanged(object sender, EventArgs e)
        {
            controller.nameItemSelectedMeasurementsItem=nameAr.Text;
        }

        private void number_TextChanged(object sender, EventArgs e)
        {
            controller.numberItemSelectedMeasurementsItem = number.Text;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(controller.temp.type))
            {
                itemsWidget.getMeasurementsItems();

                if (controller.dataProcessing(nameAr.Text, nameEn.Text, description.Text, number.Text, image.Image))
                    Close();
            }

        }

        private void DialogAddAndUpdateClassify_Load(object sender, EventArgs e)
        {
           // controller = new ItemController();
            itemsWidget = new ItemsWidget(controller, toolTip1);
            Guna2Panel panel = itemsWidget.returnNewTablePanels();
            tableMeasurements.Controls.Add(panel);
            panel.Width = tableMeasurements.Width;
            panel.Top -= 10;
           itemsWidget.fillTablePanels();
            perant.DataSource = controller.mainItms;
            group.DataSource = controller.groups;
            itemType.DataSource = controller.types;
            company.DataSource = controller.companies;
            perant.TextOnly("nameAr");
            nameAr.TextOnly();
            nameEn.TextOnly();
            perant.TextOnly();
            group.TextOnly();
            itemType.TextOnly();
            number.NumberOnly();
            company.TextOnly();
            setComboBox();
            if (controller.prosessesType == ProsessesType.update)
            {
                fillFeild();
            }
            DialogAddAndUpdateClassify_SizeChanged(mainBody, null);
        }
        private void fillFeild()
        {
            nameAr.Text = controller.temp.nameAr;
            nameEn.Text = controller.temp.nameEn;
            number.Text = controller.temp.ClassifyNumber.ToString();
            description.Text = controller.temp.description;
            if (controller.temp.type == "رئيسي")
                btnMainOrSup_Click(btnMain, null);
            if (controller.temp.type == "فرعي")
                btnMainOrSup_Click(btnSup, null);
          if(controller.temp.image!=null)
            {
                MemoryStream memoryStream = new MemoryStream();
                memoryStream.Write(controller.temp.image, 0, controller.temp.image.Count());
                image.Image = Image.FromStream(memoryStream);
            }
        }
        void setComboBox()
        {
            perant.SelectedItem = controller.temp.perantItem;
            group.SelectedItem = controller.temp.ClassifyGroup;
            company.SelectedItem = controller.temp.Company;
            itemType.SelectedItem = controller.temp.TypesClassify;
            //type.Text = controller.temp.type;
        }
        private void perant_SelectionChangeCommitted(object sender, EventArgs e)
        {
          number.Text=  controller.selectedPerantItem(perant.SelectedItem);
            if (controller.temp.perantItem!=null&& controller.temp.perantItem.id != 0)
                group.DataSource = new List<NewModel.EFModel.ClassifyGroup>();
            else
                group.DataSource = controller.groups;
        }

        private void group_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedClassifyGroup(group.SelectedItem);
            if (controller.temp.ClassifyGroup != null && controller.temp.ClassifyGroup.id != 0)
                perant.DataSource = new List<Classify>();
            else
                perant.DataSource = controller.mainItms;

        }

        private void company_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedCompany(company.SelectedItem);
        }

        private void itemType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            controller.selectedTypeItem(itemType.SelectedItem);
        }
    
        private void btnClear_Click(object sender, EventArgs e)
        {
            //itemsWidget.clearTablePanels();
            //nameAr.Clear();
            //nameEn.Clear();
            //number.Clear();
            //description.Clear();
           
            //setComboBox();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void DialogAddAndUpdateClassify_FormClosing(object sender, FormClosingEventArgs e)
        {
            controller.clearTempData();
        }

        private void DialogAddAndUpdateClassify_SizeChanged(object sender, EventArgs e)
        {
            Control control= sender as Control;
            int totalHieght = 0;
            foreach (Control item in control.Controls)
            {
                item.Width = control.Width;
                if(item.Name!= "panelMeasurements")
                totalHieght += item.Height;
            }
            //panelMeasurements.Height=mainBody.Height-totalHieght+ panelMeasurements.Height-20;
            panelMeasurements.Height = mainBody.Height - totalHieght-20;
            panelDescriptionAndImage.Height= (int)((mainBody.Height) * .20);
        }

        private void supTitel_SizeChanged(object sender, EventArgs e)
        {
            panelBtnMainAndScendery.Location = new Point((supTitel.Width - panelBtnMainAndScendery.Width) / 2, (supTitel.Height - panelBtnMainAndScendery.Height) / 2);
        }

        private void panelNames_SizeChanged(object sender, EventArgs e)
        {
            int x = (int)(panelNames.Width * .01)/2;
            foreach (Control item in panelNames.Controls)
            {
                if (!(item is Label))
                {
                    item.Width = (int)(panelNames.Width * .49);
                    item.Location = new Point(x, (panelNames.Height - item.Height) / 2);
                    x += item.Width + (int)(panelNames.Width * .01 * 0.70);
                }
            }
        }

        private void panelMianData_SizeChanged(object sender, EventArgs e)
        {
            if (panelMianData.Width < 951)
                panelFialdMainData.Width = panelMianData.Width;
            else
                panelFialdMainData.Width = 951;
            panelFialdMainData.Location = new Point((panelMianData.Width - panelFialdMainData.Width) / 2, (panelMianData.Height - panelFialdMainData.Height) / 2);
          //  panelFialdMainData_SizeChanged(null, null);
        }

        private void panelFialdMainData_SizeChanged(object sender, EventArgs e)
        {
            
            int margin = (int)(panelFialdMainData.Width * 0.04) / 7;
            int width = (int)(panelFialdMainData.Width * 0.16) ;
            int newLeftLocation = 0;
            //for(int i=panelFialdMainData.Controls.Count-1;i>=0; i--)
            for(int i=0;i< panelFialdMainData.Controls.Count; i++)
            {
                Control item = panelFialdMainData.Controls[i];
                if (!(item is Label))
                {
                    newLeftLocation += margin;
                    item.Width = width;
                    item.Location = new Point(newLeftLocation, (panelFialdMainData.Height - item.Height) / 2);
                    newLeftLocation += item.Width;
                }
            }
        }

        private void panelDescriptionAndImage_SizeChanged(object sender, EventArgs e)
        {
            int margin = (int)(panelDescriptionAndImage.Width * 0.01)/2;
            int newLeftLocation =margin;
            int height = (int)(panelDescriptionAndImage.Height * 0.95);
            int newTopLocation = (int)(panelDescriptionAndImage.Height * 0.2)/2;
            panelImage.Size = new Size((int)(panelDescriptionAndImage.Width * 0.30), height);
            panelImage.Location = new Point(newLeftLocation, newTopLocation);
            newLeftLocation += panelImage.Width + margin;
            description.Size = new Size((int)(panelDescriptionAndImage.Width * 0.68), height);
            description.Location = new Point(newLeftLocation, newTopLocation);
           
        }

        private void panelImage_SizeChanged(object sender, EventArgs e)
        {
            if (panelImage.Width < 129)
                image.Width = panelImage.Width-4;
            else image.Width = 127;
            image.Location = new Point((panelImage.Width - image.Width) / 2, (panelImage.Height - image.Height) / 2);
            labelImage.Location = new Point((panelImage.Width - labelImage.Width) / 2, (int)((panelImage.Height - image.Height) * .10));
        }

        private void footer_SizeChanged(object sender, EventArgs e)
        {
            int margin = (int)(footer.Width * 0.01) /3;
            int width = (int)(footer.Width * 0.10);
            width = footer.Width<300? (int)(footer.Width * 0.30): width < 100 ? 97 : width;
            int newLeftLocation = (footer.Width- width*3)/2;
            for (int i = 0; i < footer.Controls.Count; i++)
            {
                Control item = footer.Controls[i];
                if (!(item is Label))
                {
                    newLeftLocation += margin;
                    item.Width = width;
                    item.Location = new Point(newLeftLocation, (footer.Height - item.Height) / 2);
                    newLeftLocation += item.Width;
                }
            }
        }

        private void panelMeasurements_SizeChanged(object sender, EventArgs e)
        {
           // tableMeasurements.Size = new Size((int)(panelMeasurements.Width * 0.30), (int)(panelMeasurements.Height * 0.70));
            int margin = (int)(panelMeasurements.Width * 0.01) / 2;
            int newLeftLocation = (panelMeasurements.Width- tableMeasurements.Width)/2;
            int newTopLocation = (int)(panelMeasurements.Height * 0.2) / 2;
            if (panelMeasurements.Width < 1100)
            {
                tableMeasurements.Width = panelMeasurements.Width;
                panelSettingItem.Width = panelMeasurements.Width;
            }
            else
            {
                tableMeasurements.Width = 1079;
                panelSettingItem.Width = 1079;
            }
            panelSettingItem.Location = new Point((panelMeasurements.Width - panelSettingItem.Width) / 2, newTopLocation  );
          
            newTopLocation+= margin+panelSettingItem.Height;
          
            tableMeasurements.Location = new Point((panelMeasurements.Width- tableMeasurements.Width)/2, newTopLocation);
          
        }

        private void panelSettingItem_SizeChanged(object sender, EventArgs e)
        {
            int margin = (int)(panelSettingItem.Width * 0.10) / 5;
            int width = (int)(panelSettingItem.Width * 0.20);
            int newLeftLocation = 0;
            //for(int i=panelFialdMainData.Controls.Count-1;i>=0; i--)
            for (int i = 0; i < panelSettingItem.Controls.Count; i++)
            {
                Control item = panelSettingItem.Controls[i];
                if (!(item is Label))
                {
                    newLeftLocation += margin;
                    //item.Width = panelSettingItem.Controls.Count-1==i? width/2:width;
                    item.Width = 0==i? width/2:width;
                 //   AppDialogAleart.showAleartNoPermissions("item.name=" + item.Name + ";i= "+i+ ";item.GetType().Name="+ item.GetType().Name);
                    item.Location = new Point(newLeftLocation, (panelSettingItem.Height - item.Height) / 2);
                    newLeftLocation += item.Width;
                }
            }
        }

        private void tableMeasurements_SizeChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tableMeasurements.Controls.Count; i++)
            {
                Control item = tableMeasurements.Controls[i];
                if (!(item is Label))
                {
                   
                    item.Width = tableMeasurements.Width;
                }
            }
        }

        private void headerTable_SizeChanged(object sender, EventArgs e)
        {
         
                //Guna2TextBox minimumPurchaseAmount = BuildControls.buildTextBox("أقل مبلغ شراء", "minimumPurchaseAmount", size, new Point(35, 5));
                //Guna2TextBox reductionPercentage = BuildControls.buildTextBox("تخفيض", "reductionPercentage", size, new Point(160, 5));
                //Guna2TextBox wholesalePurchasePrice = BuildControls.buildTextBox(" س. شراء جملة", "wholesalePurchasePrice", size, new Point(285, 5));
                //Guna2TextBox purchasePrice = BuildControls.buildTextBox("سعر الشراء", "purchasePrice", size, new Point(285, 5));
                //Guna2TextBox wholesalePrice = BuildControls.buildTextBox("س. بيع جملة", "wholesalePrice", size, new Point(35, 5));
                //Guna2TextBox sellingPrice = BuildControls.buildTextBox("سعر البيع", "sellingPrice", size, new Point(160, 5));
                //KryptonComboBox unit = BuildControls.buildComboBox("الوحده" + countnNewRow, "unit", ComboBoxSize, new Point(413, 5), tempList, eventHandler: comboBox_SelectionChangeCommitted);
                //Guna2TextBox barcode = BuildControls.buildTextBox("الباركود", "barcode", size, new Point(285, 5));
                Control row = sender as Control;
                int feildWidght = (int)(row.Width * 0.113);
                for (int i = 0; i < row.Controls.Count; i++)
                {
                    row.Controls[i].Width = row.Controls.Count -1== i? feildWidght/2:feildWidght;
                }
            }
    }

}
