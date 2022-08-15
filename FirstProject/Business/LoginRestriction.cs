using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstProject.ViewModels;
using System.Web.SessionState;
using System.Web.Mvc;

namespace FirstProject.Business
{
    public class LoginRestriction : Controller
    {
        public bool IsRestricted()
        { 
            if (Session["agencyId"] == null)
            {
                return true;
            }
            {
                return false;
            }


        }
    }
}