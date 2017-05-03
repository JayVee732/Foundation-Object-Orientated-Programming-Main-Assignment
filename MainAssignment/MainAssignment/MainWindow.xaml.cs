using System;
using System.Collections.Generic;
using System.IO;
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

namespace MainAssignment
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            calDisplay.FirstDayOfWeek = DayOfWeek.Monday;
            calDisplay.IsTodayHighlighted = true;
            calDisplay.SelectedDates.Add(new DateTime(2017, 3, 5));

            DayText d1 = new DayText { Year = 2017, 5, 3, Text = "test1!"};



            //using (StreamReader sr = new StreamReader("testDates.txt"))
            //{
            //    string day = sr.ReadLine();
            //    while (day != null)
            //    {
            //        string[] newDay = day.Split(',');
            //        string checkDay = newDay[0];                    
            //    }
            //    sr.Close();
            //}
        }

        private void calDisplay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDateTextBox.Text = calDisplay.SelectedDate.ToString();
        }
    }
}
    