using Sistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Data
{
    public class DbInitializer
    {
        public static void Initialize(SistemaContext context)
        {
            context.Database.EnsureCreated();

            // Buscar si existen registros en la tabla categoria
            if (context.Categoria.Any())
            {
                return;
            }

            // Crear datos a ser agregados en la tabla Categoria
            var categorias = new Categoria[]
            {
                new Categoria
                {
                    Nombre = "Programación",
                    Descripcion = "Cursos de Programación",
                    Estado = true
                },
                new Categoria
                {
                    Nombre = "Diseño Gráfico",
                    Descripcion = "Cursos de Diseño Gráfico",
                    Estado = true
                }
            };

            // Agregar los datos de las categorias
            foreach (Categoria c in categorias)
            {
                context.Categoria.Add(c);
            }

            // Guardar los datos en la Base de Datos
            context.SaveChanges();
        }
    }
}
