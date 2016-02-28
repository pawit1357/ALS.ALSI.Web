using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.Constant;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALS.ALSI.Biz.DataAccess
{
    public partial class template_seagate_lpc_coverpage
    {
        //private static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(template_seagate_lpc_coverpage));

        #region "Property"
        public CommandNameEnum RowState { get; set; }
        #endregion

        private static IRepository<template_seagate_lpc_coverpage> _repository
        {
            get { return ObjectFactory.GetInstance<IRepository<template_seagate_lpc_coverpage>>(); }
        }

        public IEnumerable<template_seagate_lpc_coverpage> SelectAll()
        {
            return _repository.GetAll().ToList();
        }

        public template_seagate_lpc_coverpage SelectByID(int _id, string _type)
        {
            return _repository.First(x => x.sample_id == _id && x.lpc_type.Equals(_type));
        }

        public void Insert()
        {
            _repository.Add(this);
        }

        public void Update()
        {
            template_seagate_lpc_coverpage existing = _repository.Find(x => x.sample_id == this.sample_id && x.particle_type.Equals(this.particle_type)).FirstOrDefault();
            _repository.Edit(existing, this);
        }

        public void Delete()
        {
            _repository.Delete(this);
        }

        #region "Custom"

        public void InsertList(List<template_seagate_lpc_coverpage> _lists)
        {
            foreach (template_seagate_lpc_coverpage tmp in _lists)
            {
                _repository.Add(tmp);
            }
        }
        public void UpdateList(List<template_seagate_lpc_coverpage> _lists)
        {
            foreach (template_seagate_lpc_coverpage tmp in _lists)
            {
                tmp.Update();
            }
        }


        public List<template_seagate_lpc_coverpage> SelectBySampleID(int _sample_id)
        {
            return _repository.Find(x => x.sample_id == _sample_id).ToList();
        }


        public void DeleteBySampleID(int _sampleID)
        {
            List<template_seagate_lpc_coverpage> lists = _repository.Find(x => x.sample_id == _sampleID).ToList();
            foreach (template_seagate_lpc_coverpage tmp in lists)
            {
                _repository.Delete(tmp);
            }
        }

        //public void InsertList(List<template_seagate_lpc_coverpage> _lists)
        //{
        //    foreach (template_seagate_lpc_coverpage tmp in _lists)
        //    {
        //        switch (tmp.RowState)
        //        {
        //            case CommandNameEnum.Add:
        //                _repository.Add(tmp);
        //                break;
        //            case CommandNameEnum.Edit:
        //                template_seagate_lpc_coverpage existing = _repository.Find(x => x.ID == tmp.ID).FirstOrDefault();
        //                _repository.Edit(existing, tmp);
        //                break;
        //        }

        //    }
        //}
        //public void UpdateList(List<template_seagate_lpc_coverpage> _lists)
        //{
        //    foreach (template_seagate_lpc_coverpage tmp in _lists)
        //    {
        //        tmp.Update();
        //    }
        //}
        public static List<template_seagate_lpc_coverpage> FindAllBySampleID(int _sampleID)
        {
            return _repository.GetAll().Where(x => x.sample_id == _sampleID).ToList();
        }

        #endregion
        public double Average(Double[] valueList)
        {
            double result = 0.0;
            foreach (double value in valueList)
            {
                result += value;
            }
            return result / valueList.Length;
        }
        public List<ReportLPC> generateReport(List<template_seagate_lpc_coverpage> covers)
        {
            List<ReportLPC> result = new List<ReportLPC>();
            template_seagate_lpc_coverpage cover03 = covers[0];

            tb_m_specification spec = new tb_m_specification().SelectByID(covers[0].specification_id.Value);// covers[0].tb_m_specification;

            ReportLPC tmp = new ReportLPC();
            //0.3
            tmp.liquidParticleCount = "Total number of particles ≥ 0.3μm ";
            tmp.specificationLimit = string.Empty;
            tmp.result = string.Empty;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_03);
            tmp.lpc_type = Convert.ToInt16(cover03.lpc_type);
            tmp.unit = spec.E;
            result.Add(tmp);
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "1st Run";
            tmp.specificationLimit = string.Empty;
            tmp.result = cover03.b25;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_03);
            tmp.lpc_type = Convert.ToInt16(cover03.lpc_type);
            tmp.unit = spec.E;
            if (cover03.item_visible[0] == '1')
            {
                result.Add(tmp);
            }
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "2nd Run";
            tmp.specificationLimit = string.Empty;
            tmp.result = cover03.d25;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_03);
            tmp.lpc_type = Convert.ToInt16(cover03.lpc_type);
            tmp.unit = spec.E;
            if (cover03.item_visible[1] == '1')
            {
                result.Add(tmp);
            }
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "3rd Run";
            tmp.specificationLimit = string.Empty;
            tmp.result = cover03.f25;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_03);
            tmp.lpc_type = Convert.ToInt16(cover03.lpc_type);
            tmp.unit = spec.E;
            if (cover03.item_visible[2] == '1')
            {
                result.Add(tmp);
            }
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "Average";
            tmp.specificationLimit = cover03.lpc_type.Equals("1") ? spec.F : spec.H;
            tmp.result = Math.Round(Average(new Double[] { Convert.ToDouble(cover03.b25), Convert.ToDouble(cover03.d25), Convert.ToDouble(cover03.f25) }), 0).ToString();
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_03);
            tmp.lpc_type = Convert.ToInt16(cover03.lpc_type);
            tmp.unit = spec.E;
            if (cover03.item_visible[3] == '1')
            {
                result.Add(tmp);
            }
            //0.6
            template_seagate_lpc_coverpage cover06 = covers[1];
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "Total number of particles ≥ 0.6μm ";
            tmp.specificationLimit = string.Empty;
            tmp.result = string.Empty;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_06);
            tmp.lpc_type = Convert.ToInt16(cover06.lpc_type);
            tmp.unit = spec.E;
            result.Add(tmp);
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "1st Run";
            tmp.specificationLimit = string.Empty;
            tmp.result = cover06.b25;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_06);
            tmp.lpc_type = Convert.ToInt16(cover06.lpc_type);
            tmp.unit = spec.E;
            if (cover06.item_visible[0] == '1')
            {
                result.Add(tmp);
            }
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "2nd Run";
            tmp.specificationLimit = string.Empty;
            tmp.result = cover06.d25;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_06);
            tmp.lpc_type = Convert.ToInt16(cover06.lpc_type);
            tmp.unit = spec.E;
            if (cover06.item_visible[1] == '1')
            {
                result.Add(tmp);
            }
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "3rd Run";
            tmp.specificationLimit = string.Empty;
            tmp.result = cover06.f25;
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_06);
            tmp.lpc_type = Convert.ToInt16(cover06.lpc_type);
            tmp.unit = spec.E;
            if (cover06.item_visible[2] == '1')
            {
                result.Add(tmp);
            }
            tmp = new ReportLPC();
            tmp.liquidParticleCount = "Average";
            tmp.specificationLimit = cover06.lpc_type.Equals("1") ? spec.G : spec.I;
            tmp.result = Math.Round(Average(new Double[] { Convert.ToDouble(cover06.b25), Convert.ToDouble(cover06.d25), Convert.ToDouble(cover06.f25) }), 0).ToString();
            tmp.particle_type = Convert.ToInt16(ParticleTypeEnum.PAR_06);
            tmp.lpc_type = Convert.ToInt16(cover06.lpc_type);
            tmp.unit = spec.E;
            if (cover06.item_visible[3] == '1')
            {
                result.Add(tmp);
            }

            return result;
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
