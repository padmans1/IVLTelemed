using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Contexts;
using BlazorHero.CleanArchitecture.Infrastructure.Helpers;
using BlazorHero.CleanArchitecture.Application.Models.Identity;
using BlazorHero.CleanArchitecture.Application.Models.Telemed;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using BlazorHero.CleanArchitecture.Shared.Constants.Role;
using BlazorHero.CleanArchitecture.Shared.Constants.User;
using BlazorHero.CleanArchitecture.Application.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorHero.CleanArchitecture.Infrastructure
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IStringLocalizer<DatabaseSeeder> _localizer;
        private readonly BlazorHeroContext _db;
        private readonly UserManager<BlazorHeroUser> _userManager;
        private readonly RoleManager<BlazorHeroRole> _roleManager;

        public DatabaseSeeder(
            UserManager<BlazorHeroUser> userManager,
            RoleManager<BlazorHeroRole> roleManager,
            BlazorHeroContext db,
            ILogger<DatabaseSeeder> logger,
            IStringLocalizer<DatabaseSeeder> localizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
            _localizer = localizer;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddBasicUser();
            
            for (int i = 0; i < 20; i++)
            {
                var firstName = string.Empty;
                var lastName = string.Empty;
                var email = string.Empty;
                var userName = string.Empty;
                if (i % 2 == 0)
                {
                    firstName = "Sriram";
                    lastName = "Padmanabhan";
                    email = "ram.heman8" + i.ToString() + "@gmail.com";
                    userName = "sriramP8" + i.ToString();
                }
                else
                {
                    firstName = "Anil";
                    lastName = "Murthy";
                    email = "anil.chat8" + i.ToString() + "@gmail.com";
                    userName = "anilM8" + i.ToString();

                }
                // AddVisit(email);
                // var patient = new BlazorHeroUser()
                // {
                //     FirstName = firstName,
                //     LastName = lastName,
                //     Email = email,
                //     DOB = new DateTime(1980 + i, 1, 1),
                //     Gender = "Male",
                //     UserType = UserType.Patient,
                //     UserName = userName
                    
                // };

                // var patientInfo = new PatientInfo()
                // {
                //     MRN = "IVL_" + i.ToString(),
                //     Patient = patient
                // };
                // AddPatient(patientInfo);

            }

            //AddPatient();
            _db.SaveChanges();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new BlazorHeroRole(RoleConstants.AdministratorRole, _localizer["Administrator role with full permissions"]);
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                    _logger.LogInformation(_localizer["Seeded Administrator Role."]);
                }
                //Check if User Exists
                var superUser = new BlazorHeroUser
                {
                    FirstName = "Sriram",
                    LastName = "Padmanabhan",
                    Email = "ram.heman87@gmail.com",
                    UserName = "sriramp87",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserType = UserType.Doctor,
                    OrganizationDetails = new Organization() {Name = "Hospital" },
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddBasicUser()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var basicRole = new BlazorHeroRole(RoleConstants.BasicRole, _localizer["Basic role with default permissions"]);
                var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
                if (basicRoleInDb == null)
                {
                    await _roleManager.CreateAsync(basicRole);
                    _logger.LogInformation(_localizer["Seeded Basic Role."]);
                }
                //Check if User Exists
                var basicUser = new BlazorHeroUser
                {
                    FirstName = "Sharat",
                    LastName = "Kumar",
                    Email = "sharat.kumar@gmail.com",
                    UserName = "sharatk",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    UserType = UserType.Operator,
                    OrganizationDetails = new Organization() {Name = "Test"},
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
                if (basicUserInDb == null)
                {
                    await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                    _logger.LogInformation(_localizer["Seeded User with Basic Role."]);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddPatient(PatientInfo patientInfo)
        {
            Task.Run(async () =>
            {
                var basicUserInDb = await _userManager.FindByEmailAsync(patientInfo.Patient.Email);
                if(basicUserInDb == null)
                {
                    await _userManager.CreateAsync(patientInfo.Patient, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(patientInfo.Patient, RoleConstants.BasicRole);
                    var dbData = await _db.AddAsync<PatientInfo>(patientInfo);
                    _logger.LogInformation(_localizer["Seeded Patient with Basic Role."]);
                }
              
            }).GetAwaiter().GetResult();
        }
        private void AddVisit(string email)
        {
            Task.Run( async () =>
            {
                var basicUserInDb = await _userManager.FindByEmailAsync(email);
                if(basicUserInDb != null)
                {
                    var visit = new Visit(){
                        Patient = basicUserInDb,
                        Description = "Test Visit"
                    };
                   var dbData =  await _db.AddAsync<Visit>(visit);
                }
            }).GetAwaiter().GetResult();
        }
    }
}