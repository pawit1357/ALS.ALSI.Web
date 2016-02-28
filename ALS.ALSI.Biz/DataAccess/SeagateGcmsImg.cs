﻿using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_gcms_coverpage_img
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_gcms_coverpage_img));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_seagate_gcms_coverpage_img> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_gcms_coverpage_img>>(); }
        }

        public IEnumerable<template_seagate_gcms_coverpage_img> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_gcms_coverpage_img SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_gcms_coverpage_img existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_seagate_gcms_coverpage_img> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_gcms_coverpage_img tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_seagate_gcms_coverpage_img> _lists)
        {
            foreach (template_seagate_gcms_coverpage_img tmp in _lists)
            {
                //template_seagate_gcms_coverpage_img existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
                //if (existing == null)
                //{
                    _repository.Add(tmp);
                //}
                //else
                //{
                //    _repository.Edit(existing, tmp);
                //}
 

            }
        }
        public static void UpdateList(List<template_seagate_gcms_coverpage_img> _lists)
        {
            foreach (template_seagate_gcms_coverpage_img tmp in _lists)
            {
                tmp.Update();
            }
        }
        public static List<template_seagate_gcms_coverpage_img> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
