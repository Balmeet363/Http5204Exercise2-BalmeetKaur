using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetGrooming.Models.ViewModels
{
    public class UpdatePet
    {
        //what information dfoes update pet needs
        // needs a list of species and individual pet
        public Pet pet { get; set; }
        public List<Species> species { get; set; }

    }
}