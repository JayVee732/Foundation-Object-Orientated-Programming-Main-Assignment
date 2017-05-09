/*=====================================================
 * Program Name: Calendar Application
 * Author: Jamie Higgins - S00162685
 * Version: 1.0
 * -----------------------------------------
 * Program Purpose: This application allows the user to
 * view their calendar. Events can be viewed on a day-
 * by-day basis and can be edited. Events can also
 * be added.
 * 
 * Functionality: 
 * - Program doesn't auto-refresh a search query
 * 
 * - User needs to be sure that what they enter is correct for the 
 *   startTime and endTime in the "Add" and "Edit" menus or it
 *   won't work
 *   
 * - It isn't the prettiest application
 ====================================================*/
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
    /// A day can be selected from the calendar at the top of the program.
    /// The user can then choose to add, edit or delete an event.
    /// </summary>
    public partial class MainWindow : Window
    {
        //Variables used within all of the program
        CalendarData2017Entities db = new CalendarData2017Entities();
        public string[] FilterBy = { "A-Z", "Z-A" };
        public MainWindow()
        {
            InitializeComponent();
            //Centres the screen on startup
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the first day of the week to Monday and highlights today
            calDisplay.FirstDayOfWeek = DayOfWeek.Monday;
            calDisplay.IsTodayHighlighted = true;
            calDisplay.SelectedDate = DateTime.Now;
            //Sets the combobox to the array and selects the first option
            cbxFilter.ItemsSource = FilterBy;
            cbxFilter.SelectedIndex = 0;
        }

        private void calDisplay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //When a day is selected, the events are displayed in the listbox
            if (cbxFilter.SelectedIndex == 0)
            {
                var query = from ev in db.Events
                            where ev.Day == calDisplay.SelectedDate.Value
                            select ev;

                lbxEvents.ItemsSource = query.ToList();
            }
            else
            {
                var query = from ev in db.Events
                            where ev.Day == calDisplay.SelectedDate.Value
                            orderby ev.EventName descending
                            select ev;

                lbxEvents.ItemsSource = query.ToList();
            }

            //Changes the title of the program to reflect the day selected
            if (calDisplay.SelectedDate.HasValue)
            {
                DateTime date = calDisplay.SelectedDate.Value;
                this.Title = "Calendar - " + date.ToShortDateString();
            }
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            //Opens a new window for adding an event
            try
            {
                AddEvent addEv = new AddEvent();
                addEv.Owner = this;
                addEv.Show();
            }
            catch (Exception fe)
            {
                MessageBox.Show(fe.Message);
            }
        }

        private void btnDeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = lbxEvents.SelectedValue as Event;
            //When an event is selected
            if (selectedEvent != null)
            {
                //Askes the user if they're sure they want to remove the event
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this event?", "Delete Event?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    //Removes the event from the database
                    try
                    {
                        db.Events.Remove(selectedEvent);
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    //If the user decides to click "Delete" without selecting an event first
                    MessageBox.Show("Please select an event before deleting");
                }
            }
        }

        private void btnEditEvent_Click(object sender, RoutedEventArgs e)
        {
            Event selectedEvent = lbxEvents.SelectedItem as Event;
            try
            {
                //When there is a selected event, a new window will open
                if (selectedEvent != null)
                {
                    //Passes the currently selected event to the new window
                    Application.Current.Properties["selectedEvent"] = selectedEvent;
                    EditEvent editEv = new EditEvent();
                    editEv.Owner = this;

                    editEv.Show();
                }
                else
                {
                    //If the user chooses to edit an event without selecting one
                    MessageBox.Show("Please select an event to edit first!");
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
