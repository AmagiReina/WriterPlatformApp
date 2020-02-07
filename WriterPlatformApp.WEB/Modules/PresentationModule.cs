using AutoMapper;
using Ninject;
using Ninject.Modules;
using WriterPlatformApp.BLL.DI;
using WriterPlatformApp.WEB.DI;

namespace WriterPlatformApp.WEB.Modules
{
    public class PresentationModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx =>
            new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BusinessProfile());
                cfg.AddProfile(new ViewModelProfile());

            });
            return config;
        }
    }
}