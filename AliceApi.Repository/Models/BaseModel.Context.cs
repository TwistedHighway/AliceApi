﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AliceApi.Repository.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseEntities : DbContext
    {
        public BaseEntities()
            : base("name=BaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieMPAA> MovieMPAAs { get; set; }
        public DbSet<Movie> Movies1 { get; set; }
        public DbSet<MemberProfile> MemberProfiles { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
    }
}
