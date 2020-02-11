using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Implementatiton;
using WriterPlatformApp.WEB.App_Start;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Controllers
{
    public class RatingController : Controller
    {
        private RatingBOImpl ratingBo;
        private IMapper mapper;

        public RatingController(IMapper mapper)
        {
            ratingBo = NinjectConfig.GetRatingBO();
            this.mapper = mapper;
        }

        public JsonResult Get(int titleId)
        {
            string userId = User.Identity.GetUserId();

            var rating = ratingBo.FindByNameAndTitle(titleId, userId);

            var viewModel = mapper.Map<RatingViewModel>(rating);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Add()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Add(RatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserProfilesId = User.Identity.GetUserId();
                RatingBO rating = mapper.Map<RatingViewModel, RatingBO>(model);
                bool found = ratingBo.FindExist(model.TitleId,
                    model.UserProfilesId);

                if (found)
                {
                    rating.Id = ratingBo.FindByNameAndTitle(model.TitleId,
                        model.UserProfilesId).Id;
                }
                ratingBo.Save(rating);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}