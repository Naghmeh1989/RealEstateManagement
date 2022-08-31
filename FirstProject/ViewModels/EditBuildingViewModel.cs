using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace FirstProject.ViewModels
{
    public class EditBuildingViewModel
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfFlats { get; set; }
        public List<Flat> Flats { get; set; }
        public int FlatId { get; set; }

        
        public int Floor { get; set; }
        public int Number { get; set; }
        public int Bedroom { get; set; }
        public bool Parking { get; set; }
        public bool PetAllowed { get; set; }
        public bool Furnished { get; set; }
        public bool BillsIncluded { get; set; }
    }
}