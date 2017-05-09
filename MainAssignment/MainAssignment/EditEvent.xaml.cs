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
    /// Once the user has selected an event, this window will open with the selected
    /// event within all of the textboxes and combo boxes.
    /// Once the user has selected the Save button, the event will get updated with
    /// the new information and saved to the database.
    /// </summary>
    public partial class EditEvent : Window
    {
        //Variables used within the window
        CalendarData2017Entities db = new CalendarData2017Entities();
        public enum EventColour { Red, Green, Yellow, Blue };
        //The selected event
        public Event selectedEvent = Application.Current.Properties["selectedEvent"] as Event;

        public EditEvent()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Binds the emun to the combobox
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
                //Fills in the text boxes and combo box with the selected event
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
                //If the user somehow brakes it...
                MessageBox.Show("No event selected");
                this.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Finds the event in the database with the same EventID
            var query = from ev in db.Events
                        where ev.EventID == selectedEvent.EventID
                        select ev;
            //Foreach is used so that the same EventID can be used instead
            //of creating a whole new event with a seperate EventID
            foreach (Event ev in query)
            {
                //Parsing the startTime and endTime so they can be saved to the database
                string startTime = tbxStartTime.Text;
                TimeSpan st = TimeSpan.Parse(startTime);

                string endTime = tbxEndTime.Text;
                TimeSpan et = TimeSpan.Parse(endTime);

                ev.EventName = tbxEventName.Text;
                ev.Day = dpDay.SelectedDate.Value;
                ev.StartTime = TimeSpan.Parse(st.ToString(@"hh\:mm"));
                ev.EndTime = TimeSpan.Parse(et.ToString(@"hh\:mm"));
                ev.Colour = cbxColour.SelectedValue.ToString();
                ev.EventDescription = tbxDescription.Text;
            }
            //Saves the changes
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
