using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arriendos.Models
{
    public class Propiedad
    {
        [Key]
        public int Id { get; set; }
        public string Direccion { get; set; }
        public double Precio { get; set; }
        public int IdCiudad { get; set; }

        public virtual ApplicationUser Usuario { get; set; }

        [ForeignKey("IdCiudad")]
        public virtual Ciudad ciudad { get; set; }


    }
}