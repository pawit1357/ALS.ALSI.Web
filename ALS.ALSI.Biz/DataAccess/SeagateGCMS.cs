﻿using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_gcms_coverpage_2
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_gcms_coverpage_2));

        #region "Property"
        public int order { get; set; }
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_seagate_gcms_coverpage_2> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_gcms_coverpage_2>>(); }
        }

        public IEnumerable<template_seagate_gcms_coverpage_2> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_gcms_coverpage_2 SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_gcms_coverpage_2 existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }
        public static void CloneData(int oldSampleId, int newSampleId)
        {
            try
            {
                List<template_seagate_gcms_coverpage_2> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_seagate_gcms_coverpage_2 item in lists)
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

        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_seagate_gcms_coverpage_2> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_gcms_coverpage_2 tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_seagate_gcms_coverpage_2> _lists)
        {
            foreach (template_seagate_gcms_coverpage_2 tmp in _lists)
            {

                tmp.Insert();
            }

        }
        public static void UpdateList(List<template_seagate_gcms_coverpage_2> _lists)
        {
            foreach (template_seagate_gcms_coverpage_2 tmp in _lists)
            {
                tmp.Update();
            }
        }
        public static List<template_seagate_gcms_coverpage_2> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        public static template_seagate_gcms_coverpage_2 FindBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).FirstOrDefault();
        }

        #endregion
    }
}
