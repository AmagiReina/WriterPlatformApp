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
    public class RatingBOImpl : GeneralImpl<RatingBO>
    {
        public RatingBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
           : base(unitOfWork, mapper)
        {

        }

        #region Standard Queries
        public override RatingBO FindById(int? id)
        {
            var rating = unitOfWork.Rating.FindById(id);

            return mapper.Map<RatingBO>(rating);
        }

        public override IEnumerable<RatingBO> GetAll()
        {
            IEnumerable<Rating> ratings = unitOfWork.Rating.GetAll();

            var ratingMap = mapper.Map<IEnumerable<Rating>, List<RatingBO>>(ratings);

            return ratingMap;
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override void Save(RatingBO businessObject)
        {
            var rating = mapper.Map<Rating>(businessObject);

            if (rating.Id == 0)
            {
                Add(rating);
            }
            else
            {
                rating = unitOfWork.Rating.FindById(businessObject.Id);
                var ratingEntry = 
                unitOfWork.Rating.SaveSingleField(rating);
                ratingEntry.CurrentValues.SetValues(businessObject);
                unitOfWork.Rating.Save();
                //Update(rating);
            }
        }

        private void Add(Rating rating)
        {
            unitOfWork.Rating.Create(rating);
            unitOfWork.Rating.Save();
        }

        private void Update(Rating rating)
        {
            unitOfWork.Rating.Update(rating);           
            unitOfWork.Rating.Save();
        }
        #endregion

        #region AsyncQueries
        public async Task<IEnumerable<RatingBO>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }
        #endregion

        #region Additional
        public RatingBO FindByNameAndTitle(int titleId, string userId)
        {
            var rating = unitOfWork.Rating.Include("RatingTypes")
                .Where(x => x.TitleId == titleId)
                .Where(x => x.UserProfilesId == userId).FirstOrDefault();

            return mapper.Map<RatingBO>(rating);
        }

        public bool FindExist(int titleId, string userId)
        {
            var rating = unitOfWork.Rating.Include("RatingTypes")
                .Where(x => x.TitleId == titleId)
                .Where(x => x.UserProfilesId == userId).Any();

            return rating;
        }
        #endregion
    }
}
