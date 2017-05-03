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
        public Event selectedEvent = Application.Current.Properties["selectedEvent"] as Event;
        public Event editedEvent = Application.Current.Properties["selectedEvent"] as Event;
        public EditEvent()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
            string eventName = tbxEventName.Text;
            DateTime day = dpDay.SelectedDate.Value;
            string startTime = tbxStartTime.Text;
            string endTime = tbxEndTime.Text;
            string colour = cbxColour.SelectedValue.ToString();
            string description = tbxDescription.Text;

            editedEvent.EventName = eventName;
            editedEvent.Day = day;
            editedEvent.StartTime = startTime;
            editedEvent.EndTime = endTime;
            editedEvent.Colour = colour;
            editedEvent.EventDescription = description;

            MainWindow main = this.Owner as MainWindow;

            if (editedEvent != selectedEvent)
            {
                db.Events.Remove(selectedEvent);
                db.Events.Add(editedEvent);
            }
            
            this.Close();
        }
    }
}
