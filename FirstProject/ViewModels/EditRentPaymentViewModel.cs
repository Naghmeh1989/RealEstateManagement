using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class EditRentPaymentViewModel
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool IsPaid { get; set; }
    }
}