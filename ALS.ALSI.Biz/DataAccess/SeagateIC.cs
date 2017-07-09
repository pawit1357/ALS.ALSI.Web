using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_ic_coverpage 
    {


        //////private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_ic_coverpage));

        private static IRepository<template_seagate_ic_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_ic_coverpage>>(); }
        }

        #region "Property"
        public String wunitText { get; set; }
        public String wunitTextReport { get { return Configurations.getUnitText(this.wunit.Value); } }
        #endregion


        public IEnumerable<template_seagate_ic_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_ic_coverpage SelectByID(int _id)
        {
            return _repository.First(x => x.sample_id == _id);
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_ic_coverpage existing = _repository.Find(x => x.id == this.id).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
            else
            {
                Console.WriteLine();
            }
         
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"
        public void UpdateBySampleID()
        {
            template_seagate_ic_coverpage existing = _repository.Find(x => x.sample_id == this.sample_id).FirstOrDefault();
            if (existing != null)
            {
                _repository.Edit(existing, this);
            }
            else
            {
                Console.WriteLine();
            }
          
        }
        public template_seagate_ic_coverpage SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).FirstOrDefault();
        }
        public static List<template_seagate_ic_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        public IEnumerable SearchData()
        {
            return _repository.GetAll().ToList();

        }

        #endregion


        public static void InsertList(List<template_seagate_ic_coverpage> _lists)
        {

            foreach (template_seagate_ic_coverpage tmp in _lists)
            {
                tmp.Insert();
            }
        }
        public static void UpdateList(List<template_seagate_ic_coverpage> _lists)
        {
            foreach (template_seagate_ic_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }


        public static void DeleteBySampleID(int _sampleID)
        {
            List<template_seagate_ic_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_ic_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }


        //#region "IDisposable Support"
        //void IDisposable.Dispose()
        //{
        //    GC.SuppressFinalize(this);
        //}
        //#endregion

        //public IEnumerable<template_seagate_ic_coverpage> SelectAll()
        //{
        //    using (ALSIEntities ctx = new ALSIEntities())
        //    {
        //        return (from c in ctx.template_seagate_ic_coverpage select c).ToList();
        //    }
        //}

        //public template_seagate_ic_coverpage SelectByID(int _id)
        //{
        //    using (ALSIEntities ctx = new ALSIEntities())
        //    {
        //        return (from c in ctx.template_seagate_ic_coverpage where c.ID == _id select c).FirstOrDefault();
        //    }
        //}

        //public void Insert()
        //{
        //    using (ALSIEntities context = new ALSIEntities())
        //    {
        //        context.template_seagate_ic_coverpage.Add(this);
        //        context.SaveChanges();
        //    }
        //}

        //public void Update()
        //{
        //    using (ALSIEntities context = new ALSIEntities())
        //    {
        //        var result = from j in context.template_seagate_ic_coverpage where j.sample_id == this.sample_id select j;
        //        template_seagate_ic_coverpage tmp = result.FirstOrDefault();
        //        if (tmp != null)
        //        {
        //            setEditData(tmp);
        //        }
        //        context.SaveChanges();
        //    }
        //}

        //public void Delete()
        //{
        //    using (ALSIEntities context = new ALSIEntities())
        //    {
        //        var tmp = from p in context.template_seagate_ic_coverpage where p.ID == this.ID select p;
        //        context.template_seagate_ic_coverpage.Remove(tmp.FirstOrDefault());
        //        context.SaveChanges();
        //    }
        //}

        //#region "Custom"
        //private void setEditData(template_seagate_ic_coverpage _value)
        //{
        //    _value.sample_id = this.sample_id;
        //    _value.specification_id = this.specification_id;

        //    _value.b9 = this.b9;
        //    _value.b10 = this.b10;
        //    _value.b11 = this.b11;

        //    _value.b14 = this.b14;
        //    _value.b15 = this.b15;
        //    _value.b16 = this.b16;
        //    _value.b17 = this.b17;
        //    _value.b18 = this.b18;
        //    _value.b19 = this.b19;
        //    _value.b20 = this.b20;
        //    _value.b23 = this.b23;
        //    _value.b24 = this.b24;
        //    _value.b25 = this.b25;
        //    _value.b26 = this.b26;
        //    _value.b27 = this.b27;
        //    _value.b28 = this.b28;

        //    _value.c14 = this.c14;
        //    _value.c15 = this.c15;
        //    _value.c16 = this.c16;
        //    _value.c17 = this.c17;
        //    _value.c18 = this.c18;
        //    _value.c19 = this.c19;
        //    _value.c20 = this.c20;
        //    _value.c23 = this.c23;
        //    _value.c25 = this.c25;
        //    _value.c26 = this.c26;
        //    _value.c27 = this.c27;
        //    _value.c28 = this.c28;
        //    _value.c24 = this.c24;

        //    _value.result_c25 = this.result_c25;
        //    _value.result_c26 = this.result_c26;
        //    _value.result_c27 = this.result_c27;
        //    _value.result_c28 = this.result_c28;
        //    _value.result_c29 = this.result_c29;
        //    _value.result_c30 = this.result_c30;
        //    _value.result_c31 = this.result_c31;
        //    _value.result_c32 = this.result_c32;

        //    _value.result_c34 = this.result_c34;
        //    _value.result_c35 = this.result_c35;
        //    _value.result_c36 = this.result_c36;
        //    _value.result_c37 = this.result_c37;
        //    _value.result_c38 = this.result_c38;
        //    _value.result_c39 = this.result_c39;
        //    _value.result_c40 = this.result_c40;

        //}
        //public template_seagate_ic_coverpage SelectBySampleID(int _id)
        //{
        //    using (ALSIEntities ctx = new ALSIEntities())
        //    {
        //        return (from c in ctx.template_seagate_ic_coverpage where c.sample_id == _id select c).FirstOrDefault();
        //    }
        //}

        //#endregion

    }
}
