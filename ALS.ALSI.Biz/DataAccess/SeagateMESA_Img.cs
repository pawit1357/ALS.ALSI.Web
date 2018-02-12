using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_mesa_img
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_mesa_img));

        #region "Property"
        public CommandNameEnum RowState { get; set; }

        public byte[] img1 { get; set; }
        public byte[] img2 { get; set; }
        public byte[] img3 { get; set; }
        public byte[] img4 { get; set; }

        #endregion

        private static IRepository<template_seagate_mesa_img> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_mesa_img>>(); }
        }

        public IEnumerable<template_seagate_mesa_img> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_mesa_img SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_mesa_img existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
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
                List<template_seagate_mesa_img> lists = _repository.Find(x => x.sample_id == oldSampleId).ToList();
                if (null != lists && lists.Count > 0)
                {
                    foreach (template_seagate_mesa_img item in lists)
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
            List<template_seagate_mesa_img> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_mesa_img tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_seagate_mesa_img> _lists)
        {
            foreach (template_seagate_mesa_img tmp in _lists)
            {
                template_seagate_mesa_img existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
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
                //        template_seagate_mesa_img existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
                //        _repository.Edit(existing, tmp);
                //        break;
                //}

            }
        }
        public static void UpdateList(List<template_seagate_mesa_img> _lists)
        {
            foreach (template_seagate_mesa_img tmp in _lists)
            {
                tmp.Update();
            }
        }
        public static List<template_seagate_mesa_img> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
