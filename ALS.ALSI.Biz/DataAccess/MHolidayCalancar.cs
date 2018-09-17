using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class holiday_calendar
    {

        private static IRepository<holiday_calendar> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<holiday_calendar>>(); }
        }

        #region "Property"

        #endregion


        public List<holiday_calendar> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            _repository.Edit(this, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }


        //public DateTime GetWorkingDay(DateTime CalDate, int _day)
        //{
        //    DateTime result = DateTime.Now;
        //    DateTime _calDate = new DateTime(CalDate.Year, CalDate.Month, CalDate.Day, 0, 0, 0);
        //    //_day--;
        //    //if (_day > 0)
        //    //{
        //    //    if (IsHoliday(_calDate))
        //    //    {
        //    //    }
        //    //    _calDate = _calDate.AddDays(1);
        //    //    return _calDate;
        //    //}
        //    return GetWorkingDay(CalDate,_day);
        //}

        public DateTime GetWorkingDay(DateTime StartDate, int _NumberOfBusinessDays)
        {
            int NumberOfBusinessDays = (_NumberOfBusinessDays - 1);
            //Knock the start date down one day if it is on a weekend.
            if (StartDate.DayOfWeek == DayOfWeek.Saturday |
                StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                NumberOfBusinessDays -= 1;
            }

            int index = 0;

            for (index = 1; index <= NumberOfBusinessDays; index++)
            {
                switch (StartDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        StartDate = StartDate.AddDays(2);
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        StartDate = StartDate.AddDays(1);
                        break;
                    case DayOfWeek.Saturday:
                        StartDate = StartDate.AddDays(3);
                        break;
                }
                if (IsThaiHoliday(StartDate))
                {
                    StartDate = StartDate.AddDays(1);
                }
            }

            //check to see if the end date is on a weekend.
            //If so move it ahead to Monday.
            //You could also bump it back to the Friday before if you desired to. 
            //Just change the code to -2 and -1.
            if (StartDate.DayOfWeek == DayOfWeek.Saturday)
            {
                StartDate = StartDate.AddDays(2);
            }
            else if (StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                StartDate = StartDate.AddDays(1);
            }

            return StartDate;
        }

        public bool IsHoliday(DateTime CurrentDate)
        {

            DateTime Date = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            List<holiday_calendar> h = SelectAll().Where(x => x.DATE_HOLIDAYS == CurrentDate).ToList();


            return ((CurrentDate.DayOfWeek == DayOfWeek.Saturday)
               || (CurrentDate.DayOfWeek == DayOfWeek.Sunday)
               || (h.Count > 0));

        }

        public bool IsThaiHoliday(DateTime CurrentDate)
        {
            List<holiday_calendar> h = SelectAll().Where(x => x.DATE_HOLIDAYS == CurrentDate).ToList();
            return   (h.Count > 0);
        }

    }
}
