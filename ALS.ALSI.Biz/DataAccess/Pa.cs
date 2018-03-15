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

        public String iscontainer_text { get; set; }
        public String container_id_text { get; set; }
        public String isfluid1_text { get; set; }
        public String fluid1_id_text { get; set; }
        public String isfluid2_text { get; set; }
        public String fluid2_id_text { get; set; }
        public String fluid3_id_text { get; set; }
        public String isfluid3_text { get; set; }
        public String istshb01_text { get; set; }
        public String istshb02_text { get; set; }
        public String istshb03_text { get; set; }

        public String ispots01_text { get; set; }
        public String isdissolving_text { get; set; }
        public String ispressurerinsing_text { get; set; }
        public String isinternalrinsing_text { get; set; }
        public String isagitation_text { get; set; }
        public String iswashquantity_text { get; set; }
        public String isrewashingquantity_text { get; set; }
        public String iswashpressurerinsing_text { get; set; }
        public String iswashinternalrinsing_text { get; set; }
        public String iswashagitation_text { get; set; }
        public String isoven_text { get; set; }
        public String isdesiccator_text { get; set; }
        public String gravimetricalalysis_id_text { get; set; }
        public String iseasydry_text { get; set; }
        public String isambientair_text { get; set; }

        public String iszeissaxioimager2_text { get; set; }
        public String ismeasuringsoftware_text { get; set; }
        public String isautomated_text { get; set; }
        public String material_id_text { get; set; }
        public String lbPermembrane_text { get; set; }
        public String totalResidueWeight { get; set; }


        public String isfiltrationmethod_text { get; set; }

        public byte[] img1 { get; set; }
        public byte[] img2 { get; set; }
        public byte[] img3 { get; set; }
        public byte[] img4 { get; set; }
        public byte[] img5 { get; set; }
        #endregion

        //public byte[] img1 { get; set; }
        //public byte[] img2 { get; set; }
        //public byte[] img3 { get; set; }
        //public byte[] img4 { get; set; }
        //public byte[] img5 { get; set; }

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
