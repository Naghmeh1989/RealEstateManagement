using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class DetailsRentPaymentViewModel
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public int PaymentDay { get; set; }
        public Decimal RentAmount { get; set; }
        public string BuildingName { get; set; }
        public int FlatNumber { get; set; }
    }
}
