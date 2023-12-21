using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
        int columnCount = 0, i = 0;
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
        private static readonly Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>//Приоритеты действий
        {
            { "σ", 1 },  // селекция
            { "п", 1 },  // проекция
            { "×", 2 },  // декартово произведение
            { "⋈", 2 },  // соединение
            { "∩", 2 },  // пересечение
            { "/", 2 },  // деление
            { "∪", 3 },  // объединение
        };
        private void AddTab(DataTable dataTable)//Создание новой таблицы
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
        private void BtnAddTable_Click(object sender, RoutedEventArgs e) //Добавление данных о новой таблице
        {
            AddTableWindow addTable = new AddTableWindow();
            addTable.ShowDialog();
            tableName = addTable.TableName;
            columnCount = addTable.ColumnCount;
            if (!addTable.IsNullData)
            {
                MessageBox.Show("Пользователь прервал добавление", "Добавление новой таблицы", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DataTable newTable = new DataTable(tableName); // Создаем новую таблицу           
            for (int i = 0; i < columnCount; i++) // Добавляем столбцы в новую таблицу
            {
                NameColumnWindow nameColumnWindow = new NameColumnWindow(tableName);
                nameColumnWindow.ShowDialog();
                string columnName = nameColumnWindow.NameTable;
                newTable.Columns.Add(columnName);
            }
            TabItem newTabItem = new TabItem// Создаем новую вкладку
            {
                Header = tableName,
                Content = new DataGrid // Используем DataGrid для отображения таблицы
                {
                    Name = tableName + "DataGrid",
                    ItemsSource = newTable.DefaultView // Используем DefaultView для привязки данных
                }
            };
            MainTabControl.Items.Add(newTabItem); // Добавляем вкладку в MainTabControl
        }
        private void BtnLoadTables_Click(object sender, RoutedEventArgs e)//Загрузка таблиц из файла
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
                                            if (string.IsNullOrWhiteSpace(reader.GetValue(i)?.ToString()))
                                            {
                                                // Если хотя бы одна ячейка пустая, помечаем строку как пустую
                                                isHeaderRowEmpty = true;
                                                break;
                                            }
                                        if (isHeaderRowEmpty)// Пропускаем обработку строки с пустой ячейкой
                                            continue;
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            if (string.IsNullOrWhiteSpace(reader.GetValue(i)?.ToString()))
                                                continue; // Пропускаем обработку столбца с пустой ячейкой
                                            dataTable.Columns.Add(reader.GetValue(i).ToString());
                                        }
                                    }
                                    else
                                    {
                                        DataRow row = dataTable.NewRow();
                                        for (int i = 0; i < Math.Min(reader.FieldCount, dataTable.Columns.Count); i++)
                                            row[i] = reader.GetValue(i);
                                        dataTable.Rows.Add(row);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            AddTab(dataTable);
                        } while (reader.NextResult());
                    }
                }
            else
            {
                MessageBox.Show("Пользователь прервал загрузку", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
        private void BtnSaveTables_Click(object sender, RoutedEventArgs e)//Сохранение всех таблиц
        {
            if (MainTabControl.Items.Count == 0)
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
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
                    MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);// Обработка исключения (например, если встретится объединенная ячейка)
                    return; // Прерываем выполнение метода
                }
            else
                MessageBox.Show("Пользователь прервал сохранение"); ///////////////////////////////////////////
        }
        private XLCellValue GetXLCellValue(object value)//Получение нужного типа данных
        {
            if (value is int)
                return (int)value;
            else if (value is double)
                return (double)value;
            return value.ToString();
        }
        private void OperationButton_Click(object sender, RoutedEventArgs e)//Клик по кнопке операции
        {
            if (sender is Button button)
            {
                string operation = button.Content?.ToString(); // Получаем текст из свойства Content кнопки              
                TbxOperation.Text += operation;
            }
        }
        private void BtnRelationalOperation_Click(object sender, RoutedEventArgs e)//Событие кнопки для вычисления операции
        {
            if (MainTabControl.Items.Count > 0 && !string.IsNullOrWhiteSpace(TbxOperation.Text))
            {
                try
                {
                    string operationText = TbxOperation.Text;
                    // Проверяем наличие всех таблиц, участвующих в операции и соответствие требований к операциям
                    if (!IsOperationValid(operationText))
                        throw new ArgumentException("Одна или несколько таблиц, участвующих в операции, отсутствуют в списке.");
                    PerformRelationalOperation(operationText);//Вычисление данных                       
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Нет данных для обработки или выражение отсутствует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsOperationValid(string operationText)
        {
            var tabNames = MainTabControl.Items.OfType<TabItem>().Select(tab => tab.Header.ToString());
            // Регулярное выражение для операций с одной таблицей
            var regexSingleTableOperation = new Regex(@"([пσ])\(([^)]+?)\)\(([^)]+?)\)");
            // Регулярное выражение для операций с двумя таблицами
            var regexSetOperation = new Regex(@"([^\s∪×⋈∩/]+)\s*([∪×⋈∩/]+)\s*([^\s∪×⋈∩/]+)");

            // Проверка операции с одной таблицей
            var matchSingleTable = regexSingleTableOperation.Match(operationText);
            if (matchSingleTable.Success)
            {
                var operation = matchSingleTable.Groups[1].Value.Trim();
                var fields = matchSingleTable.Groups[2].Value.Split(',');

                if (OperatorPrecedence.ContainsKey(operation) && fields.Length < OperatorPrecedence[operation])
                    throw new ArgumentException($"Некорректное количество полей в операции {operation}. Ожидается не менее {OperatorPrecedence[operation]} полей.", "Ошибка");

                var tableName = matchSingleTable.Groups[3].Value.Trim();
                var foundTabItem = MainTabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.Header.ToString() == tableName);

                if (foundTabItem == null)
                    throw new ArgumentException($"Таблица {tableName} не найдена.");

                var dataGrid = (DataGrid)foundTabItem.Content;
                var dataView = dataGrid.ItemsSource as DataView;

                if (dataView == null)
                    throw new ArgumentException($"Ошибка: DataView для таблицы {tableName} не найден.", "Ошибка");

                var table = dataView.Table;

                if (!tabNames.Contains(tableName))
                    throw new ArgumentException($"Таблица {tableName} отсутствует в списке вкладок.", "Ошибка");

                if (table.Rows.Count == 0)
                    throw new ArgumentException($"Таблица {tableName} не содержит строк.", "Ошибка");

                if (operation == "σ")
                {
                    if (fields.Length != 1)
                        throw new ArgumentException("Некорректное количество полей в операции селекции. Ожидается ровно 1 поле для условия.");

                    var condition = fields[0].Trim();
                    var regexCondition = new Regex(@"\s*([^<>=!]+)\s*([<>=!]+)\s*(""[^""]+""|\S+)\s*");
                    var matchCondition = regexCondition.Match(condition);

                    if (!matchCondition.Success)
                        throw new ArgumentException("Некорректный формат условия селекции. Ожидается формат: поле оператор значение.");

                    var fieldName = matchCondition.Groups[1].Value.Trim();
                    var op = matchCondition.Groups[2].Value.Trim();
                    var value = matchCondition.Groups[3].Value.Trim();

                    // ... (код проверки условия)

                }
                else if (operation == "п")
                    foreach (var field in fields)
                    {
                        string trimmedField = field.Trim();
                        if (!table.Columns.Contains(trimmedField))
                            throw new ArgumentException($"Столбец {trimmedField} не найден в таблице {tableName}.");

                    }
                else
                    throw new ArgumentException($"Некорректная операция: {operationText}.");

            }
            // Проверка операции с двумя таблицами
            else
            {
                var matchSetOperation = regexSetOperation.Match(operationText);
                if (matchSetOperation.Success)
                {
                    var setOperation = matchSetOperation.Groups[2].Value.Trim();
                    if (setOperation == "∪" || setOperation == "×" || setOperation == "⋈" || setOperation == "∩" || setOperation == "/")
                    {
                        var table1Name = matchSetOperation.Groups[1].Value.Trim();
                        var table2Name = matchSetOperation.Groups[3].Value.Trim();

                        var table1 = GetDataTableByName(table1Name);
                        var table2 = GetDataTableByName(table2Name);

                        if (table1 == null || table2 == null)
                            throw new ArgumentException($"Одна из таблиц {table1Name}, {table2Name} не найдена.");
                        // Дополнительная логика для обработки этих операций
                    }
                    else
                        throw new ArgumentException($"Некорректная операция: {operationText}.");
                }
                else
                    throw new ArgumentException($"Некорректная операция: {operationText}.");
                // Проверка наличия только одной операции в выражении
                if (regexSetOperation.Matches(operationText).Count > 1)
                {
                    throw new ArgumentException("В выражении может быть только одна операция между таблицами.");
                }
            }
            return true;
        }
        private DataTable GetDataTableByName(string tableName)
        {
            var foundTabItem = MainTabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.Header.ToString() == tableName);
            return foundTabItem?.Content is DataGrid dataGrid ? (dataGrid.ItemsSource as DataView)?.Table : null;
        }
        private void PerformRelationalOperation(string operationText)
        {
            // Разделяем операцию на части
            string[] operationParts = operationText.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            if (operationParts.Length == 3)
            {
                // Проекция или селекция
                string operationName = operationParts[0];

                if (operationName == "п")
                {
                    // Если операция проекции
                    string[] projectionParts = operationParts[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    if (projectionParts.Length >= 1)
                    {
                        // Есть как минимум один аргумент в проекции
                        string tableName = operationParts[2].Trim(); // Третий элемент - название таблицы

                        // Проверим наличие таблицы в MainTabControl
                        if (MainTabControl.Items.OfType<TabItem>().Any(tab => tab.Header.ToString() == tableName))
                        {
                            // Получаем таблицу из вкладки
                            var tabItem = MainTabControl.Items.OfType<TabItem>().FirstOrDefault(tab => tab.Header.ToString() == tableName);
                            var dataGrid = (DataGrid)tabItem.Content;
                            var dataView = dataGrid.ItemsSource as DataView;

                            if (dataView == null)
                            {
                                throw new ArgumentException($"Ошибка: DataView для таблицы {tableName} не найден.", "Ошибка");
                            }

                            var table = dataView.Table;

                            // Проверяем наличие всех полей в таблице
                            foreach (var field in projectionParts) // Теперь начинаем с элемента №0, так как это поля проекции
                            {
                                string trimmedField = field.Trim();
                                if (!table.Columns.Contains(trimmedField))
                                {
                                    throw new ArgumentException($"Столбец {trimmedField} не найден в таблице {tableName}.");
                                }
                            }

                            // Выполняем операцию проекции
                            DataTable resultTable = PerformProjection(table, projectionParts);

                            // Создаем новую вкладку для результатов проекции
                            var resultTabItem = new TabItem();
                            resultTabItem.Header = $"R{i + 1}"; // i - это номер вкладки
                            i++; // Увеличиваем номер вкладки

                            // Создаем DataGrid для отображения результатов
                            var resultDataGrid = new DataGrid();
                            resultDataGrid.ItemsSource = resultTable.DefaultView;

                            // Добавляем DataGrid к содержимому вкладки
                            resultTabItem.Content = resultDataGrid;

                            // Добавляем вкладку к MainTabControl
                            MainTabControl.Items.Add(resultTabItem);

                            // Выбираем только что созданную вкладку
                            MainTabControl.SelectedItem = resultTabItem;
                        }
                        else
                        {
                            throw new ArgumentException($"Таблица {tableName} не найдена в списке.");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Некорректное количество аргументов в проекции.");
                    }
                }
                else if (operationName == "σ")
                {
                    // Если операция селекции
                    string condition = operationParts[1].Trim(); // Второй элемент - условие

                    // Проверяем, что условие не пусто
                    if (!string.IsNullOrEmpty(condition))
                    {
                        // Получаем таблицу из MainTabControl
                        var tabItem = MainTabControl.Items.OfType<TabItem>().FirstOrDefault();
                        var dataGrid = (DataGrid)tabItem.Content;
                        var dataView = dataGrid.ItemsSource as DataView;

                        if (dataView == null)
                            throw new ArgumentException("Ошибка: DataView для таблицы не найден.", "Ошибка");

                        var table = dataView.Table;

                        // Выполняем операцию селекции
                        DataTable resultTable = PerformSelection(table, condition);

                        // Создаем новую вкладку для результатов селекции
                        var resultTabItem = new TabItem();
                        resultTabItem.Header = $"R{i + 1}"; // i - это номер вкладки
                        i++; // Увеличиваем номер вкладки

                        // Создаем DataGrid для отображения результатов
                        var resultDataGrid = new DataGrid();
                        resultDataGrid.ItemsSource = resultTable.DefaultView;
                        resultTabItem.Content = resultDataGrid;

                        // Добавляем вкладку к MainTabControl
                        MainTabControl.Items.Add(resultTabItem);
                        MainTabControl.SelectedItem = resultTabItem;
                    }
                }
            }
            else if (operationParts.Length == 1)
            {
                string[] tableNames = operationParts[0].Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (tableNames.Length == 2)
                {
                    string tableName1 = tableNames[0].Trim();
                    string tableName2 = tableNames[1].Trim();
                    // Определить операцию между таблицами
                    string operation = DetermineTableOperation(operationText);

                    if (!string.IsNullOrEmpty(operation))
                    {
                        // Вызовите метод для обработки операции между таблицами
                        DataTable resultTable = PerformBinaryOperation(operation, tableName1, tableName2);

                        // Создаем новую вкладку для результатов селекции
                        var resultTabItem = new TabItem();
                        resultTabItem.Header = $"R{i + 1}"; // i - это номер вкладки
                        i++; // Увеличиваем номер вкладки

                        // Создаем DataGrid для отображения результатов
                        var resultDataGrid = new DataGrid();
                        resultDataGrid.ItemsSource = resultTable.DefaultView;
                        resultTabItem.Content = resultDataGrid;

                        // Добавляем вкладку к MainTabControl
                        MainTabControl.Items.Add(resultTabItem);
                        MainTabControl.SelectedItem = resultTabItem;
                    }
                    else
                    {
                        throw new ArgumentException("Некорректная операция между таблицами.");
                    }
                }
                else
                {
                    throw new ArgumentException("Некорректное количество таблиц для операции.");
                }
            }
            else
            {
                throw new ArgumentException("Некорректная структура операции.");
            }
        }
        // Метод для определения операции между двумя таблицами
        private string DetermineTableOperation(string operationText)
        {
            // Регулярное выражение для поиска операции между таблицами
            var regexTableOperation = new Regex(@"[∪×⋈∩/]");

            // Находим первую операцию между таблицами
            Match match = regexTableOperation.Match(operationText);

            if (match.Success)
            {
                return match.Value;
            }

            return null;
        }
        private DataTable PerformBinaryOperation(string operation, string tableName1, string tableName2)
        {
            DataTable table1 = GetDataTableByName(tableName1);
            DataTable table2 = GetDataTableByName(tableName2);

            if (table1 == null || table2 == null)
            {
                throw new ArgumentException($"Одна из таблиц {tableName1}, {tableName2} не найдена.");
            }

            switch (operation)
            {
                case "×": // Декартово произведение
                    return PerformCartesianProduct(table1, table2);
                case "⋈": // Соединение
                    return PerformJoin(table1, table2);
                case "∩": // Пересечение
                    return PerformIntersection(table1, table2);
                case "/": // Деление
                    return PerformDivision(table1, table2);
                case "∪": // Объединение
                    return PerformUnion(table1, table2);
                default:
                    throw new ArgumentException($"Некорректная операция между таблицами: {operation}");
            }
        }
        private DataTable PerformCartesianProduct(DataTable table1, DataTable table2)
        {
            // Ваш код для декартова произведения
            // Пример:
            DataTable resultTable = new DataTable();

            foreach (DataColumn col in table1.Columns)
            {
                resultTable.Columns.Add($"{table1.TableName}.{col.ColumnName}", col.DataType);
            }

            foreach (DataColumn col in table2.Columns)
            {
                resultTable.Columns.Add($"{table2.TableName}.{col.ColumnName}", col.DataType);
            }

            foreach (DataRow row1 in table1.Rows)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    DataRow resultRow = resultTable.NewRow();
                    foreach (DataColumn col in table1.Columns)
                    {
                        resultRow[$"{table1.TableName}.{col.ColumnName}"] = row1[col.ColumnName];
                    }
                    foreach (DataColumn col in table2.Columns)
                    {
                        resultRow[$"{table2.TableName}.{col.ColumnName}"] = row2[col.ColumnName];
                    }
                    resultTable.Rows.Add(resultRow);
                }
            }

            return resultTable;
        }
        private DataTable PerformJoin(DataTable table1, DataTable table2)
        {
            // Ваш код для соединения
            // Пример:
            DataColumn[] commonColumns = table1.Columns.OfType<DataColumn>()
                .Intersect(table2.Columns.OfType<DataColumn>(), DataColumnComparer.Default)
                .ToArray();

            if (commonColumns.Length == 0)
            {
                throw new ArgumentException("Для соединения необходимо наличие общих столбцов.");
            }

            DataTable resultTable = new DataTable();

            foreach (DataColumn col in table1.Columns)
            {
                resultTable.Columns.Add($"{table1.TableName}.{col.ColumnName}", col.DataType);
            }

            foreach (DataColumn col in table2.Columns)
            {
                if (!commonColumns.Contains(col, DataColumnComparer.Default))
                {
                    resultTable.Columns.Add($"{table2.TableName}.{col.ColumnName}", col.DataType);
                }
            }

            foreach (DataRow row1 in table1.Rows)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    bool match = true;
                    foreach (DataColumn col in commonColumns)
                    {
                        if (!row1[col.ColumnName].Equals(row2[col.ColumnName]))
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        DataRow resultRow = resultTable.NewRow();
                        foreach (DataColumn col in table1.Columns)
                        {
                            resultRow[$"{table1.TableName}.{col.ColumnName}"] = row1[col.ColumnName];
                        }
                        foreach (DataColumn col in table2.Columns)
                        {
                            if (!commonColumns.Contains(col, DataColumnComparer.Default))
                            {
                                resultRow[$"{table2.TableName}.{col.ColumnName}"] = row2[col.ColumnName];
                            }
                        }
                        resultTable.Rows.Add(resultRow);
                    }
                }
            }

            return resultTable;
        }
        private DataTable PerformIntersection(DataTable table1, DataTable table2)
        {
            // Ваш код для пересечения
            // Пример:
            DataColumn[] commonColumns = table1.Columns.OfType<DataColumn>()
                .Intersect(table2.Columns.OfType<DataColumn>(), DataColumnComparer.Default)
                .ToArray();

            if (commonColumns.Length == 0)
            {
                throw new ArgumentException("Для пересечения необходимо наличие общих столбцов.");
            }

            DataTable resultTable = new DataTable();

            foreach (DataColumn col in commonColumns)
            {
                resultTable.Columns.Add(col.ColumnName, col.DataType);
            }

            foreach (DataRow row1 in table1.Rows)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    bool match = true;
                    foreach (DataColumn col in commonColumns)
                    {
                        if (!row1[col.ColumnName].Equals(row2[col.ColumnName]))
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        DataRow resultRow = resultTable.NewRow();
                        foreach (DataColumn col in commonColumns)
                        {
                            resultRow[col.ColumnName] = row1[col.ColumnName];
                        }
                        resultTable.Rows.Add(resultRow);
                    }
                }
            }

            return resultTable;
        }
        private DataTable PerformDivision(DataTable table1, DataTable table2)
        {
            // Ваш код для деления
            // Пример:
            DataColumn[] commonColumns = table1.Columns.OfType<DataColumn>()
                .Intersect(table2.Columns.OfType<DataColumn>(), DataColumnComparer.Default)
                .ToArray();

            if (commonColumns.Length == 0)
            {
                throw new ArgumentException("Для деления необходимо наличие общих столбцов.");
            }

            DataTable resultTable = new DataTable();

            foreach (DataColumn col in table1.Columns)
            {
                if (!commonColumns.Contains(col, DataColumnComparer.Default))
                {
                    resultTable.Columns.Add($"{table1.TableName}.{col.ColumnName}", col.DataType);
                }
            }

            foreach (DataRow row1 in table1.Rows)
            {
                bool existsInTable2 = table2.AsEnumerable().Any(row2 =>
                {
                    foreach (DataColumn col in commonColumns)
                    {
                        if (!row1[col.ColumnName].Equals(row2[col.ColumnName]))
                        {
                            return false;
                        }
                    }
                    return true;
                });

                if (!existsInTable2)
                {
                    DataRow resultRow = resultTable.NewRow();
                    foreach (DataColumn col in resultTable.Columns)
                    {
                        resultRow[col.ColumnName] = row1[col.ColumnName];
                    }
                    resultTable.Rows.Add(resultRow);
                }
            }

            return resultTable;
        }
        private DataTable PerformUnion(DataTable table1, DataTable table2)
        {
            // Ваш код для объединения
            // Пример:
            DataTable resultTable = table1.Clone();

            foreach (DataRow row in table2.Rows)
            {
                resultTable.ImportRow(row);
            }

            return resultTable;
        }
        // Класс для сравнения DataColumn
        public class DataColumnComparer : IEqualityComparer<DataColumn>
        {
            public static readonly DataColumnComparer Default = new DataColumnComparer();

            public bool Equals(DataColumn x, DataColumn y)
            {
                return x.ColumnName == y.ColumnName && x.DataType == y.DataType;
            }

            public int GetHashCode(DataColumn obj)
            {
                return obj.ColumnName.GetHashCode() ^ obj.DataType.GetHashCode();
            }
        }
        //Операция Проекции
        private DataTable PerformProjection(DataTable table, string[] fields)
        {
            // Создаем новую таблицу для результата проекции
            DataTable resultTable = new DataTable();

            // Добавляем столбцы в новую таблицу
            foreach (var field in fields)
            {
                string trimmedField = field.Trim();
                resultTable.Columns.Add(trimmedField, table.Columns[trimmedField].DataType);
            }

            // Копируем данные из исходной таблицы в новую таблицу
            foreach (DataRow row in table.Rows)
            {
                DataRow newRow = resultTable.NewRow();
                foreach (var field in fields)
                {
                    string trimmedField = field.Trim();
                    newRow[trimmedField] = row[trimmedField];
                }
                resultTable.Rows.Add(newRow);
            }

            return resultTable;
        }
        //Операция Селекции
        private DataTable PerformSelection(DataTable table, string condition)
        {
            // Преобразуем кавычки в одинарные кавычки
            condition = condition.Replace("\"", "'");

            // Создаем выражение фильтрации
            var filteredRows = table.AsEnumerable().Where(row => EvaluateRow(row, condition)).ToArray();

            // Проверяем, что есть отфильтрованные строки
            if (filteredRows.Length == 0)
            {
                throw new ArgumentException("Отфильтрованные данные пусты. Проверьте условие фильтрации.");
            }

            // Создаем новую таблицу на основе отфильтрованных данных
            DataTable resultTable = filteredRows.CopyToDataTable();

            return resultTable;
        }
        private bool EvaluateRow(DataRow row, string condition)
        {
            // Парсим условие на части (поле, оператор, значение)
            var parts = condition.Split('=');

            if (parts.Length == 2)
            {
                // Левая часть выражения
                var columnName = parts[0].Trim();
                // Правая часть выражения
                var value = parts[1].Trim();

                // Убеждаемся, что у нас есть такая колонка
                if (row.Table.Columns.Contains(columnName))
                {
                    // Получаем значение из ячейки
                    var cellValue = row[columnName];

                    // Сравниваем значения, учитывая тип данных в столбце
                    if (cellValue is string stringValue)
                    {
                        // Если это строка, сравниваем с учетом регистра
                        return string.Equals(stringValue, value, StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        // Если это не строка, используем стандартное сравнение
                        return object.Equals(cellValue, value);
                    }
                }
                else
                {
                    throw new ArgumentException($"Столбец {columnName} не найден в таблице.");
                }
            }
            else
            {
                throw new ArgumentException($"Некорректный формат условия: {condition}");
            }
        }
    }
}