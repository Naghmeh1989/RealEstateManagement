using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class EditContractViewModel
    {
        public int RentPayDay { get; set; }

        public DateTime EndDate { get; set; }
        public decimal RentAmount { get; set; }
    
        public string TenantLastName { get; set; }

        public string BuildingName { get; set; }
        public int FlatNumber { get; set; }

        public string TenantFirstName { get; set; }
        public DateTime StartDate { get; set; }
    }
}