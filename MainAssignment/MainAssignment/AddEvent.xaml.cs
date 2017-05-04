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
            Event newEvent = new Event()
            {
                //EventName = tbxEventName.Text,
                //Day = dpDay.SelectedDate.Value,
                //StartTime = tbxStartTime.Text,
                //EndTime = tbxEndTime.Text,
                //Colour = cbxColour.SelectedValue.ToString(),
                //EventDescription = tbxDescription.Text
            };

            db.Events.Add(newEvent);
            db.SaveChanges();
            this.Close();
        }
    }
}
