using System;
using CustomerDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerDemo;

public class CimentalContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    
    public string DbPath { get; }
    
    public CimentalContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "cimental.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}