using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_ftir_coverpage 
    {


        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_ftir_coverpage));

        private static IRepository<template_seagate_ftir_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_ftir_coverpage>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<template_seagate_ftir_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_ftir_coverpage SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_ftir_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public void UpdateBySampleID()
        {
            template_seagate_ftir_coverpage existing = _repository.Find(x => x.sample_id == this.sample_id).FirstOrDefault();
            this.ID = existing.ID;
            _repository.Edit(existing, this);
        }

        public template_seagate_ftir_coverpage SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).FirstOrDefault();
        }
        public static List<template_seagate_ftir_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();

        }

        #endregion



    }
}
