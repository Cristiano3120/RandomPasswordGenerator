using System.Windows;
using System.Windows.Controls;

namespace RandomPasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<Chars, bool> _chars = new()
        {
            { Chars.UpperCase, true },
            { Chars.LowerCase, true },
            { Chars.Digits, true },
            { Chars.Brackets, true },
            { Chars.CurlyBrackets, true },
            { Chars.Dollar, true },
            { Chars.ExclamationMark,true },
            { Chars.QuestionMark, true },
            { Chars.AtSign, true },
            { Chars.Hashtag, true },
            { Chars.Percent, true },
            { Chars.Dot, true },
            { Chars.And, true },
            { Chars.QuotationMark, true },
            { Chars.Euro, true }
        };

        public MainWindow()
        {
            InitializeComponent();
            ContolWindowState.RegisterWindowButtons(MinimizeBtn, CloseBtn);
            InitBtns();
        }

        public void InitBtns()
        {
            GenerateBtn.Click += (_, _) =>
            {
                if (int.TryParse(PasswordLengthTextBox.Text, out int passwordLength))
                {
                    PasswordTextBox.Text = PasswordGenerator.GeneratePassword(_chars, passwordLength);
                }
                else
                {
                    MessageBox.Show("Please enter a valid password length.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            PasswordLengthTextBox.PreviewTextInput += (_, args) =>
            {
                if (!args.Text.All(char.IsDigit) || PasswordLengthTextBox.Text.Length >= 2 
                    && PasswordLengthTextBox.SelectionLength == 0)
                {
                    args.Handled = true;
                }
            };
            CopyBtn.Click += CopyBtn_Click;
        }

        public void CopyBtn_Click(object sender, RoutedEventArgs args)
            => Clipboard.SetText(PasswordTextBox.Text);

        private void CheckBox_Click(object sender, RoutedEventArgs args)
        {
            CheckBox checkBox = (CheckBox)sender;
            Chars chars = Enum.Parse<Chars>((string)checkBox.Tag);
            _chars[chars] = checkBox.IsChecked == true;
        }
    }
}