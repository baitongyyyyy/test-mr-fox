using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace Project.Context
{
    public class ApplicationDBContext:DbContext
    { 
        public DbSet<Concert> Concert { get; set; }
        public DbSet<ConcertTicket> ConcertTicket { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            string relativePath = @"db\MrFox.db";

            //var parentdir = Path.GetDirectoryName(AppContext.BaseDirectory);
            //string myString = parentdir.Remove(parentdir.IndexOf("bin"),  parentdir.Length - parentdir.IndexOf("bin"));
            //string absolutePath = Path.Combine(myString, relativePath);
             
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = relativePath };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }

}
