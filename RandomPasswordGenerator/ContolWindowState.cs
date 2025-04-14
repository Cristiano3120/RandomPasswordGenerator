using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows;

namespace RandomPasswordGenerator
{
    public static partial class ContolWindowState
    {
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_RESTORE = 0xF120;
        private const int SC_CLOSE = 0xF060;

        [LibraryImport("user32.dll", EntryPoint = "SendMessageW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public static void RegisterWindowButtons(Button minimizeButton, Button closeButton)
        {
            minimizeButton.Click += MinimizeButton_Click;
            closeButton.Click += CloseButton_Click;
        }

        private static void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((Button)sender);
            nint windowHandle = new WindowInteropHelper(window).Handle;

            int wParam = window.WindowState == WindowState.Maximized
                ? SC_RESTORE
                : SC_MAXIMIZE;

            SendMessage(windowHandle, WM_SYSCOMMAND, wParam, IntPtr.Zero);
        }

        private static void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((Button)sender);
            nint windowHandle = new WindowInteropHelper(window).Handle;
            SendMessage(windowHandle, WM_SYSCOMMAND, SC_MINIMIZE, IntPtr.Zero);
        }

        private static void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((Button)sender);
            nint windowHandle = new WindowInteropHelper(window).Handle;
            SendMessage(windowHandle, WM_SYSCOMMAND, SC_CLOSE, IntPtr.Zero);
        }
    }
}
