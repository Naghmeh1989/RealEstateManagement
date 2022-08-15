using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class CreateContractViewModel
    {
        public int RentPayDay { get; set; }
        
        public DateTime EndDate{ get; set; }
        public decimal RentAmount { get; set; }
        public int FlatId { get; set; }

        public int TenantId { get; set; }
        public int AgencyId { get; set; }
        public string TenantLastName { get; set; }
        
        public string Address { get; set; }

        public string TenantFirstName { get; set; }
        public DateTime StartDate { get; set; }
    }
    
}