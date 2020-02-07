using AutoMapper;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.DI
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            // Genre
            CreateMap<GenreViewModel, GenreBO>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<GenreBO>());
            CreateMap<GenreBO, GenreViewModel>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<GenreViewModel>());

            // UserForRegister
            CreateMap<RegisterViewModel, UserBO>()
               .ConstructUsing(c => DependencyResolver.Current.GetService<UserBO>());
            CreateMap<UserBO, RegisterViewModel>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<RegisterViewModel>());

            // UserForLogin
            CreateMap<LoginViewModel, UserBO>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<UserBO>());
            CreateMap<UserBO, LoginViewModel>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<LoginViewModel>());

            // User
            CreateMap<UserViewModel, UserBO>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<UserBO>());
            CreateMap<UserBO, UserViewModel>()
              .ConstructUsing(c => DependencyResolver.Current.GetService<UserViewModel>());

            // Message
            CreateMap<MessageViewModel, MessageBO>()
                .ForMember(dest => dest.Titles, opt => opt.MapFrom(x => x.Titles))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<MessageBO>());
            CreateMap<MessageBO, MessageViewModel>()
                .ForMember(dest => dest.Titles, opt => opt.MapFrom(x => x.Titles))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<MessageViewModel>());

            // Title
            CreateMap<TitleViewModel, TitleBO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(x => x.Genres))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(x => x.Messages));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<TitleBO>());
            CreateMap<TitleBO, TitleViewModel>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(x => x.Genres))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(x => x.Messages));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<TitleViewModel>());

        }
    }
}