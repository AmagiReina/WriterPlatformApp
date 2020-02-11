using AutoMapper;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Implementatiton;
using WriterPlatformApp.WEB.App_Start;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Controllers
{
    public class TitleController : Controller
    {
        private TitleBOImpl titleBo;
        private IMapper mapper;

        public TitleController(IMapper mapper)
        {
            titleBo = NinjectConfig.GetTitleBO();
            this.mapper = mapper;
        }

        [Authorize]
        // GET: Title/Index
        public async Task<ActionResult> Index()
        {
            IEnumerable<TitleBO> titles = await titleBo.GetAllTitlesAsync();

            foreach (var item in titles)
            {
                titleBo.CalculateRating(item);
            }
                    
            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            
            return View(viewModel);
        }
       

        [Authorize]
        [HttpGet]
        // GET: Title/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var title = titleBo.FindById(id);

            var viewModel = mapper.Map<TitleBO, TitleViewModel>(title);

            return View(viewModel);          
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TitleViewModel title)
        {
            if (ModelState.IsValid)
            {
                title.UserProfilesId = User.Identity.GetUserId();
                var titleBO = mapper.Map<TitleViewModel, TitleBO>(title);
                titleBo.SetStart(titleBO);
                titleBo.Save(titleBO);
            }
            else
                return View(title);

            return RedirectToAction("Index");
        }

    }
}