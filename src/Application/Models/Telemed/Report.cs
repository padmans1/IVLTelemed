using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Application.Models.Identity;

namespace BlazorHero.CleanArchitecture.Application.Models.Telemed
{
    public class Report : AuditableEntity<int>
    {
        public BlazorHeroUser Patient { get; set; }
        public Visit Visit { get; set; }

        public string Description { get; set; }

        public string RightEyeObservations{ get; set; }
        
        public string LeftEyeObservations{ get; set; }
 
        public List<Image> RightEyeImages{ get; set; }

        public List<Image> LeftEyeImages{ get; set; }

        public AddressInfo Address { get; set; }
     

    }
}
