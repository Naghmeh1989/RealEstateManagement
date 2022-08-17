using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstProject.ViewModels;
using System.Web.SessionState;
using System.Web.Mvc;

namespace FirstProject.Business
{
    public class LoginBusiness //: IDisposable
    {
        public bool IsRestricted(int? agencyId)
        {
            
            if (agencyId == null )
            {
                return true;
            }
            {
                return false;
            }


        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        this.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}