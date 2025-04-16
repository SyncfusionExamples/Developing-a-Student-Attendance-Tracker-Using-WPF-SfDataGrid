using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace StudentAttendenceTrackerDemo
{
    public class StudentsViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<StudentsModel> students;

        public ObservableCollection<StudentsModel> Students
        {
            get { return students; }
            set { students = value; }
        }

        private ObservableCollection<MonthlyRecordsModel> monthlyRecords;

        public ObservableCollection<MonthlyRecordsModel> MonthlyRecords
        {
            get { return monthlyRecords; }
            set { monthlyRecords = value; }
        }

        private ObservableCollection<string> studentsNames;

        public ObservableCollection<string> StudentsNames
        {
            get { return studentsNames; }
            set { studentsNames = value; }
        }

        private ObservableCollection<string> monthNames;

        public ObservableCollection<string> MonthNames
        {
            get { return monthNames; }
            set { monthNames = value; }
        }



        public StudentsViewModel()
        {
            Students = new ObservableCollection<StudentsModel>(GenerateStudents(false));
            MonthlyRecords = new ObservableCollection<MonthlyRecordsModel>(GenerateMonthlyRecords(DateTime.Now));
            StudentsNames = new ObservableCollection<string>(){"Alice", "Benjamin", "Chloe", "Daniel", "Eva",
                                                                 "Felix", "Grace", "Henry", "Isla", "Jack",
                                                                 "Kylie", "Liam", "Mia", "Nathan", "Olivia",
                                                                 "Patrick", "Quinn", "Rachel", "Samuel", "Tara",
                                                                 "Umar", "Violet", "William", "Xavier", "Yasmin",
                                                                 "Zachary", "Amelia", "Brandon", "Caitlyn", "Dylan",
                                                                 "Ella", "Finn", "Gabriel", "Hannah", "Ian",
                                                                 "Jasmine", "Kevin", "Layla", "Mason", "Nora",
                                                                 "Oscar", "Peyton", "Reuben", "Sophie", "Tristan",
                                                                 "Uma", "Victor", "Wendy", "Xena", "Yusuf" };

            MonthNames = new ObservableCollection<string>() { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                                                             "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<StudentsModel> GenerateStudents(bool IsFutureDate)
        {
            var random = new Random();
            string[] names = { "Alice", "Benjamin", "Chloe", "Daniel", "Eva",
                                "Felix", "Grace", "Henry", "Isla", "Jack",
                                "Kylie", "Liam", "Mia", "Nathan", "Olivia",
                                "Patrick", "Quinn", "Rachel", "Samuel", "Tara",
                                "Umar", "Violet", "William", "Xavier", "Yasmin",
                                "Zachary", "Amelia", "Brandon", "Caitlyn", "Dylan",
                                "Ella", "Finn", "Gabriel", "Hannah", "Ian",
                                "Jasmine", "Kevin", "Layla", "Mason", "Nora",
                                "Oscar", "Peyton", "Reuben", "Sophie", "Tristan",
                                "Uma", "Victor", "Wendy", "Xena", "Yusuf" };

            Array.Sort(names);
            var students = new ObservableCollection<StudentsModel>();

            for (int i = 1; i <= 50; i++)
            {
                var student = new StudentsModel
                {
                    Id = i,
                    Name = names[i - 1],                   
                    Mathematics = IsFutureDate ? false : random.Next(0, 2) == 1, // 50% chance of presence
                    History = IsFutureDate ? false : random.Next(0, 2) == 1,
                    Science = IsFutureDate ? false : random.Next(0, 2) == 1,
                    English = IsFutureDate ? false : random.Next(0, 2) == 1
                };
                students.Add(student);
            }

            return students;
        }

        public ObservableCollection<MonthlyRecordsModel> GenerateMonthlyRecords(DateTime dateTime)
        {
            var records = new ObservableCollection<MonthlyRecordsModel>();
            int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var today = DateTime.Today;
            var isCurrentMonth = dateTime.Year == today.Year && dateTime.Month == today.Month;
            var isFutureMonth = dateTime.Date > today.Date;

            var random = new Random();

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(dateTime.Year, dateTime.Month, day);
                bool isPastOrToday = date <= today;

                records.Add(new MonthlyRecordsModel
                {
                    Date = day,
                    Day = date.ToString("dddd", CultureInfo.InvariantCulture),
                    Mathematics = isFutureMonth || !isPastOrToday ? false : random.Next(0, 2) == 1,
                    History = isFutureMonth || !isPastOrToday ? false : random.Next(0, 2) == 1,
                    Science = isFutureMonth || !isPastOrToday ? false : random.Next(0, 2) == 1,
                    English = isFutureMonth || !isPastOrToday ? false : random.Next(0, 2) == 1
                });
            }

            return records;
        }

        //public ObservableCollection<MonthlyRecordsModel> GenerateMonthlyRecords(DateTime dateTime)
        //{
        //    var records = new ObservableCollection<MonthlyRecordsModel>();
        //    var today = DateTime.Today;
        //    int daysInMonth = DateTime.DaysInMonth(today.Year, today.Month);
        //    var random = new Random();

        //    for (int day = 1; day <= daysInMonth; day++)
        //    {
        //        var date = new DateTime(today.Year, today.Month, day);
        //        records.Add(new MonthlyRecordsModel
        //        {
        //            Date = day,
        //            Day = date.ToString("dddd", CultureInfo.InvariantCulture), // Monday, Tuesday, etc.
        //            Mathematics = random.Next(0, 2) == 1,
        //            History = random.Next(0, 2) == 1,
        //            Science = random.Next(0, 2) == 1,
        //            English = random.Next(0, 2) == 1
        //        });
        //    }
        //    return records;
        //}
    }





}
