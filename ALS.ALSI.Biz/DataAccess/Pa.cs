using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_pa
    {

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_pa> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_pa>>(); }
        }

        public IEnumerable<template_pa> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_pa SelectByID(int _id)
        {
            return _repository.Find(x => x.sample_id == _id).FirstOrDefault();
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_pa existing = _repository.Find(x => x.sample_id == this.sample_id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        public static void DeleteBySampleID(int _sampleID)
        {
            template_pa template_pa = _repository.Find(x => x.sample_id == _sampleID).FirstOrDefault();
            if (template_pa != null)
            {
                _repository.Delete(template_pa);
            }

        }

        public static void CloneData(int oldSampleId, int newSampleId)
        {
            try
            {
                List<template_pa> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_pa item in lists)
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

}
