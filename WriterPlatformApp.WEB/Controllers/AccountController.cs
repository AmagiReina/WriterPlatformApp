using AutoMapper;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WriterPlatformApp.BLL.BO;
using WriterPlatformApp.BLL.Identity;
using WriterPlatformApp.BLL.Implementatiton;
using WriterPlatformApp.WEB.App_Start;
using WriterPlatformApp.WEB.ViewModels;

namespace WriterPlatformApp.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserBOImpl userBo;
        private IMapper mapper;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IMapper mapper)
        {
            userBo = NinjectConfig.GetUserBO();
            this.mapper = mapper;
        }

        // Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {            
                UserBO user = mapper.Map<LoginViewModel, UserBO>(model);
                ClaimsIdentity claim = await userBo.Authenticate(user);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        // Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.isLocked = false;
                model.Role = "user";
                UserBO user = mapper.Map<RegisterViewModel, UserBO>(model);

                OperationDetails operationDetails = await userBo.Create(user);
                if (operationDetails.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(operationDetails.Property,
                        operationDetails.Message);

            }
            else return View(model);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
  
    }
}