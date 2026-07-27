using LifeiOS.Models;

namespace LifeiOS.ViewModels
{
    public class CalendarViewModel
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime CurrentMonth { get; set; }

        public int DaysInMonth { get; set; }

        public int FirstDayOfWeek { get; set; }

        public List<CalendarEvent> Events { get; set; } = new();
    }
}