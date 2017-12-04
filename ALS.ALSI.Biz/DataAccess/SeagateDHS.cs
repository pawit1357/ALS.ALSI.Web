using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_dhs_coverpage
    {
        ////private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_dhs_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public string unitText { get { return this.unit.ToString(); } }
        #endregion

        private static IRepository<template_seagate_dhs_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_dhs_coverpage>>(); }
        }

        public IEnumerable<template_seagate_dhs_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_dhs_coverpage SelectByID(int _id)
        {
            return _repository.First(x => x.ID == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_dhs_coverpage existing = _repository.Find(x => x.ID == this.ID).FirstOrDefault();
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
                List<template_seagate_dhs_coverpage> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_seagate_dhs_coverpage item in lists)
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
            List<template_seagate_dhs_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_dhs_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_seagate_dhs_coverpage> _lists)
        {
            foreach (template_seagate_dhs_coverpage tmp in _lists)
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
        public static void UpdateList(List<template_seagate_dhs_coverpage> _lists)
        {
         
            //foreach (template_seagate_dhs_coverpage tmp in _lists)
            //{
            //    template_seagate_dhs_coverpage _tmp = _repository.Find(x => x.sample_id == _lists[0].sample_id).FirstOrDefault();
            //         if (_tmp != null)
            //         {
            //             _tmp.Delete();
            //         }
            //}
            foreach (template_seagate_dhs_coverpage tmp in _lists)
            {
                //template_seagate_dhs_coverpage _tmp = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
                //if (_tmp != null)
                //{
                    tmp.Update();
                //}
                //else
                //{
                //    tmp.Insert();
                //}
 
            }
        }
        public static List<template_seagate_dhs_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
