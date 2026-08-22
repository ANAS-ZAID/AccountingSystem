using System;
using System.Drawing;
using System.Windows.Forms;

namespace AccountingSystem.core.Database.Seed
{
    /// <summary>
    /// Displays the one-time administrator credentials created by the database seeder.
    /// The values are read-only but selectable, and each value can be copied separately
    /// or together using the dedicated copy buttons.
    /// </summary>
    internal sealed class SeedCredentialsDialog : Form
    {
        private readonly string _loginName;
        private readonly string _password;
        private readonly TextBox _loginNameTextBox;
        private readonly TextBox _passwordTextBox;
        private readonly Label _copyStatusLabel;

        public SeedCredentialsDialog(string loginName, string password)
        {
            _loginName = loginName ?? string.Empty;
            _password = password ?? string.Empty;

            Text = "بيانات الدخول الأولية";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ClientSize = new Size(560, 355);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            var titleLabel = new Label
            {
                AutoSize = false,
                Text = "تم إنشاء حساب مدير النظام بنجاح",
                Font = new Font(Font.FontFamily, 14F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 52
            };

            var noteLabel = new Label
            {
                AutoSize = false,
                Text = "احفظ بيانات الدخول قبل إغلاق هذه النافذة. لن يتم عرض كلمة المرور المؤقتة مرة أخرى.",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 48,
                Padding = new Padding(18, 4, 18, 4)
            };

            var contentPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 150,
                ColumnCount = 3,
                RowCount = 2,
                Padding = new Padding(18, 12, 18, 8),
                RightToLeft = RightToLeft.Yes
            };
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            contentPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            contentPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            var loginLabel = CreateFieldLabel("اسم المستخدم:");
            var passwordLabel = CreateFieldLabel("كلمة المرور:");

            _loginNameTextBox = CreateCredentialTextBox(_loginName);
            _passwordTextBox = CreateCredentialTextBox(_password);

            var copyLoginButton = CreateCopyButton("نسخ");
            copyLoginButton.Click += delegate { CopyToClipboard(_loginName, "تم نسخ اسم المستخدم"); };

            var copyPasswordButton = CreateCopyButton("نسخ");
            copyPasswordButton.Click += delegate { CopyToClipboard(_password, "تم نسخ كلمة المرور"); };

            contentPanel.Controls.Add(loginLabel, 0, 0);
            contentPanel.Controls.Add(_loginNameTextBox, 1, 0);
            contentPanel.Controls.Add(copyLoginButton, 2, 0);
            contentPanel.Controls.Add(passwordLabel, 0, 1);
            contentPanel.Controls.Add(_passwordTextBox, 1, 1);
            contentPanel.Controls.Add(copyPasswordButton, 2, 1);

            var actionsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 52,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(18, 5, 18, 5),
                WrapContents = false
            };

            var copyAllButton = new Button
            {
                Text = "نسخ بيانات الدخول كاملة",
                AutoSize = false,
                Width = 195,
                Height = 36,
                UseVisualStyleBackColor = true
            };
            copyAllButton.Click += delegate
            {
                string credentials = "اسم المستخدم: " + _loginName + Environment.NewLine +
                                     "كلمة المرور: " + _password;
                CopyToClipboard(credentials, "تم نسخ بيانات الدخول كاملة");
            };

            var closeButton = new Button
            {
                Text = "إغلاق",
                DialogResult = DialogResult.OK,
                AutoSize = false,
                Width = 100,
                Height = 36,
                UseVisualStyleBackColor = true
            };

            actionsPanel.Controls.Add(closeButton);
            actionsPanel.Controls.Add(copyAllButton);

            _copyStatusLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                ForeColor = SystemColors.ControlText,
                Padding = new Padding(10, 4, 10, 8)
            };

            AcceptButton = closeButton;
            CancelButton = closeButton;

            Controls.Add(_copyStatusLabel);
            Controls.Add(actionsPanel);
            Controls.Add(contentPanel);
            Controls.Add(noteLabel);
            Controls.Add(titleLabel);
        }

        private static Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false,
                Margin = new Padding(4, 8, 4, 8)
            };
        }

        private static TextBox CreateCredentialTextBox(string value)
        {
            var textBox = new TextBox
            {
                Text = value,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.No,
                TextAlign = HorizontalAlignment.Left,
                Margin = new Padding(4, 10, 4, 10),
                ShortcutsEnabled = true
            };

            textBox.Enter += delegate { textBox.SelectAll(); };
            textBox.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                if (textBox.SelectionLength == 0)
                {
                    textBox.SelectAll();
                }
            };

            return textBox;
        }

        private static Button CreateCopyButton(string text)
        {
            return new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(4, 8, 4, 8),
                UseVisualStyleBackColor = true
            };
        }

        private void CopyToClipboard(string value, string successMessage)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Clipboard.SetText(value);
                }

                _copyStatusLabel.Text = successMessage;
            }
            catch (Exception ex)
            {
                _copyStatusLabel.Text = "تعذر النسخ إلى الحافظة: " + ex.Message;
            }
        }
    }
}
