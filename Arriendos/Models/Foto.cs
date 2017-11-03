using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arriendos.Models
{
    public class Foto
    {
        [Key]
        public int Id { get; set; }
        public byte[] Imagen { get; set; }
        public int IdPropiedad { get; set; }

        [ForeignKey("IdPropiedad")]
        public virtual Propiedad propiedad { get; set; }
    }
}