using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class IndexTenantViewModel
    {
        public int TenantId { get; set; }
        public string TenantFirstName { get; set; }
        public string TenantLastName { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int FlatNumber { get; set; }
        public int BuildingId { get; set; }
        public int FlatId { get; set; }
        public string BuildingAddress { get; set; }



    }
}