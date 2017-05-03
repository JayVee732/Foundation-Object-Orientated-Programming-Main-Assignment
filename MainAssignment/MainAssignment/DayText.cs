using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainAssignment
{
    class DayText
    {
        public DateTime Year { get; set; }
        public string Text { get; set; }
        
        public DayText ()
        {

        }

        public DayText(DateTime year, string text)
        {
            this.Year = year;
            this.Text = text;
        }
    }
}
