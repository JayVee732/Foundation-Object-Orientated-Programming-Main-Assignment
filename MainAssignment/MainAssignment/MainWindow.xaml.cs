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
        CalendarData2017Entities db = new CalendarData2017Entities();
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            calDisplay.FirstDayOfWeek = DayOfWeek.Monday;
            calDisplay.IsTodayHighlighted = true;
            calDisplay.SelectedDate = DateTime.Now;
        }

        private void calDisplay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = from ev in db.Events
                        where ev.Day == calDisplay.SelectedDate.Value
                        select ev;

            lbxEvents.ItemsSource = query.ToList();

            if (calDisplay.SelectedDate.HasValue)
            {
                DateTime date = calDisplay.SelectedDate.Value;
                this.Title = "Calendar - " + date.ToShortDateString();
            }
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            AddEvent addEv = new AddEvent();
            addEv.Owner = this;
            addEv.Show();
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = lbxEvents.SelectedValue as Event;

            if (selectedEvent != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this event?", "Delete Event?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    db.Events.Remove(selectedEvent);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Please select an event before deleting");
                }
            }
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = lbxEvents.SelectedItem as Event;
            try
            {
                if (selectedEvent != null)
                {
                    Application.Current.Properties["selectedEvent"] = selectedEvent;
                    EditEvent editEv = new EditEvent();
                    editEv.Owner = this;

                    editEv.Show();
                }
            }

            catch (Exception fe)
            {
                MessageBox.Show(fe.Message);
                throw;
            }
        }
    }
}
