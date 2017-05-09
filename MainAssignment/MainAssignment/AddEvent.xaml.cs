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
    /// Once the user selects the "Add" button on the main window, this window
    /// will appear, allowing the user to add their own event to the calendar.
    /// When the user selects the "Save" button, the event is added to the
    /// database.
    /// </summary>
    public partial class AddEvent : Window
    {
        //Variables used within the window
        CalendarData2017Entities db = new CalendarData2017Entities();
        public enum EventColour { Red, Green, Yellow, Blue };

        public AddEvent()
        {
            //Sets the window to load in the centre of the screen
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Binds emun list to combobox
            cbxColour.ItemsSource = Enum.GetNames(typeof(EventColour));
            cbxColour.SelectedIndex = 0;
            dpDay.SelectedDate = DateTime.Now;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Parses startTime and endTime to be stored in the database
            string startTime = tbxStartTime.Text;
            TimeSpan st = TimeSpan.Parse(startTime);

            string endTime = tbxEndTime.Text;
            TimeSpan et = TimeSpan.Parse(endTime);
            //Creates a new event for the database with a unique EventID
            Event newEvent = new Event()
            {
                EventName = tbxEventName.Text,
                Day = dpDay.SelectedDate.Value,
                StartTime = TimeSpan.Parse(st.ToString(@"hh\:mm")),
                EndTime = TimeSpan.Parse(et.ToString(@"hh\:mm")),
                Colour = cbxColour.SelectedValue.ToString(),
                EventDescription = tbxDescription.Text
            };

            try
            {
                //Adds the event to the database
                db.Events.Add(newEvent);
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
