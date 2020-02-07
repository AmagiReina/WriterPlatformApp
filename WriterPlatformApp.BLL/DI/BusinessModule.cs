using Ninject.Modules;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.DI
{
    public class BusinessModule : NinjectModule
    {
        private string connectionString;

        public BusinessModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWorkImpl>().WithConstructorArgument(connectionString);
        }
    }
}
