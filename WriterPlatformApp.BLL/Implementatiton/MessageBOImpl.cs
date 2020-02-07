using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.DAL.Entities;
using WriterPlatformApp.DAL.UnitOfWork;

namespace WriterPlatformApp.BLL.Implementatiton
{
    public class MessageBOImpl: GeneralImpl<MessageBO>
    {
        public MessageBOImpl(IUnitOfWork unitOfWork, IMapper mapper)
            :base(unitOfWork, mapper)
        {

        }

        public override MessageBO FindById(int id)
        {
            var message = unitOfWork.Message.FindById(id);

            return mapper.Map<MessageBO>(message);
        }

        public override IEnumerable<MessageBO> GetAll()
        {
            IEnumerable<Message> messages = unitOfWork.Message.Include("Titles", "UserProfiles");

            var messageMap = mapper.Map<IEnumerable<Message>, List<MessageBO>>(messages);

            return messageMap;
        }

        public IQueryable<MessageBO> GetMessagesByTitle(int titleId)
        {
            IQueryable<Message> messagesByTitle = unitOfWork.Message.Include("Titles", "UserProfiles")
                .Where(m => m.TitleId == titleId);

            var messageMap = mapper.Map<IQueryable<Message>, List<MessageBO>>
                (messagesByTitle).AsQueryable();

            return messageMap;
        }

        public override void Remove(int id)
        {
            if (id != 0)
            {
                unitOfWork.Message.Delete(id);
                unitOfWork.Message.Save();
            }
        }

        public override void Save(MessageBO businessObject)
        {
            var message = mapper.Map<Message>(businessObject);

            if (message.Id == 0)
            {
                Add(message);
            }
            else
            {
                Update(message);
            }
        }

        private void Add(Message message)
        {
            unitOfWork.Message.Create(message);
            unitOfWork.Message.Save();
        }

        private void Update(Message message)
        {
            unitOfWork.Message.Update(message);
            unitOfWork.Message.Save();
        }
    }
}
