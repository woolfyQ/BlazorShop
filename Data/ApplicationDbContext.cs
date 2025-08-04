﻿using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }    



    }
}
