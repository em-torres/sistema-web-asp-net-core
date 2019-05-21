using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.UserStrings
{
    public class ErrorStrings
    {
        // Errores Generales
        public const string ModelReqError = "Campo Requerido.";
        public const string Excess50Error = "Debe de tener de 3 a 50 caracteres.";
        public const string Excess256Error = "No debe exceder los 256 caracteres.";

        // Errores en Modelo Categoria
        public const string CatNombreError = "El nombre debe de tener de 3 a 50 caracteres.";
        public const string CatDescripcionError = "La descripción no debe exceder los 256 caracteres.";
    }
}
