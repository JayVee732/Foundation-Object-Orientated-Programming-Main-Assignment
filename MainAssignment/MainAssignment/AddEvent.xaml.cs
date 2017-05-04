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
    /// Interaction logic for AddEvent.xaml
    /// </summary>
    public partial class AddEvent : Window
    {
        CalendarData2017Entities db = new CalendarData2017Entities();
        public enum EventColour { Red, Green, Yellow, Blue };

        public AddEvent()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
            string startTime = tbxStartTime.Text;
            TimeSpan st = TimeSpan.Parse(startTime);

            string endTime = tbxEndTime.Text;
            TimeSpan et = TimeSpan.Parse(endTime);

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
