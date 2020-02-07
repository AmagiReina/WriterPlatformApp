using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public class GenreBOImpl : GeneralImpl<GenreBO>
    {
        public GenreBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
            :base(unitOfWork, mapper)
        {

        }

        public override IEnumerable<GenreBO> GetAll()
        {
            IEnumerable<Genre> genres = unitOfWork.Genre.GetAll();

            var genreMap = mapper.Map<IEnumerable<Genre>, List<GenreBO>>(genres);

            return genreMap;
        }

        public override GenreBO FindById(int id)
        {
            var genre = unitOfWork.Genre.FindById(id);

            return mapper.Map<GenreBO>(genre);
        }

        public override void Save(GenreBO businessObject)
        {
            var genre = mapper.Map<Genre>(businessObject);

            if (genre.Id == 0)
            {
                Add(genre);
            } 
            else
            {
                Update(genre);
            }
        }

        public override void Remove(int id)
        {
            if (id != 0)
            {
                unitOfWork.Genre.Delete(id);
                unitOfWork.Genre.Save();
            }
        }

        private void Add(Genre genre)
        {
            unitOfWork.Genre.Create(genre);
            unitOfWork.Genre.Save();
        }

        private void Update(Genre genre)
        {
            unitOfWork.Genre.Update(genre);
            unitOfWork.Genre.Save();
        }

    }
}
