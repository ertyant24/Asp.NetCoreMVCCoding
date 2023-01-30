﻿using Microsoft.EntityFrameworkCore;

namespace Asp.NetCoreMVCCoding.Datas
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
