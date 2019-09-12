using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
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

        public static void InsertList(List<holiday_calendar> _lists)
        {
            try
            {
                foreach (holiday_calendar tmp in _lists)
                {
                    _repository.Add(tmp);
                }
            }catch (Exception)
            {
                //Console.WriteLine();
            }

        }

        public holiday_calendar SelectByID(DateTime _date)
        {
            return _repository.Find(x => x.DATE_HOLIDAYS.ToString("yyyyMMdd").Equals(_date.ToString("yyyyMMdd"))).FirstOrDefault();//.Year == _date.Year && x.DATE_HOLIDAYS.Month == _date.Month && x.DATE_HOLIDAYS.Day == _date.Day).FirstOrDefault();
        }

        public static void deleteByYear(int year)
        {
            List<holiday_calendar> hcs = _repository.GetAll().Where(x=>Convert.ToInt16(x.YEAR_HOLIDAYS) == year).ToList();
            foreach(holiday_calendar hc in hcs)
            {
                hc.Delete();
            }
        }

        public IEnumerable SearchData()
        {
            //List<holiday_calendar> hcs = _repository.GetAll().Where(x => x.YEAR_HOLIDAYS.Equals(this.YEAR_HOLIDAYS)).ToList();
            Console.WriteLine();
            //return _repository.GetAll().ToList();
            using (ALSIEntities ctx = new ALSIEntities())
            {
                var result = from j in ctx.holiday_calendar select j;// j.status == "A" select j;

                //if (this.ID > 0)
                //{
                //    result = result.Where(x => x.ID == this.ID);
                //}
                if (!String.IsNullOrEmpty(this.YEAR_HOLIDAYS))
                {
                    result = result.Where(x => x.YEAR_HOLIDAYS.Contains(this.YEAR_HOLIDAYS));
                }
                return result.ToList();
            }
        }



        public DateTime GetWorkingDayLab(DateTime StartDate, int addDay, bool isUrgent = false)
        {
            try
            {

                if (isUrgent)
                {
                    //find holiday in period
                    List<DateTime> excludedDates = SelectAll().Select(x => x.DATE_HOLIDAYS).ToList();

                    int index = 0;
                    DateTime dtEndDate = StartDate.AddDays(addDay - 1);
                    for (DateTime date = StartDate; date.Date <= dtEndDate.Date; date = date.AddDays(1))
                    {

                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || excludedDates.Contains(date))
                        {
                            index++;
                        }
                    }
                    StartDate = StartDate.AddDays((addDay+index) - 1);
                    while (StartDate.DayOfWeek == DayOfWeek.Saturday || StartDate.DayOfWeek == DayOfWeek.Sunday || excludedDates.Contains(StartDate))
                    {
                        StartDate = StartDate.AddDays(1);
                    }
                }
                else
                {
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
                }
            }
            catch (Exception ex)
            {

            }
            return StartDate;
        }






        //public DateTime GetWorkingDayCustomer(DateTime startDate, int addDay)
        //{
        //    DateTime endDate = startDate.AddDays(addDay);
        //    int workingDays = Enumerable.Range(0, Convert.ToInt32(endDate.Subtract(startDate).TotalDays)).Select(i => new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }.Contains(startDate.AddDays(i).DayOfWeek) ? 0 : 1).Sum();


        //    DateTime StartDate = startDate.AddDays(addDay + (addDay - workingDays));
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
        //public DateTime GetWorkingDayCustomerOnlyUrgent(DateTime startDate, int addDay)
        //{
        //    DateTime endDate = startDate.AddDays(addDay);
        //    //int workingDays = Enumerable.Range(0, Convert.ToInt32(endDate.Subtract(startDate).TotalDays)).Select(i => new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }.Contains(startDate.AddDays(i).DayOfWeek) ? 0 : 1).Sum();


        //    //DateTime StartDate = startDate.AddDays(addDay + (addDay - workingDays));
        //    //if (StartDate.DayOfWeek == DayOfWeek.Saturday)
        //    //{
        //    //    StartDate = StartDate.AddDays(2);
        //    //}
        //    //else if (StartDate.DayOfWeek == DayOfWeek.Sunday)
        //    //{
        //    //    StartDate = StartDate.AddDays(1);
        //    //}
        //    //if (IsThaiHoliday(StartDate))
        //    //{
        //    //    StartDate = StartDate.AddDays(1);
        //    //}


        //    return endDate;
        //}
        //public bool IsHoliday(DateTime CurrentDate)
        //{

        //    DateTime Date = new DateTime(CurrentDate.Year, CurrentDate.Month, CurrentDate.Day, 0, 0, 0);
        //    List<holiday_calendar> h = SelectAll().Where(x => x.DATE_HOLIDAYS == CurrentDate).ToList();


        //    return ((CurrentDate.DayOfWeek == DayOfWeek.Saturday)
        //       || (CurrentDate.DayOfWeek == DayOfWeek.Sunday)
        //       || (h.Count > 0));

        //}

        public bool IsThaiHoliday(DateTime CurrentDate)
        {
            List<holiday_calendar> h = SelectAll().Where(x => x.DATE_HOLIDAYS == CurrentDate).ToList();
            return (h.Count > 0);
        }

    }
}
