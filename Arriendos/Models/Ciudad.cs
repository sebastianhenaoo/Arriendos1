using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arriendos.Models
{
    public class Ciudad
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int IdDepartamento { get; set; }
        [ForeignKey("IdDepartamento")]
        public Departamento departamento { get; set; }
    }
}