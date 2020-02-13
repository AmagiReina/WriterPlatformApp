using AutoMapper;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.DAL.Entities;

namespace WriterPlatformApp.BLL.DI
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            // Genre
            CreateMap<Genre, GenreBO>()
            .ConstructUsing(c => DependencyResolver.Current.GetService<GenreBO>());
            CreateMap<GenreBO, Genre>()
            .ConstructUsing(c => DependencyResolver.Current.GetService<Genre>());

            // Rating
            CreateMap<Rating, RatingBO>()
                .ForMember(dest => dest.RatingTypes, opt => opt.MapFrom(x => x.RatingTypes));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<RatingBO>());
            CreateMap<RatingBO, Rating>()
                .ForMember(dest => dest.RatingTypes, opt => opt.MapFrom(x => x.RatingTypes));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<Rating>());

            // RatingType
            CreateMap<RatingType, RatingTypeBO>()
           .ConstructUsing(c => DependencyResolver.Current.GetService<RatingTypeBO>());
            CreateMap<RatingTypeBO, RatingType>()
            .ConstructUsing(c => DependencyResolver.Current.GetService<RatingType>());

            // UserProfile
            CreateMap<UserProfile, UserBO>()
            .ConstructUsing(c => DependencyResolver.Current.GetService<UserBO>());
            CreateMap<UserBO, UserProfile>()
            .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore())
            .ConstructUsing(c => DependencyResolver.Current.GetService<UserProfile>());

            // User
            CreateMap<ApplicationUser, UserBO>()
            .ConstructUsing(c => DependencyResolver.Current.GetService<UserBO>());
            CreateMap<UserBO, ApplicationUser>()
            .ConstructUsing(c => DependencyResolver.Current.GetService<ApplicationUser>());

            // Message
            CreateMap<Message, MessageBO>()
                .ForMember(dest => dest.Titles, opt => opt.MapFrom(x => x.Titles))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles));
            CreateMap<MessageBO, Message>()
                .ForMember(dest => dest.Titles, opt => opt.MapFrom(x => x.Titles))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<Message>());

            // Title
            CreateMap<Title, TitleBO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(x => x.Genres))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(x => x.Messages));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<TitleBO>());
            CreateMap<TitleBO, Title>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(x => x.Genres))
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(x => x.UserProfiles))
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(x => x.Messages));
            //.ConstructUsing(c => DependencyResolver.Current.GetService<Title>());
        }
    }
}
