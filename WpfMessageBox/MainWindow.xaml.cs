using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMessageBox {
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var result = NativeAPI.MessageBox(IntPtr.Zero, "hello world 你好", "Test 測試", MessageBoxFlag.YesNo | MessageBoxFlag.IconImformation);

            switch (result) {
                case MessageBoxReturn.Yes:
                    textBlock1.Text = "Yes";
                    break;
                case MessageBoxReturn.No:
                    textBlock1.Text = "No";
                    break;
                default:
                    textBlock1.Text = "";
                    break;
            }
        }
    }
}
