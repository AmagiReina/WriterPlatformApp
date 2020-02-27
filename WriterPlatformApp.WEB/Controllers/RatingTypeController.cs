using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Implementatiton;
using WriterPlatformApp.WEB.App_Start;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Controllers
{
    public class RatingTypeController : Controller
    {
        private readonly RatingTypeBOImpl ratingTypeBo;
        private readonly IMapper mapper;

        public RatingTypeController(IMapper mapper)
        {
            ratingTypeBo = NinjectConfig.GetRatingTypeBO();
            this.mapper = mapper;
        }
        // GET: RatingType
        public async Task<ActionResult> Index()
        {
            var ratingTypes = await ratingTypeBo.GetRatingsAsync();

            var viewModel = mapper.Map
                <IEnumerable<RatingTypeBO>, List<RatingTypeViewModel>>
                (ratingTypes);

            return PartialView(viewModel);
        }
    }
}