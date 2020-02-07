using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Index()
        {
            IEnumerable<TitleBO> titles = titleBo.GetAll();
            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            
            return View(viewModel);
        }
       

        [Authorize]
        [HttpGet]
        // GET: Title/Details
        public ActionResult Details(int id)
        {
            var title = titleBo.FindById(id);

            var viewModel = mapper.Map<TitleBO, TitleViewModel>(title);

            return View(viewModel);          
        }




    }
}