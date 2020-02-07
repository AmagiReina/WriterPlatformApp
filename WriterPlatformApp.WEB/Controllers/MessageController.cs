using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Implementatiton;
using WriterPlatformApp.WEB.App_Start;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Controllers
{
    public class MessageController : Controller
    {
        private MessageBOImpl messageBo;
        private IMapper mapper;

        public MessageController(IMapper mapper)
        {
            messageBo = NinjectConfig.GetMessageBO();
            this.mapper = mapper;
        }

        public ActionResult Index(int id)
        {
            var messages = messageBo.GetMessagesByTitle(id);

            var viewModel = mapper.Map<IEnumerable<MessageBO>,List<MessageViewModel>>
                (messages);


            return PartialView(viewModel);
        }

        // GET: Message
        [HttpGet]
        public ActionResult Add()
        {
            return PartialView();
        }

        public JsonResult Add(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserProfilesId = User.Identity.GetUserId();
                MessageBO message = mapper.Map<MessageViewModel, MessageBO>(model);
                messageBo.Save(message);
            }

                return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}