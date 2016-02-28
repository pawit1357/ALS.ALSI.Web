﻿using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_wd_gcms_coverpage
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_wd_gcms_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_wd_gcms_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_wd_gcms_coverpage>>(); }
        }

        public IEnumerable<template_wd_gcms_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_wd_gcms_coverpage SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_wd_gcms_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_wd_gcms_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_wd_gcms_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_wd_gcms_coverpage> _lists)
        {
            foreach (template_wd_gcms_coverpage tmp in _lists)
            {
                //switch (tmp.RowState)
                //{
                //    case CommandNameEnum.Add:
                        tmp.Insert();
                //        break;
                //    case CommandNameEnum.Edit:
                //        tmp.Update();
                //        break;
                //}

            }
        }
        public static void UpdateList(List<template_wd_gcms_coverpage> _lists)
        {
            foreach (template_wd_gcms_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }
        public static List<template_wd_gcms_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
