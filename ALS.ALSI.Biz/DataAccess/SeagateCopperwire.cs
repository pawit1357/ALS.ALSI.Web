using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    
    public partial class template_seagate_copperwire_coverpage
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_copperwire_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_seagate_copperwire_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_copperwire_coverpage>>(); }
        }

        public IEnumerable<template_seagate_copperwire_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_copperwire_coverpage SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_copperwire_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_seagate_copperwire_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_copperwire_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_seagate_copperwire_coverpage> _lists)
        {
            foreach (template_seagate_copperwire_coverpage tmp in _lists)
            {
                template_seagate_copperwire_coverpage existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                if (existing == null)
                {
                    _repository.Add(tmp);
                }
                else
                {
                    _repository.Edit(existing, tmp);
                }
                //switch (tmp.RowState)
                //{
                //    case CommandNameEnum.Add:
                //        _repository.Add(tmp);
                //        break;
                //    case CommandNameEnum.Edit:
                //        template_seagate_copperwire_coverpage existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
                //        _repository.Edit(existing, tmp);
                //        break;
                //}

            }
        }
        public static void UpdateList(List<template_seagate_copperwire_coverpage> _lists)
        {
            foreach (template_seagate_copperwire_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }
        public static List<template_seagate_copperwire_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }


        public static void CloneData(int oldSampleId,int newSampleId)
        {
            try
            {
                List<template_seagate_copperwire_coverpage> listOfData = FindAllBySampleID(oldSampleId);
                if (null != listOfData && listOfData.Count > 0)
                {
                    foreach (template_seagate_copperwire_coverpage item in listOfData)
                    {
                        item.sample_id = newSampleId;
                        item.Insert();
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
