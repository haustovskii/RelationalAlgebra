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
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();
        }
        private void ImgClose_MouseDown(object sender, RoutedEventArgs e) => Close();
        private void ImgPollUp_MouseDown(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private bool isClicked = false;
        private void BrdTumb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isClicked)
            {
                // Первый клик
                ElpPozition.Margin = new Thickness(25, 1, 0, 1);
                TblTypeInfo.Text = "Руководство пользователя";
                BrdTumb.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAF4E4"));
            }
            else
            {
                // Второй клик
                ElpPozition.Margin = new Thickness(0, 1, 25, 1);
                TblTypeInfo.Text = "Теоретический материал";
                BrdTumb.Background = Brushes.White;
            }

            // Инвертируем состояние клика
            isClicked = !isClicked;
        }
    }
}
