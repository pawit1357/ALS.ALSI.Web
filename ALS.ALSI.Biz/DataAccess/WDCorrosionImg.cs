using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_wd_corrosion_img
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_wd_corrosion_img));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        public byte[] img1 { get; set; }
        public byte[] img2 { get; set; }
        #endregion

        private static IRepository<template_wd_corrosion_img> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_wd_corrosion_img>>(); }
        }

        public IEnumerable<template_wd_corrosion_img> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_wd_corrosion_img SelectByID(int _id)
        {
            return _repository.First(x => x.id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_wd_corrosion_img existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_wd_corrosion_img> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_wd_corrosion_img tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        public static void InsertList(List<template_wd_corrosion_img> _lists)
        {
            foreach (template_wd_corrosion_img tmp in _lists)
            {
                //template_wd_corrosion_img existing = _repository.Find(x => x.id == tmp.id).FirstOrDefault();
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
        public static void UpdateList(List<template_wd_corrosion_img> _lists)
        {
            foreach (template_wd_corrosion_img tmp in _lists)
            {
                tmp.Update();
            }
        }
        public static List<template_wd_corrosion_img> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
    }
}
