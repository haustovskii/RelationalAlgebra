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

namespace RelationalAlgebra
{
    /// <summary>
    /// Логика взаимодействия для NameColumnWindow.xaml
    /// </summary>
    public partial class NameColumnWindow : Window
    {
        public NameColumnWindow(string NameTable)
        {
            InitializeComponent();
            TblName.Text = $"Таблица {NameTable}";
        }
        public string NameTable => TbxNameTable.Text;
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void ImgClose_MouseDown(object sender, RoutedEventArgs e) => Close();
        private void ImgPollUp_MouseDown(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    }
}
