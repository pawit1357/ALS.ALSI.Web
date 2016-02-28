using ALS.ALIS.Repository.Base;
using ALS.ALIS.Repository.Interface;
using ALS.ALSI.Biz.DataAccess;
using StructureMap;

namespace ALS.ALSI.Biz
{
    public static class GeneralManager
    {
        public static void Initialize()
        {
            // Hook up the interception
            ObjectFactory.Initialize(
                x =>
                {
                    x.ForRequestedType<IUnitOfWorkFactory>().TheDefaultIsConcreteType<EFUnitOfWorkFactory>();
                    x.ForRequestedType(typeof(IRepository<>)).TheDefaultIsConcreteType(typeof(EFRepository<>));
                }
            );
            // We tell the concrete factory what EF model we want to use
            EFUnitOfWorkFactory.SetObjectContext(() => new ALSIEntities());
        }

        public static void Commit()
        {
            UnitOfWork.Commit();
        }

        public static void Dispose()
        {
            UnitOfWork.Current.Dispose();
        }
    }
}
