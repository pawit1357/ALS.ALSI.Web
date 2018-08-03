using ALS.ALIS.Repository.Interface;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_f_ic
    {

        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_f_ic));

        private static IRepository<template_f_ic> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_f_ic>>(); }
        }

        #region "Property"

        #endregion


        public IEnumerable<template_f_ic> SelectAll()
        {
            return _repository.GetAll().ToList();
        }
        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_f_ic> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_f_ic tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_f_ic> _lists)
        {

            foreach (template_f_ic tmp in _lists)
            {
                tmp.Insert();
            }
        }
        public template_f_ic SelectByID(int _id)
        {
            
            return _repository.First(x => x.id == _id);
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

        #region "Custom"
        public List<template_f_ic> SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).ToList();
        }
        public IEnumerable SearchData()
        {
            //using (ALSIEntities ctx = new ALSIEntities())
            //{
            //    var result = from j in ctx.template_f_ic select j;

            //    if (this.id > 0)
            //    {
            //        result = result.Where(x => x.id == this.id);
            //    }
            //    if (!String.IsNullOrEmpty(this.name))
            //    {
            //        result = result.Where(x => x.name == this.name);
            //    }
            //    return result.ToList();
            //}
            return _repository.GetAll().ToList();
        }

        #endregion
    }
}
