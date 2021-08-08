using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BlazorHero.CleanArchitecture.Application.Models.Telemed
{
    public class Organization: AuditableEntity<int>
    {
        public String Name { get; set; }

        public AddressInfo Address { get; set; }
       
    }
}
