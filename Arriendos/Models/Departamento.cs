using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arriendos.Models
{
    public class Departamento
    {
        [Key]
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }

        public List<Ciudad> ciudades { get; set; }
    }
}