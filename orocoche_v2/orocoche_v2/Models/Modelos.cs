//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Modelos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modelos()
        {
            this.Reservas = new HashSet<Reservas>();
        }

        public decimal IdModelos { get; set; }
        public int Marca { get; set; }
        public string Modelo { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Potencia { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Peso { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public decimal Año { get; set; }
        public decimal StockTotal { get; set; }
        public int Tipo { get; set; }
        public int Motor { get; set; }
        public Nullable<bool> Premium { get; set; }
        public string Imagen { get; set; }

        public virtual Marca Marca1 { get; set; }
        public virtual TipoCoche TipoCoche { get; set; }
        public virtual TipoMotor TipoMotor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservas> Reservas { get; set; }

    }
}