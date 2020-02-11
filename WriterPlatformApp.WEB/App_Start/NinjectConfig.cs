using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using WriterPlatformApp.BLL.DI;
using WriterPlatformApp.BLL.Implementatiton;

namespace WriterPlatformApp.WEB.App_Start
{
    public static class NinjectConfig
    {
        private static StandardKernel kernel;

        public static void Configure()
        {
            NinjectModule businessModule = new BusinessModule("DefaultConnection");
            NinjectModule presentationModule = new Modules.PresentationModule();
            kernel = new StandardKernel(businessModule, presentationModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        public static GenreBOImpl GetGenreBO()
        {
            return kernel.Get<GenreBOImpl>();
        }

        public static RatingBOImpl GetRatingBO()
        {
            return kernel.Get<RatingBOImpl>();
        }

        public static RatingTypeBOImpl GetRatingTypeBO()
        {
            return kernel.Get<RatingTypeBOImpl>();
        }

        public static MessageBOImpl GetMessageBO()
        {
            return kernel.Get<MessageBOImpl>();
        }

        public static TitleBOImpl GetTitleBO()
        {
            return kernel.Get<TitleBOImpl>();
        }

        public static IUserBOImpl GetUserBO()
        {
            return kernel.Get<UserBOImpl>();
        }
    }
}