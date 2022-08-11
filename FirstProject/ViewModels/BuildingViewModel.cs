using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using FirstProject.ViewModels;

namespace FirstProject.ViewModels
{
    public class BuildingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int NumberOfFlats { get; set; }
        public List<Flat> Flats { get; set; }

    }
}