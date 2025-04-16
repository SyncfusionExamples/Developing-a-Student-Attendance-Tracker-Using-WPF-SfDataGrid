using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendenceTrackerDemo
{
   public class StudentsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Mathematics { get; set; }
        public bool History { get; set; }
        public bool Science { get; set; }
        public bool English { get; set; }
       
    }

    public class MonthlyRecordsModel
    {
        public int Date { get; set; }
        public string? Day { get; set; }
        public bool? Mathematics { get; set; }
        public bool? History { get; set; }
        public bool? Science { get; set; }
        public bool? English { get; set; }
    }
}
