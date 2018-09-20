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

        public DateTime GetWorkingDayLab(DateTime StartDate, int addDay)
        {
            //int NumberOfBusinessDays = (_NumberOfBusinessDays - 1);

            StartDate = StartDate.AddDays(addDay);


            if (StartDate.DayOfWeek == DayOfWeek.Saturday)
            {
                StartDate = StartDate.AddDays(2);
            }
            else if (StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                StartDate = StartDate.AddDays(1);
            }
            if (IsThaiHoliday(StartDate))
            {
                StartDate = StartDate.AddDays(1);
            }


            return StartDate;
        }


        //public DateTime GetWorkingDay(DateTime StartDate)
        //{
        //    StartDate = StartDate.AddDays(-1);

        //    if (StartDate.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        StartDate = StartDate.AddDays(2);
        //    }
        //    else if (StartDate.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        StartDate = StartDate.AddDays(1);
        //    }
        //    if (IsThaiHoliday(StartDate))
        //    {
        //        StartDate = StartDate.AddDays(1);
        //    }
        //    return StartDate;
        //}


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
