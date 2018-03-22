using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class job_sample_logs 
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(job_sample_logs));

        private static IRepository<job_sample_logs> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<job_sample_logs>>(); }
        }

        #region "Property"
        public String job_number { get; set; }
        public String resposible { get; set; }
        public int role_id { get; set; }
        #endregion


        public IEnumerable<job_sample_logs> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public job_sample_logs SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            job_sample_logs existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        //public job_sample_logs SelectDate(int _sample_Id, int _status)
        //{
        //    return _repository.Find(x => x.job_sample_id == _sample_Id && x.job_status == _status).OrderBy(x=>x.date).FirstOrDefault();
        //}

        public List<job_sample_logs> SelectNotification(users_login userLogin )
        {

            /*
            1   Root
            2   Login
            3   Chemist
            4   SrChemist
            5   Admin
            6   LabManager
            7   Account
            */
            List<job_sample_logs> logs = new List<job_sample_logs>();
            using (var ctx = new ALSIEntities())
            {
                var result = from l in ctx.job_sample_logs
                             join j in ctx.job_sample on l.job_sample_id equals j.ID
                             where l.is_active == "0"
                             select new
                             {
                                 j.step1owner,
                                 j.step2owner,
                                 j.step3owner,
                                 j.step4owner,
                                 j.step5owner,
                                 j.step6owner,
                                 j.step7owner,
                                 l.log_title,
                                 l.job_remark,
                                 l.date,
                                 j.job_status,
                                 j.job_number
                             };

                DataTable dt = result.ToDataTable();
                foreach (DataRow dr in dt.Rows)
                {


                    bool con1 = (dr["step1owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step1owner"]) == userLogin.id;
                    bool con2 = (dr["step2owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step2owner"]) == userLogin.id;
                    bool con3 = (dr["step3owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step3owner"]) == userLogin.id;
                    bool con4 = (dr["step4owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step4owner"]) == userLogin.id;
                    bool con5 = (dr["step5owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step5owner"]) == userLogin.id;
                    bool con6 = (dr["step6owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step6owner"]) == userLogin.id;
                    bool con7 = (dr["step7owner"] == DBNull.Value) ? false : Convert.ToInt16(dr["step7owner"]) == userLogin.id;


                    Boolean isOk = con1 || con2 || con3 || con4 || con5 || con6 || con7;

                    int jobStatus = Convert.ToInt16(dr["job_status"]);
                    string logTitle = dr["log_title"].ToString();
                    string jobRemark = dr["job_remark"].ToString();
                    string jobNumber = dr["job_number"].ToString();

                    DateTime date = Convert.ToDateTime(dr["date"].ToString());


                    StatusEnum status = (StatusEnum)Enum.Parse(typeof(StatusEnum), dr["job_status"].ToString(), true);
                    switch (status)
                    {
                        case StatusEnum.LOGIN_CONVERT_TEMPLATE:
                            break;
                        case StatusEnum.LOGIN_SELECT_SPEC:
                            if (isOk && userLogin.role_id == 2)
                            {
                                job_sample_logs jobSampleLog = new job_sample_logs
                                {
                                    log_title = String.Format("{0}->{1}", job_number, logTitle),
                                    job_number = jobNumber,
                                    job_remark = jobRemark,
                                    date = date
                                };
                                logs.Add(jobSampleLog);
                            }
                            break;
                        case StatusEnum.CHEMIST_TESTING:
                            if (isOk && userLogin.role_id == 3)
                            {
                                job_sample_logs jobSampleLog = new job_sample_logs
                                {
                                    log_title =  logTitle,
                                    job_number = jobNumber,
                                    job_remark = jobRemark,
                                    date = date
                                };
                                logs.Add(jobSampleLog);
                            }
                            break;
                        case StatusEnum.SR_CHEMIST_CHECKING:
                            if (isOk && userLogin.role_id == 4)
                            {
                                job_sample_logs jobSampleLog = new job_sample_logs
                                {
                                    log_title = String.Format("{0}->{1}", job_number, logTitle),
                                    job_number = jobNumber,
                                    job_remark = jobRemark,
                                    date = date
                                };
                                logs.Add(jobSampleLog);
                            }
                            break;
                        case StatusEnum.ADMIN_CONVERT_WORD:
                            if (isOk && userLogin.role_id == 5)
                            {
                                job_sample_logs jobSampleLog = new job_sample_logs
                                {
                                    log_title = String.Format("{0}->{1}", job_number, logTitle),
                                    job_number = jobNumber,
                                    job_remark = jobRemark,
                                    date = date
                                };
                                logs.Add(jobSampleLog);
                            }
                            break;
                        case StatusEnum.LABMANAGER_CHECKING:
                            if (isOk && userLogin.role_id == 5)
                            {
                                job_sample_logs jobSampleLog = new job_sample_logs
                                {
                                    log_title = String.Format("{0}->{1}", job_number, logTitle),
                                    job_number = jobNumber,
                                    job_remark = jobRemark,
                                    date = date
                                };
                                logs.Add(jobSampleLog);
                            }
                            break;
                        case StatusEnum.ADMIN_CONVERT_PDF:
                            if (isOk && userLogin.role_id == 5)
                            {
                                job_sample_logs jobSampleLog = new job_sample_logs
                                {
                                    log_title = String.Format("{0}->{1}", job_number, logTitle),
                                    job_number = jobNumber,
                                    job_remark = jobRemark,
                                    date = date
                                };
                                logs.Add(jobSampleLog);
                            }
                            break;
                           
                    }
                }
                return logs;
            }


        }
        public List<job_sample_logs> SearchData()
        {
            return _repository.GetAll().ToList();

        }

        #endregion

    }
}
