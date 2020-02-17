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
        private readonly TitleBOImpl titleBo;
        private readonly IMapper mapper;

        public TitleController(IMapper mapper)
        {
            titleBo = NinjectConfig.GetTitleBO();
            this.mapper = mapper;
        }

        [Authorize]
        // GET: Title/Index
        public ActionResult Index()
        {
            return View();
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
            var newTitle = titleBo.FindById(id);

            var viewModel = mapper.Map<TitleBO, TitleViewModel>(newTitle);

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

        public async Task<ActionResult> GetAllTitles()
        {
            IEnumerable<TitleBO> titles = await titleBo.GetAllTitlesAsync();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);


            return PartialView(viewModel);
        }

        public ActionResult SearchByAuthor(string name)
        {
            var titles = titleBo.SearchByAuthor(name);

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            return PartialView("GetAllTitles", viewModel);
        }

        public ActionResult SearchByTitleName(string name)
        {
            var titles = titleBo.SearchByTitleName(name);

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            return PartialView("GetAllTitles", viewModel);
        }

        public ActionResult SearchByGenre(string name)
        {
            var titles = titleBo.SearchByGenre(name);

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            return PartialView("GetAllTitles", viewModel);
        }

        public ActionResult SortByGenre()
        {
            var titles = titleBo.SortByGenre();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            return PartialView("GetAllTitles", viewModel);
        }

        public ActionResult SortByRating()
        {
            var titles = titleBo.SortByRating();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            return PartialView("GetAllTitles", viewModel);
        }

        public ActionResult SortByComment()
        {
            var titles = titleBo.SortByCommentAmount();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            return PartialView("GetAllTitles", viewModel);
        }

    }
}