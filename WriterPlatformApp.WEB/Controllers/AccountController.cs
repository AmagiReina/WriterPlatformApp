using AutoMapper;
using Microsoft.AspNet.Identity;
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
        private readonly IUserBOImpl userBo;
        private readonly IMapper mapper;
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
                if (claim == null && !userBo.GetLocked(user))
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                } 
                else if  (claim == null && userBo.GetLocked(user))
                {
                    ModelState.AddModelError("", "Пользователь удален");
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

        [Authorize]
        [HttpGet]
        public ActionResult Manage()
        {
            string userId = User.Identity.GetUserId();

            var user = userBo.GetUserById(userId);

            var viewModel = mapper.Map<UserBO, UserViewModel>(user);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Manage(UserViewModel model)
        {
            string UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                UserBO user = mapper.Map<UserViewModel, UserBO>(model);
                user.Id = UserId;
                userBo.Edit(user);
            }
            AuthenticationManager.SignOut();
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordViewModel model)
        {         
            string UserId = User.Identity.GetUserId();
                if (ModelState.IsValid)
            {
                ChangePasswordBO user = mapper.Map<ChangePasswordViewModel, 
                    ChangePasswordBO>(model);
                user.Id = UserId;
                userBo.ChangePassword(user);
                AuthenticationManager.SignOut();
            }
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete()
        {
            string userId = User.Identity.GetUserId();

            var user = userBo.GetUserById(userId);

            userBo.Remove(user);

            AuthenticationManager.SignOut();

            return RedirectToAction("Login");
        }
  
    }
}
