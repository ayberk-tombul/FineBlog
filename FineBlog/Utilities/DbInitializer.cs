﻿using FineBlog.Data;
using FineBlog.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace FineBlog.Utilities
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if(!_roleManager.RoleExistsAsync(WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.WebsiteAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Firstname = "Super",
                    Lastname = "Admin"
                },"Admin@0011").Wait();

                var appUser = _context.ApplicationUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser!=null)
                {
                    _userManager.AddToRoleAsync(appUser,WebsiteRoles.WebsiteAdmin).GetAwaiter().GetResult();
                }


                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                        Title = "About Us",
                        Slug = "about-Us"
                    },
                    new Page()
                    {
                        Title = "Contact Us",
                        Slug = "contact"
                    },
                    new Page()
                    {
                        Title = "Privacy Policy",
                        Slug = "privacy"
                    }
                };

                _context.Pages.AddRange(listOfPages);
                _context.SaveChanges();

            }
        }
    }
}
