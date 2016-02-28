using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_wd_lpc_coverpage
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_wd_lpc_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        //public string runText { get { return this.run.ToString(); } }
        #endregion

        private static IRepository<template_wd_lpc_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_wd_lpc_coverpage>>(); }
        }

        public IEnumerable<template_wd_lpc_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_wd_lpc_coverpage SelectByID(int _id)
        {
            return _repository.Find(x => x.sample_id == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_wd_lpc_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
            else
            {
                _repository.Add(this);
            }
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public void InsertList(List<template_wd_lpc_coverpage> _lists)
        {
            foreach (template_wd_lpc_coverpage tmp in _lists)
            {
                
                _repository.Add(tmp);
            }
        }
        public void UpdateList(List<template_wd_lpc_coverpage> _lists)
        {
            foreach (template_wd_lpc_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }

        public template_wd_lpc_coverpage SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).FirstOrDefault();
        }

        public void DeleteBySampleID(int _sampleID)
        {
            List<template_wd_lpc_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_wd_lpc_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static List<template_wd_lpc_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
