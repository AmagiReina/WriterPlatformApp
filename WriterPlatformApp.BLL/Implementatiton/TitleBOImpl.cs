using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public class TitleBOImpl: GeneralImpl<TitleBO>
    {
        private const int START_RATING = 0;
        public TitleBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
            :base(unitOfWork, mapper)
        {

        }

        #region Standard Queries
        public override TitleBO FindById(int? id)
        {
            var title = unitOfWork.Title.FindById(id);

            var messages = unitOfWork.Title.Include("Messages")
                .Select(x => x.Messages).FirstOrDefault();
            var genres = unitOfWork.Title.Include("Genres")
                .Select(x => x.Genres).FirstOrDefault();
            var users = unitOfWork.Title.Include("UserProfiles")
                .Select(x => x.UserProfiles).FirstOrDefault();

            title.Genres = genres;
            title.UserProfiles = users;

            ICollection<Message> messagesList = messages
                .Where(x => x.TitleId == id).ToList();

            if (messagesList != null)
            {
                title.Messages = messagesList; 
            }

            return mapper.Map<TitleBO>(title);
        }

        public override IEnumerable<TitleBO> GetAll()
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres", "UserProfiles", "Messages");

            var titleMap = mapper.Map<IEnumerable<Title>, List<TitleBO>>(titles);

            return titleMap;
        }

        public override void Remove(int id)
        {
            if (id != 0)
            {
                unitOfWork.Title.Delete(id);
                unitOfWork.Title.Save();
            }
        }

        public override void Save(TitleBO businessObject)
        {
            var title = mapper.Map<Title>(businessObject);

            if (title.Id == 0)
            {
                Add(title);
            }
            else
            {
                Update(title);
            }
        }

        private void Add(Title title)
        {
            unitOfWork.Title.Create(title);
            unitOfWork.Title.Save();
        }

        private void Update(Title title)
        {
            unitOfWork.Title.Update(title);
            unitOfWork.Title.Save();
        }
        #endregion


        #region AsyncQueries
        public async Task<IEnumerable<TitleBO>> GetAllTitlesAsync()
        {
            return await Task.Run(() => GetAll());
        }
        #endregion


        #region Additional
        public void SetStart(TitleBO title)
        {
            title.PublicationDate = DateTime.Now;
            title.Rating = START_RATING;
        }

        public int GetAverage(TitleBO title)
        {
            var ratings = unitOfWork.Rating.GetAll();
            int averageRating = 0;

            if (ratings.Count() > 0 /*&& title.Rating != 0*/)
            {
                double avg = ratings.AsQueryable().AsNoTracking()
                      .Where(x => x.TitleId == title.Id)
                      .Select(x => x.RatingTypes.RatingNumber)
                      .Average();

                averageRating = Convert.ToInt32(avg);
            }

            return averageRating;
        }

        /**
         * Search by author
         * */
        public IEnumerable<TitleBO> SearchByAuthor(string name)
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres", "UserProfiles");

            var found = titles.Where(t => t.UserProfiles.UserName
            .Contains(name));

            var foundMapper = mapper.Map<IEnumerable<Title>, List<TitleBO>>
                (found);

            return foundMapper;
        }

        /**
         * Search by name of title
         * */
        public IEnumerable<TitleBO> SearchByTitleName(string name)
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres", "UserProfiles");

            var found = titles.Where(t => t.TitleName
            .Contains(name));

            var foundMapper = mapper.Map<IEnumerable<Title>, List<TitleBO>>
                (found);

            return foundMapper;
        }

        /**
         * Search by genre
         * */
         public IEnumerable<TitleBO> SearchByGenre(string name)
         {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres", "UserProfiles");

            var found = titles.Where(t => t.Genres.GenreName
            .Contains(name));

            var foundMapper = mapper.Map<IEnumerable<Title>, List<TitleBO>>
                (found);

            return foundMapper;
         }

        /**
         * Sort by genre
         * */
        public IEnumerable<TitleBO> SortByGenre()
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres", 
                "UserProfiles");

            var sorted = titles.OrderByDescending(s => s.Genres.GenreName)
                                .Take(50);

            var sortedMapper = mapper.Map<IEnumerable<Title>, List<TitleBO>>
                (sorted);

            return sortedMapper;
        }

        /**
         * Sort by Rating
         * */
        public IEnumerable<TitleBO> SortByRating()
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres",
                "UserProfiles");

            var sorted = titles.OrderByDescending(x => x.Rating)
                .Take(50);

            var sortedMapper = mapper.Map<IEnumerable<Title>, List<TitleBO>>
               (sorted);

            return sortedMapper;
        }

        /**
         * Sort by Comments Count
         * */
        public IEnumerable<TitleBO> SortByCommentAmount()
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres",
                "UserProfiles", "Messages");

            var sorted = titles.OrderByDescending(x => x.Messages.Count())
                .Take(50);

            var sortedMapper = mapper.Map<IEnumerable<Title>, List<TitleBO>>
               (sorted);

            return sortedMapper;
        }
        
        public void CalculateRating(TitleBO title)
        {
            title.Rating = GetAverage(title);
                 
            Title titleEntity = unitOfWork.Title.FindById(title.Id);

            var titleEntry = 
                unitOfWork.Title.SaveSingleField(titleEntity);

            titleEntry.CurrentValues.SetValues(title);
            unitOfWork.Title.Save();
        }
        #endregion

    }
}
