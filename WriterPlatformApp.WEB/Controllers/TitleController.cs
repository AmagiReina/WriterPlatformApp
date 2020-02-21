using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Implementatiton;
using WriterPlatformApp.WEB.App_Start;
using WriterPlatformApp.WEB.Helpers;
using WriterPlatformApp.WEB.Modules;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Controllers
{
    public class TitleController : Controller
    {
        private readonly TitleBOImpl titleBo;
        private readonly IMapper mapper;
        private readonly int pageSize = 9;
        private const int NUMBER_ONE = 1;

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
        public ActionResult Create(TitleViewModel title, HttpPostedFileBase uploadPdfFile)
        {
            if (ModelState.IsValid)
            {
                using (var binaryReader = new BinaryReader(uploadPdfFile.InputStream))
                {
                    if (uploadPdfFile != null && uploadPdfFile.ContentLength > 0)
                    {                       
                        PdfSaveModule pdfFile = new PdfSaveModule(
                            title.TitleName + title.UserProfilesId, Server.MapPath("~/Files/"));                   
                        uploadPdfFile.SaveAs(Server.MapPath("~/Files/") + pdfFile.fileName);

                        var path = pdfFile.GetPath();
                        title.ContentPath = path;                      
                    }
                }
                title.UserProfilesId = User.Identity.GetUserId();
                var titleBO = mapper.Map<TitleViewModel, TitleBO>(title);
                titleBo.SetStart(titleBO);
                titleBo.Save(titleBO);
            }
            else
                return View(title);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileResult ReadFile(int id)
        {
            var title = titleBo.FindById(id);
            byte[] fileBytes = null;
            if (title != null)
            {
                string titlePath = Server.MapPath("~/Files/") + title.TitleName + ".pdf";
                fileBytes = PdfSaveModule.ReadFile(titlePath);             
            }
            return File(fileBytes, "application/pdf");
        }

        public ActionResult GetAllTitles(int page)
        {            
            IEnumerable<TitleBO> titles = titleBo.GetAll();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                                .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
                (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView(titlePages);
        }

        public ActionResult SearchByAuthor(string name, int page)
        {
            var titles = titleBo.SearchByAuthor(name);

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                                .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
                (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView("GetAllTitles", titlePages);
        }

        public ActionResult SearchByTitleName(string name, int page)
        {
            var titles = titleBo.SearchByTitleName(name);

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                                .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
                (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView("GetAllTitles", titlePages);
        }

        public ActionResult SearchByGenre(string name, int page)
        {
            var titles = titleBo.SearchByGenre(name);

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                                .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
                (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView("GetAllTitles", titlePages);
        }

        public ActionResult SortByGenre(int page)
        {
            var titles = titleBo.SortByGenre();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                               .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
               (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView("GetAllTitles", titlePages);
        }

        public ActionResult SortByRating(int page)
        {
            var titles = titleBo.SortByRating();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                               .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
               (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView("GetAllTitles", titlePages);
        }

        public ActionResult SortByComment(int page)
        {
            var titles = titleBo.SortByCommentAmount();

            var viewModel = mapper.Map<IEnumerable<TitleBO>, List<TitleViewModel>>
                (titles);

            var titlePages = new PagedData<TitleViewModel>
            {
                Data = viewModel.Skip((page - NUMBER_ONE) * pageSize)
                               .Take(pageSize).ToList(),

                NumberOfPages = Convert.ToInt32
               (Math.Ceiling((double)viewModel.Count() / pageSize)),

                CurrentPage = page
            };

            return PartialView("GetAllTitles", titlePages);
        }

    }
}