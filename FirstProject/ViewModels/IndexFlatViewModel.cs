using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstProject.ViewModels
{
    public class IndexFlatViewModel
    {
        public string BuildingAddress { get; set; }
        public int FlatId { get; set; }
        public string BuildingName { get; set; } 
        public int Floor { get; set; }
        public int Number { get; set; }
        public int Bedroom { get; set; }
        public bool Parking { get; set; }
        public bool PetAllowed { get; set; }
        public bool Furnished { get; set; }
        public bool BillsIncluded { get; set; }
    }
}