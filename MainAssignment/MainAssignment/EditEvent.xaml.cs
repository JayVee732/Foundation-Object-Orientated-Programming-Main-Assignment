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

namespace MainAssignment
{
    /// <summary>
    /// Interaction logic for EditEvent.xaml
    /// </summary>
    public partial class EditEvent : Window
    {
        CalendarData2017Entities db = new CalendarData2017Entities();
        public enum EventColour { Red, Green, Yellow, Blue };
        public Event selectedEvent = Application.Current.Properties["selectedEvent"] as Event;

        public EditEvent()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cbxColour.ItemsSource = Enum.GetNames(typeof(EventColour));
            cbxColour.SelectedValue = selectedEvent.Colour;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedEvent != null)
                {
                    tbxEventName.Text = selectedEvent.EventName;
                    dpDay.SelectedDate = selectedEvent.Day;
                    tbxStartTime.Text = selectedEvent.StartTime.ToString();
                    tbxEndTime.Text = selectedEvent.EndTime.ToString();
                    cbxColour.Text = selectedEvent.Colour;
                    tbxDescription.Text = selectedEvent.EventDescription;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No event selected");
                this.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var query = from ev in db.Events
                        where ev.EventID == selectedEvent.EventID
                        select ev;

            foreach (Event ev in query)
            {
                //ev.EventName = tbxEventName.Text;
                //ev.Day = dpDay.SelectedDate.Value;
                //ev.StartTime = tbxStartTime.Text;
                //ev.EndTime = tbxEndTime.Text;
                //ev.Colour = cbxColour.SelectedValue.ToString();
                //ev.EventDescription = tbxDescription.Text;
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            this.Close();
        }
    }
}
