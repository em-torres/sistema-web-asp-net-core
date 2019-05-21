using DispStr = Sistema.UserStrings.DisplayStrings;
using ErStr = Sistema.UserStrings.ErrorStrings;
using Sistema.UserStrings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }

        [Required(ErrorMessage = ErStr.ModelReqError)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ErStr.CatNombreError)]
        public string Nombre { get; set; }

        [StringLength(256, ErrorMessage = ErStr.CatDescripcionError)]
        [Display(Name = DispStr.GenDescDisplay)]
        public string Descripcion { get; set; }

        public Boolean Estado { get; set; }
    }
}
