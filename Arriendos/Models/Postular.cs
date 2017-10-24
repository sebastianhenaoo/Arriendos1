using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arriendos.Models
{
    public class Postular
    {
        [Key]
        public int Id { get; set; }
        public int IdPropiedad { get; set; }

        [ForeignKey("IdPropiedad")]
        public Propiedad propiedad { get; set; }
        public ApplicationUser usuario { get; set; }


    }
}