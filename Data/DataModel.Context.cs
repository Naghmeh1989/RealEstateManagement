﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FirstProjectEntities : DbContext
    {
        public FirstProjectEntities()
            : base("name=FirstProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<RentPayment> RentPayments { get; set; }
    }
}
