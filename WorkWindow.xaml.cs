using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RelationalAlgebra
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        public WorkWindow()
        {
            InitializeComponent();
        }
        string tableName = null;
        int columnCount = 0;
        private string currentOperation;
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void ImgClose_MouseDown(object sender, RoutedEventArgs e) => Close();
        private void ImgPollUp_MouseDown(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void BtnOpenInfo_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = new InfoWindow();
            infoWindow.Show();
        }
        //Приоритеты действий
        private static readonly Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>
        {
            { "𝖴", 1 },
            { "∩", 1 },
            { "×", 2 },
            { "σ", 2 },
            { "п", 3 },
            { "⋈", 4 },
            { "⟕", 4 },
            { "⟖", 4 },
            { "⟗", 4 },
            { "⋉", 5 },
            { "∧", 6 },
            { "∨", 7 }
        };
        // Метод для преобразования инфиксной нотации в обратную польскую нотацию
        public static string ConvertToRPN(string infixExpression)
        {
            Stack<string> operatorStack = new Stack<string>();
            StringBuilder output = new StringBuilder();

            foreach (char token in infixExpression)
            {
                if (Char.IsLetterOrDigit(token))
                {
                    output.Append(token);
                }
                else if (token == '(')
                {
                    operatorStack.Push(token.ToString());
                }
                else if (token == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                    {
                        output.Append(" ");
                        output.Append(operatorStack.Pop());
                    }

                    if (operatorStack.Count > 0 && operatorStack.Peek() == "(")
                    {
                        operatorStack.Pop();
                    }
                }
                else
                {
                    while (operatorStack.Count > 0 && OperatorPrecedence[operatorStack.Peek()] >= OperatorPrecedence[token.ToString()])
                    {
                        output.Append(" ");
                        output.Append(operatorStack.Pop());
                    }

                    operatorStack.Push(token.ToString());
                }
            }
            while (operatorStack.Count > 0)
            {
                output.Append(" ");
                output.Append(operatorStack.Pop());
            }

            return output.ToString();
        }
        //Создание новой таблицы
        private void AddTab(DataTable dataTable)
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.ItemsSource = dataTable.DefaultView;

            TabItem tabItem = new TabItem
            {
                Header = dataTable.TableName,
                Content = dataGrid
            };

            MainTabControl.Items.Add(tabItem);
            MainTabControl.SelectedItem = tabItem;
        }
        //Добавление данных о новой таблице
        private void BtnAddTable_Click(object sender, RoutedEventArgs e)
        {
            AddTableWindow addTable = new AddTableWindow
            {
                Owner = this
            };
            addTable.ShowDialog();
            tableName = addTable.TableName;
            columnCount = addTable.ColumnCount;
            if(addTable.IsNullData)
            {
                MessageBox.Show("Пользователь прервал добавление");//////////////////////////////////////////////////////
            }
            // Создаем новую таблицу
            DataTable newTable = new DataTable(tableName);

            // Добавляем столбцы в новую таблицу
            for (int i = 0; i < columnCount; i++)
            {
                NameColumnWindow nameColumnWindow = new NameColumnWindow(tableName)
                {
                    Owner = this
                };
                nameColumnWindow.ShowDialog();
                string columnName = nameColumnWindow.NameTable;
                newTable.Columns.Add(columnName);
            }

            // Создаем новую вкладку
            TabItem newTabItem = new TabItem
            {
                Header = tableName,
                Content = new DataGrid // Используем DataGrid для отображения таблицы
                {
                    Name = tableName + "DataGrid",
                    ItemsSource = newTable.DefaultView // Используем DefaultView для привязки данных
                }
            };

            // Добавляем вкладку в MainTabControl
            MainTabControl.Items.Add(newTabItem);

            MessageBox.Show($"{tableName} с {columnCount} столбцов добавлена");
        }
        //Загрузка таблиц из файла
        private void BtnLoadTables_Click(object sender, RoutedEventArgs e)
        {
            if (MainTabControl.Items.Count > 0)
            {
                MessageBoxResult message = MessageBox.Show("Существующие данные будут удалены, продолжить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (message == MessageBoxResult.No)
                    return;
            }
            MainTabControl.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xls;*.xlsx"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                using (var stream = openFileDialog.OpenFile())
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.TableName = reader.Name;
                            // Чтение данных из Excel
                            try
                            {
                                while (reader.Read())
                                {
                                    if (reader.Depth == 0)
                                    {
                                        bool isHeaderRowEmpty = false;

                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            if (string.IsNullOrWhiteSpace(reader.GetValue(i)?.ToString()))
                                            {
                                                // Если хотя бы одна ячейка пустая, помечаем строку как пустую
                                                isHeaderRowEmpty = true;
                                                break;
                                            }
                                        }
                                        if (isHeaderRowEmpty)// Пропускаем обработку строки с пустой ячейкой
                                            continue;
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            if (string.IsNullOrWhiteSpace(reader.GetValue(i)?.ToString()))
                                            {
                                                // Пропускаем обработку столбца с пустой ячейкой
                                                continue;
                                            }
                                            dataTable.Columns.Add(reader.GetValue(i).ToString());
                                        }
                                    }
                                    else
                                    {
                                        DataRow row = dataTable.NewRow();
                                        for (int i = 0; i < Math.Min(reader.FieldCount, dataTable.Columns.Count); i++)
                                        {
                                            row[i] = reader.GetValue(i);
                                        }
                                        dataTable.Rows.Add(row);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Обработка исключения (например, если встретится объединенная ячейка)
                                MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ////////////////////////////////////////////////////
                                return; // Прерываем выполнение метода
                            }
                            AddTab(dataTable);
                        } while (reader.NextResult());
                    }
                }
            }
            MessageBox.Show("Пользователь прервал загрузку");///////////////////////////////////////////////////////////
        }
        //Сохранение всех таблиц
        private void BtnSaveTables_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        foreach (var tabItem in MainTabControl.Items)
                            if (tabItem is TabItem && ((TabItem)tabItem).Content is DataGrid)
                            {
                                var dataGrid = (DataGrid)((TabItem)tabItem).Content;
                                var dataView = (DataView)dataGrid.ItemsSource;
                                var dataTable = dataView.ToTable();

                                if (dataTable != null)
                                {
                                    var worksheet = workbook.Worksheets.Add(dataTable.TableName);
                                    // Запись заголовков столбцов
                                    for (int col = 1; col <= dataTable.Columns.Count; col++)
                                        worksheet.Cell(1, col).Value = dataTable.Columns[col - 1].ColumnName;
                                    // Запись данных
                                    for (int row = 0; row < dataTable.Rows.Count; row++)
                                    {
                                        for (int col = 1; col <= dataTable.Columns.Count; col++)
                                        {
                                            var cellValue = dataTable.Rows[row][col - 1];
                                            worksheet.Cell(row + 2, col).Value = GetXLCellValue(cellValue);
                                        }
                                    }
                                }
                            }
                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    // Обработка исключения (например, если встретится объединенная ячейка)
                    MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Прерываем выполнение метода
                }
            }
            else
                MessageBox.Show("Пользователь прервал сохранение"); ///////////////////////////////////////////
        }
        //Получение нужного типа данных
        private XLCellValue GetXLCellValue(object value)
        {
            if (value is int)
            {
                return (int)value;
            }
            else if (value is double)
            {
                return (double)value;
            }
            return value.ToString();
        }
        //Клик по кнопке операции
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Получаем текст из свойства Content кнопки
                string operation = button.Content?.ToString();

                // Добавляем текст к текущей операции
                currentOperation += operation;

                // Устанавливаем текущую операцию в TextBox
                TbxOperation.Text = currentOperation;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //получить отсортированные данные
            //реализовать операцию объеденение
        }
    }
}