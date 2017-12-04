using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_wd_hpa_for1_coverpage
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_wd_hpa_for1_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public String Ctext { get { return this.C.ToString(); } }
        public byte[] img1 { get; set; }
        #endregion

        private static IRepository<template_wd_hpa_for1_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_wd_hpa_for1_coverpage>>(); }
        }

        public IEnumerable<template_wd_hpa_for1_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_wd_hpa_for1_coverpage SelectByID(int _id)
        {
            return _repository.Find(x => x.sample_id == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_wd_hpa_for1_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
            else
            {
                Console.WriteLine();
            }
            //template_wd_hpa_for1_coverpage existing = _repository.Find(x => x.sample_id == this.sample_id).FirstOrDefault();
            //_repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }
        public static void CloneData(int oldSampleId, int newSampleId)
        {
            try
            {
                List<template_wd_hpa_for1_coverpage> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_wd_hpa_for1_coverpage item in lists)
                    {
                        item.sample_id = newSampleId;
                        item.Insert();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #region "Custom"

        //public void InsertList(List<template_wd_hpa_for1_coverpage> _lists)
        //{
        //    foreach (template_wd_hpa_for1_coverpage tmp in _lists)
        //    {
        //        _repository.Add(tmp);
        //    }
        //}
        //public void UpdateList(List<template_wd_hpa_for1_coverpage> _lists)
        //{
        //    foreach (template_wd_hpa_for1_coverpage tmp in _lists)
        //    {
        //        tmp.Update();
        //    }
        //}

        public template_wd_hpa_for1_coverpage SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).FirstOrDefault();
        }

        public List<template_wd_hpa_for1_coverpage> SeletAllBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).ToList();
        }


        public void DeleteBySampleID(int _sampleID)
        {
            List<template_wd_hpa_for1_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_wd_hpa_for1_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }
        public  void InsertList(List<template_wd_hpa_for1_coverpage> _lists)
        {

            foreach (template_wd_hpa_for1_coverpage tmp in _lists)
            {
                tmp.Insert();
            }
        }
        public  void UpdateList(List<template_wd_hpa_for1_coverpage> _lists)
        {
            foreach (template_wd_hpa_for1_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }
        //public void InsertList(List<template_wd_hpa_for1_coverpage> _lists)
        //{
        //    foreach (template_wd_hpa_for1_coverpage tmp in _lists)
        //    {
        //        switch (tmp.RowState)
        //        {
        //            case CommandNameEnum.Add:
        //                _repository.Add(tmp);
        //                break;
        //            case CommandNameEnum.Edit:
        //                template_wd_hpa_for1_coverpage existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
        //                _repository.Edit(existing, tmp);
        //                break;
        //        }

        //    }
        //}
        //public void UpdateList(List<template_wd_hpa_for1_coverpage> _lists)
        //{
        //    foreach (template_wd_hpa_for1_coverpage tmp in _lists)
        //    {
        //        tmp.Update();
        //    }
        //}
        public static List<template_wd_hpa_for1_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
