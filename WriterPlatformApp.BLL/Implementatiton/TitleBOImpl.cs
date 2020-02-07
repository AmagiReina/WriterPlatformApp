using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public class TitleBOImpl: GeneralImpl<TitleBO>
    {
        public TitleBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
            :base(unitOfWork, mapper)
        {

        }

        public override TitleBO FindById(int id)
        {
            var title = unitOfWork.Title.FindById(id);

            var messages = unitOfWork.Title.Include("Message").Select(x => x.Messages).FirstOrDefault();


            ICollection<Message> messagesList = messages.Where(x => x.TitleId == id).ToList();

            if (messagesList != null)
            {
                title.Messages = messagesList; 
            }

            return mapper.Map<TitleBO>(title);
        }

        public List<MessageBO> GetMessagesByTitle(TitleBO titleBO)
        {
            var messages = titleBO.Messages.ToList();

            return messages;
        }

        public override IEnumerable<TitleBO> GetAll()
        {
            IEnumerable<Title> titles = unitOfWork.Title.Include("Genres", "UserProfiles", "Messages");

            var titleMap = mapper.Map<IEnumerable<Title>, List<TitleBO>>(titles);

            return titleMap;
        }


        public async Task<IEnumerable<TitleBO>> GetAllTitlesAsync()
        {
            return await Task.Run(() => GetAll());
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
    }
}
