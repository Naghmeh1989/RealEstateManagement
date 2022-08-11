using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class CreateTenantViewModel
    {
        
        public string Password { get; set; }
        public string TenantFirstName { get; set; }
        public string TenantLastName { get; set; }
        public string UserName { get; set; }
        
    }
}