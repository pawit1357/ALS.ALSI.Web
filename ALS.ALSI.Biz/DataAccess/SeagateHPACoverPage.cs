﻿using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_hpa_coverpage
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_hpa_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public byte[] img1 { get; set; }
        //public int Run { get { return (int)this.seq; }  }
        public string Run { get; set; }

        #endregion

        private static IRepository<template_seagate_hpa_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_hpa_coverpage>>(); }
        }

        public IEnumerable<template_seagate_hpa_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_hpa_coverpage SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_hpa_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        public static void CloneData(int oldSampleId, int newSampleId)
        {
            try
            {
                List<template_seagate_hpa_coverpage> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_seagate_hpa_coverpage item in lists)
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

        public void InsertList(List<template_seagate_hpa_coverpage> _lists)
        {
            foreach (template_seagate_hpa_coverpage tmp in _lists)
            {
                _repository.Add(tmp);
            }
        }

        public void UpdateList(List<template_seagate_hpa_coverpage> _lists)
        {
            foreach (template_seagate_hpa_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }


        public List<template_seagate_hpa_coverpage> SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).ToList();
        }


        public static void DeleteBySampleID(int _sampleID)
        {
            MaintenanceBiz.ExecuteCommand("delete from template_seagate_hpa_coverpage where sample_id="+ _sampleID);

            //List<template_seagate_hpa_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            //foreach (template_seagate_hpa_coverpage tmp in lists)
            //{
            //    _repository.Delete(tmp);
            //}

        }

        public static List<template_seagate_hpa_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
