using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Web.SyncF.Models;

namespace Web.SyncF.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Employees> Employees { get; set; }
        public DbSet<IdentityUser> AspNetUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        
        }
    }
}
