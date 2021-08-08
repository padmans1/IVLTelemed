using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Application.Models.Telemed
{
    public class AddressInfo: AuditableEntity<int>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public double Pincode { get; set; }
        public string Email { get; set; }

        public string MobExt { get; set; }
        public Double Mobile { get; set; }



    }
}
