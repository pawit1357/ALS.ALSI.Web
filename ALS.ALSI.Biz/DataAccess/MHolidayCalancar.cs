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


        public DateTime GetWorkingDay(DateTime CalDate, int _day)
        {
            int useDay = 1;
            DateTime result = DateTime.Now;
            for (int i = 0; i < _day; i++)
            {
                while (IsHoliday(CalDate))
                {
                    CalDate = CalDate.AddDays(1);
                }
                CalDate = CalDate.AddDays(1);
                useDay++;
            }
            result = CalDate.AddDays(_day- useDay);
            return result;
        }


        public bool IsHoliday(DateTime CurrentDate)
        {

            DateTime Date = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
            List<holiday_calendar> h = SelectAll().Where(x => x.DATE_HOLIDAYS == CurrentDate).ToList();


            return ((CurrentDate.DayOfWeek == DayOfWeek.Saturday)
               || (CurrentDate.DayOfWeek == DayOfWeek.Sunday)
               || (h.Count > 0));

        }


    }
}
