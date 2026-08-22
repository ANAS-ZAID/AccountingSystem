using Guna.UI2.WinForms;
using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.Functions;

namespace AccountingSystem.core.shared
{
    public static class ValidatingData
    {
        static public void PriceOnly(this Control control)
        {
            control.KeyPress += Control_KeyPress;
            control.AddPlaceholderText();
            void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true; // منع إدخال الحرف
                }
                else
                {
                    // إذا كان الحرف نقطة عشرية، نتأكد أنه لا يوجد أكثر من نقطة عشرية واحدة
                    if (e.KeyChar == '.' && ((Guna2TextBox)sender).Text.IndexOf('.') > -1)
                    {
                        e.Handled = true;
                    }
                    // إذا كان الحرف رقم، نتأكد أن القيمة الإجمالية لن تتجاوز 1

                }
            }
        }

        static public void AddPlaceholderText(this Control control,string DisplayMember= "name")
        {
            Label label = BuildControls.buildePlaceholderText();
            Control parent = (Control)control.Parent;
            if (control is Guna2TextBox)
            {
                label.Text = ((Guna2TextBox)control).PlaceholderText;

            }
            if (control is KryptonComboBox)
            {
                KryptonComboBox comboBox = (KryptonComboBox)control;
                label.Text = comboBox.CueHint.CueHintText;
                try
                {
                    comboBox.DisplayMember = DisplayMember;
                    comboBox.ValueMember = "id";
                }
                catch { }
               //if(!String.IsNullOrEmpty(DisplayMember))
               // {
                  
               // }
            
            }
            parent.Controls.Add(label);
           
            label.BringToFront();
            label.ForeColor = AppColor.primary;
            label.BackColor = Color.Transparent;
            label.Visible = false;
            control.Tag = label;
            control.Enter += eventPlaceholder_Enter;
            control.Leave += eventPlaceholder_Leave;
            control.TextChanged += Control_TextChanged;
            control.SizeChanged += Control_SizeChanged;
            Control_SizeChanged(control, null);
            //    control.VisibleChanged += Control_VisibleChanged;

        }

        private static void Control_SizeChanged(object sender, EventArgs e)
        {
         Control control = sender as Control;
            Label label = control.Tag as Label;
            label.Location = control.Location;
            label.Left += control.Width - label.Width;
            label.Top -= label.Height / 2;
            while (label.Top <0)
            {
                label.Top++;
            }
            
        }

        private static void Control_VisibleChanged(object sender, EventArgs e)
        {
            //Control control = sender as Control;
            //(control.Tag as Label).Visible = control.Visible;
            //if (String.IsNullOrEmpty(control.Text) && !control.CanFocus)
            //    (control.Tag as Label).Visible = false;
        }

        static public void eventPlaceholder_Enter(object sender, EventArgs e)
        {
            ((sender as Control).Tag as Label).Visible=true;

        }
        static public void eventPlaceholder_Leave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if(String.IsNullOrEmpty(control.Text))
            (control.Tag as Label).Visible = false;
  
    
        }

        private static void Control_TextChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (!String.IsNullOrEmpty(control.Text))
                eventPlaceholder_Enter(sender, e);
            else if(String.IsNullOrEmpty(control.Text)&& !control.CanFocus)
                (control.Tag as Label).Visible = false;

        }
       

        static public void NumberOnly(this Control control)
        {
            control.KeyPress += Control_KeyPress;
            control.AddPlaceholderText();
          //  control.Tag =label;
           void Control_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
       
        }
        static public void PhoneOnly(this Control control)
        {
            control.KeyPress += Control_KeyPress;
            control.AddPlaceholderText();
          //  control.Tag =label;
           void Control_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
       
        }  
        static public void TextOnly(this Control control, string DisplayMember = "name")
        {
            control.KeyPress += Control_KeyPress;
            control.AddPlaceholderText(DisplayMember);
            void Control_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar)&&e.KeyChar!=(char) Keys.Space)
                    e.Handled = true;
            }
        }

      

        static public void eventTextBoxNumberOnly(object sender, KeyPressEventArgs e)
        {

           if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        
        }
      static  public void ReductionPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {

            // نتحقق إذا كان الحرف المدخل هو رقم أو نقطة عشرية أو زر حذف
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // منع إدخال الحرف
            }
            else
            {
                // إذا كان الحرف نقطة عشرية، نتأكد أنه لا يوجد أكثر من نقطة عشرية واحدة
                if (e.KeyChar == '.' && ((Guna2TextBox)sender).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
                // إذا كان الحرف رقم، نتأكد أن القيمة الإجمالية لن تتجاوز 1
                else if (char.IsDigit(e.KeyChar))
                {
                    string newText = ((Guna2TextBox)sender).Text + e.KeyChar;
                    if (double.Parse(newText) >= 1)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
        static public void eventTextBoxPriceOnly(object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // منع إدخال الحرف
            }
            else
            {
                // إذا كان الحرف نقطة عشرية، نتأكد أنه لا يوجد أكثر من نقطة عشرية واحدة
                if (e.KeyChar == '.' && ((Guna2TextBox)sender).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
                // إذا كان الحرف رقم، نتأكد أن القيمة الإجمالية لن تتجاوز 1
                
            }
        }
        static public void eventTextBoxphoneNumberOnly(object sender, KeyPressEventArgs e)
        {
           // Guna2TextBox textBox = sender as Guna2TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {

                e.Handled = true;
                //if (textBox.Text.Contains("-"))
                // {
                //     textBox.Text=textBox.Text.Replace("-","");
                //     textBox.Text ="-" +textBox.Text;
                //    // textBox.Text.Insert(0, "-");

                // }
            }
            //else if (e.KeyChar == '+')
            //{
            //    if (String.IsNullOrWhiteSpace(textBox.Text) || textBox.Text.Contains("+"))
            //        e.Handled = true;
            //}
            //else if (e.KeyChar == '-')
            //{
            //    if (String.IsNullOrWhiteSpace(textBox.Text)  )
            //        e.Handled = true;
            //    else
            //    { if(textBox.Text.Contains("-"))
            //        textBox.Text= textBox.Text.Replace("-", "");
            //        textBox.Text = "-" + textBox.Text;
            //        e.Handled = true;
            //    }

            //}
            //else
            //{
            //    e.Handled = true;
            //}
        }
        static public void eventTextBoxTextOnly(object sender, KeyPressEventArgs e)
        {
          if (!char.IsControl(e.KeyChar)&&!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Space)
                e.Handled= true;

        }
        static public void event_Enter(object sender, EventArgs e)
        {

            Guna2TextBox textBox;
            KryptonComboBox comboBox;

            Label label = BuildControls.buildePlaceholderText();
            //label.Visible = false;
            if (sender is Guna2TextBox)
            {
                textBox = (Guna2TextBox)sender;
                Control control = textBox.Parent;
                if (textBox.Tag != null)
                {
                    label = (Label)textBox.Tag;
                        label.Visible = true;
                }
                else
                {
                    control.Controls.Add(label);
                    label.Text = textBox.PlaceholderText;
                    label.Location = textBox.Location;
                    label.Top -= 17;
                    textBox.Tag = label;
                    label.Visible = true;
                }
                label.BringToFront();

            }
            if (sender is KryptonComboBox)
            {
                comboBox = (KryptonComboBox)sender;
                Control control = comboBox.Parent;
                control.Controls.Add(label);
                label.Text = comboBox.CueHint.CueHintText;
                label.Location = comboBox.Location;
                label.Top -= 17;
                label.BringToFront();
            }

        }
        static public void event_Leave(object sender, EventArgs e)
        {

            Guna2TextBox textBox;
            KryptonComboBox comboBox;

            Label label = BuildControls.buildePlaceholderText();
            label.Visible = false;
            AppDialogAleart.showAleartPreExistingData("ads");
            if (sender is Guna2TextBox)
            {
                textBox = (Guna2TextBox)sender;
                Control control = textBox.Parent;
               if (textBox.Tag!=null)
                {
                    label=(Label)textBox.Tag;
                    if (!textBox.Text.Any())
                        label.Visible = false;
                }
                label.BringToFront();
                
            }
            if (sender is KryptonComboBox)
            {
                comboBox = (KryptonComboBox)sender;
                Control control = comboBox.Parent;
                control.Controls.Add(label);
                label.Text = comboBox.CueHint.CueHintText;
                label.Location = comboBox.Location;
                label.Top -= 17;
                label.BringToFront();
            }


        }
        static public bool validatingData(string text,string message,bool isTextBox=true)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                message = ((isTextBox) ? "من فضلك أكتب " : "من فضلك أختر ") + message;
                AppDialogAleart.showAleartErrorData(message);
                return false;
            }
            else return true;
        }
        static public bool validatingData(object myObject, string message, bool isTextBox = true)
        {
            if (myObject==null)
            {
                message = ((isTextBox) ? "من فضلك أكتب " : "من فضلك أختر ") + message;
                AppDialogAleart.showAleartErrorData(message);
                return false;
            }
            else return true;
        }
        static public bool validatingDataId(int id, string message, bool isTextBox = false)
        {
            if (id==0)
            {
                message = ((isTextBox) ? "من فضلك أكتب " : "من فضلك أختر ") + message;
                AppDialogAleart.showAleartErrorData(message);
                return false;
            }
            else return true;
        }
        static public bool validatingDataComboBox(string text, string message)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                AppDialogAleart.showAleartErrorData(message);
                return false;
            }
            else return true;
        }
    
}
}
