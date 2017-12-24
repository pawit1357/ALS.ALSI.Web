using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_pa_detail
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_pa_detail));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_pa_detail> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_pa_detail>>(); }
        }

        public IEnumerable<template_pa_detail> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_pa_detail SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id );
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_pa_detail existing = _repository.Find(x => x.sample_id == this.sample_id ).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public void InsertList(List<template_pa_detail> _lists)
        {
            foreach (template_pa_detail tmp in _lists)
            {
                _repository.Add(tmp);
            }
        }
        public void UpdateList(List<template_pa_detail> _lists)
        {
            foreach (template_pa_detail tmp in _lists)
            {
                tmp.Update();
            }
        }


        public List<template_pa_detail> SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).ToList();
        }


        public void DeleteBySampleID(int _sampleID)
        {
            List<template_pa_detail> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_pa_detail tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }
        
        public static List<template_pa_detail> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
        
        public static void CloneData(int oldSampleId, int newSampleId)
        {
            try
            {
                List<template_pa_detail> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_pa_detail item in lists)
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

    }


    public class ReportLPC
    {

        public string liquidParticleCount { get; set; }
        public string specificationLimit { get; set; }
        public string result { get; set; }
        public int lpc_type { get; set; }
        public int particle_type { get; set; }
        public string unit { get; set; }


    }
}
