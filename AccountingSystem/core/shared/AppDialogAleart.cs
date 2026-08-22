using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccountingSystem.core.shared;



namespace AccountingSystem.core.Functions
{
    internal class AppDialogAleart
    {
        static public void showAleartSuccess(string message = "تمت العمليه بنجاح ")
        {
            AleartSuccessORError aleart = new AleartSuccessORError(
                 MessageType.Success);
            aleart.ShowDialog();


        }
        static public void showAleartNoPermissions(string message = "ليس لديك صلاحيات")
        {
            AleartError aleart = new AleartError(message, MessageType.Error);
            DialogResult result = aleart.ShowDialog();

        }
        static public void showAlertGetType(object obj,int len=20)
        {
            AleartError aleart = new AleartError(obj.GetType().ToString().Substring(0, len), MessageType.Error);
            DialogResult result = aleart.ShowDialog();

        }
        static public void showAleartError(string message = "حدث خطأ ما في العمليه ")
        {
            AleartSuccessORError aleart = new AleartSuccessORError(
      MessageType.Error,message);
            aleart.Show();
        }
        static public void showAleartErrorData(string message = "حدث خطأ ما في العمليه ")
        {
            AleartError aleart = new AleartError(message, MessageType.Error);
            DialogResult result = aleart.ShowDialog();
        } 
        static public void showAleart(string message, MessageType messageType)
        {
            AleartError aleart;
            switch (messageType)
            {
                case MessageType.NoDataSpecified:
                    aleart = new AleartError("لم تقم بتحديد أي بيانات " +message, messageType);
                    break;
                default:
                    aleart = new AleartError(message, messageType);
                    break;
            }
            DialogResult result = aleart.ShowDialog();
        }
        static public void showAleartPreExistingData(string message = "هذه البيانات موجوده سابقأ ")
        {
            AleartError aleart = new AleartError(message, MessageType.Error);
            DialogResult result = aleart.ShowDialog();
        }

        static public DialogResult showAleartConfirmation(string message)
        {
            AleartError aleart = new AleartError(message, MessageType.Question);
            DialogResult result = aleart.ShowDialog();
            return result;
        }
        static public void showEntityValidationErrors(DbEntityValidationException ex)
        {
            foreach (var item in ex.EntityValidationErrors)
            {
                foreach (var item1 in item.ValidationErrors)
                {
                    AppDialogAleart.showAleartNoPermissions("PropertyName" + item1.PropertyName + "ErrorMessage" + item1.ErrorMessage);
                }
            }
        }

    }
    public enum MessageType
    {
        Error,
        Warning,
        Information,
        Question,
        Success,
        NoDataSpecified
    }
}
