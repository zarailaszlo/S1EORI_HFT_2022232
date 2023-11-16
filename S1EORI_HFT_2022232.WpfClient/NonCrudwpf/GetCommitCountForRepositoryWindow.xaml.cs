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
using System.Windows.Shapes;

namespace S1EORI_HFT_2022232.WpfClient.NonCrudwpf
{
    /// <summary>
    /// Interaction logic for GetCommitCountForRepositoryWindow.xaml
    /// </summary>
    public partial class GetCommitCountForRepositoryWindow : Window
    {
        public GetCommitCountForRepositoryWindow()
        {
            InitializeComponent();
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
    }
}
