﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZZCraft.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDCraftZZEntities1 : DbContext
    {
        public BDCraftZZEntities1()
            : base("name=BDCraftZZEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CraftItems> CraftItems { get; set; }
        public virtual DbSet<InitialRes> InitialRes { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
