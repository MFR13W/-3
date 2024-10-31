using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3_практос
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private int[,] matrix; // Матрица для хранения данных
        private int M, N; // Размеры матрицы

        private void CreateMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение размеров матрицы
            try
            {
                M = int.Parse(MRowsTB.Text);
                N = int.Parse(MColsTB.Text);
            }
            catch (System.FormatException)
            {
                 MessageBox.Show("Вы не ввели числа, пожалуйста введите их");
            }
            // Создание матрицы
            matrix = new int[M, N];

            // Заполнение матрицы случайными числами
            Random random = new Random();
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = random.Next(-10, 11); // Случайные числа от -10 до 10
                }
            }

            // Отображение матрицы в DataGrid
            MatrixDataGrid.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
        }

        private void FindRowButton_Click(object sender, RoutedEventArgs e)
        {
            // Поиск первой строки с равным количеством положительных и отрицательных элементов
            int rowNumber = FindRowWithEqualCounts(matrix, M, N);

            // Вывод результата
            RTBl.Text = $"Номер первой строки с равным количеством положительных и отрицательных элементов: {rowNumber}";
        }

        // Метод для поиска первой строки с равным количеством положительных и отрицательных элементов
        private int FindRowWithEqualCounts(int[,] matrix, int M, int N)
        {
            for (int i = 0; i < M; i++)
            {
                int positiveCount = 0;
                int negativeCount = 0;

                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        positiveCount++;
                    }
                    else if (matrix[i, j] < 0)
                    {
                        negativeCount++;
                    }
                }

                if (positiveCount == negativeCount)
                {
                    return i + 1; // Номер строки (начиная с 1)
                }
            }

            return 0; // Если таких строк нет
        }

        // Метод для генерации строки с рандомными числами через запятую
        private string GetRandomNumbersString(int[,] matrix, int M, int N)
        {
            StringBuilder numbersString = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < M * N; i++)
            {
                if (i > 0)
                {
                    numbersString.Append(", ");
                }
                numbersString.Append(random.Next(-10, 11)); // Рандомное число от -10 до 10
            }
            return numbersString.ToString();
        }
        private void Save_MenuItem_Click(object sender, RoutedEventArgs e) // Окно с сохранением файла
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == true) // Получаем путь к файлу
            {
                StreamWriter file = new StreamWriter(save.FileName);
                for (int i = 0; i < 0; i++)
                {
                    file.WriteLine(MRowsTB.Text);
                }
                file.Close();

                // Диалоговое окно после сохранения таблицы
                try
                {
                    File.WriteAllText(MRowsTB.Text, "данные");
                    MessageBox.Show("Файл успешно сохранен");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}");
                }
            }
        }

        private void Open_MenuItem_Click(object sender, RoutedEventArgs e) // Окно с открытием файла
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* | Текстовые файлы | *.txt";
            open.FilterIndex = 2;
            open.Title = "Открытие таблицы";

            if (open.ShowDialog() == true) // Получаем путь к файлу
            {
                using (StreamReader file = new StreamReader(open.FileName))
                {
                    MRowsTB.Text = file.ReadToEnd();
                }

                // Диалоговое окно с открытием файла
                try
                {
                    MessageBox.Show("Файл успешно открыт");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка открытия файла: {ex.Message}");
                }
            }
        }

        private void Button_Prog(object sender, RoutedEventArgs e) // Диалоговое окно и с условием задания
        {
            MessageBox.Show("Кашаев Кирилл ИСП-24 \r\n " +
            "Дана целочисленная матрица размера M × N. Найти номер первой из ее строк, содержащих равное количество положительных и отрицательных элементов (нулевые элементы матрицы не учитываются)." +
            " Если таких строк нет, то вывести 0.", "О проге");
        }

        private void Button_Exit(object sender, RoutedEventArgs e) // Закрытие практического задания
        {
            Close();
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            MRowsTB.Text = "";
            MColsTB.Text = "";
            MatrixDataGrid.ItemsSource = null;
            RTBl.Text = "";
        }
    }
}