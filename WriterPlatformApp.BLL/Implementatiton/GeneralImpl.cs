using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public abstract class GeneralImpl<T> where T: class
    {
        protected IUnitOfWork unitOfWork;
        protected IMapper mapper;

        public GeneralImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public abstract IEnumerable<T> GetAll();
        public abstract T FindById(int id);
        public abstract void Save(T businessObject);
        public abstract void Remove(int id);
    }
}
