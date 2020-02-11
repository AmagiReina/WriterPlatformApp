using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public class RatingTypeBOImpl : GeneralImpl<RatingTypeBO>
    {
        public RatingTypeBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
          : base(unitOfWork, mapper)
        {

        }

        public override RatingTypeBO FindById(int? id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<RatingTypeBO> GetAll()
        {
            IEnumerable<RatingType> ratings = unitOfWork.RatingType.GetAll();

            var ratingMap = mapper.Map<IEnumerable<RatingType>, List<RatingTypeBO>>(ratings);

            return ratingMap;
        }

        public async Task<IEnumerable<RatingTypeBO>> GetRatingsAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public override void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override void Save(RatingTypeBO businessObject)
        {
            throw new NotImplementedException();
        }
    }
}
