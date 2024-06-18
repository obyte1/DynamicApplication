﻿using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.Data.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Authorizer> Authorizers { get; set; }

    }
}