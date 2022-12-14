using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class DetailsContractViewModel
    {
        public int ContractId { get; set; }
        public string BuildingName { get; set; }
        public int FlatNumber { get; set; }
        public string TenantFirstName { get; set; }
        public string TenantLastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PaymentDay { get; set; }
        public Decimal RentAmount { get; set; }
        public List<RentPayment> RentPayments { get; set; }
    }
}