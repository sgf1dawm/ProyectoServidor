﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace orocoche_v2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OroCocheEntities : DbContext
    {
        public OroCocheEntities()
            : base("name=OroCocheEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Modelos> Modelos { get; set; }
        public virtual DbSet<Reservas> Reservas { get; set; }
        public virtual DbSet<Sedes> Sedes { get; set; }
        public virtual DbSet<TipoCoche> TipoCoche { get; set; }
        public virtual DbSet<TipoMotor> TipoMotor { get; set; }
    }
}