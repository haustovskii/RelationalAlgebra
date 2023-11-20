using System.Windows;
using System.Windows.Input;

namespace RelationalAlgebra
{
    /// <summary>
    /// Логика взаимодействия для AddTableWindow.xaml
    /// </summary>
    public partial class AddTableWindow : Window
    {
        public AddTableWindow()
        {
            InitializeComponent();
        }
        public string TableName => TbxNameTable.Text;
        public bool IsNullData = false;
        // Свойство для количества столбцов
        public int ColumnCount
        {
            get
            {
                if (int.TryParse(TbxCountColumns.Text, out int count))
                    return count;
                return 0; // или другое значение по умолчанию
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void ImgClose_MouseDown(object sender, RoutedEventArgs e) => Close();
        private void ImgPollUp_MouseDown(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            IsNullData = true;
            this.Close();
        }
        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbxNameTable.Text)&&!string.IsNullOrWhiteSpace(TbxCountColumns.Text))
            {
                IsNullData = false;
                this.Close();
            }
        }
    }
}