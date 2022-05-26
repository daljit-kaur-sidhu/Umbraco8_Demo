using K_UmbracoDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;

namespace K_UmbracoDemo.Controller
{
    public class AccountController : SurfaceController
    {
        private IMemberService _memberService;
        private IMemberTypeService _memberTypeService;
        public AccountController(IMemberService memberService, IMemberTypeService memberTypeService)
        {
            _memberService = memberService;
            _memberTypeService = memberTypeService;
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid) {
                var user = _memberService.GetByEmail(loginModel.Email);
                //var isValid = Members.IsLoggedIn();
                var isValid = Members.Login(loginModel.Email, loginModel.Password);
                if (user != null && isValid) {
                    var userRole = Members.GetUserRoles(loginModel.Email);
                    if (userRole.Contains("Admin")) {
                        return RedirectToUmbracoPage(1072);
                    }
                    if (userRole.Contains("User"))
                    {
                        return RedirectToUmbracoPage(1062);
                    }
                }
            }
            return RedirectToCurrentUmbracoUrl();
        }
    }
}