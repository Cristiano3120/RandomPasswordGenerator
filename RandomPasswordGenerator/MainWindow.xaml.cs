using System.Windows;
using System.Windows.Controls;

namespace RandomPasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource? _cts;

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
            GUIHelper.SetBasicWindowUI(this, ParentGrid);
            InitBtns();
        }

        public void InitBtns()
        {
            GenerateBtn.Click += (_, _) =>
            {
                if (int.TryParse(PasswordLengthTextBox.Text, out int passwordLength))
                {
                    PasswordTextBox.Text = PasswordGenerator.GeneratePassword(_chars, passwordLength);
                    Clipboard.SetText(PasswordTextBox.Text);
                    _ = DisplayInfosAsync();
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
        }

        private void CheckBox_Click(object sender, RoutedEventArgs args)
        {
            CheckBox checkBox = (CheckBox)sender;
            Chars chars = Enum.Parse<Chars>((string)checkBox.Tag);
            _chars[chars] = checkBox.IsChecked == true;
        }

        public async Task DisplayInfosAsync(ushort showTime = 3000)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            ClipboardTextBox.Visibility = Visibility.Visible;

            try
            {
                await Task.Delay(showTime, token);
                ClipboardTextBox.Visibility = Visibility.Hidden;
            }
            catch (TaskCanceledException) { }
        }
    }
}