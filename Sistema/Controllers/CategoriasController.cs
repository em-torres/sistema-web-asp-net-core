﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;
using Sistema.UserMethods;

namespace Sistema.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly SistemaContext _context;

        public CategoriasController(SistemaContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page
          )
        {
            // Declaracion de Variables
            int pageSize = 10;                   // Cant de Valores a mostrar

            // Condicion Paginacion
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Parámetros a ser recibidos en la vista
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DescSortParam"] = (sortOrder == "descr_asc" ? "descr_desc" : "descr_asc");
            ViewData["NombSortParam"] = (String.IsNullOrEmpty(sortOrder) ? "nom_desc" : "");
            
            // Listado de datos desde la BD del contexto/modelo Categoria
            var categorias = from s in _context.Categoria select s;

            // Filtrado de Datos
            if (!string.IsNullOrEmpty(searchString))
            {
                categorias = categorias.Where(s => s.Nombre.Contains(searchString) ||
                                                   s.Descripcion.Contains(searchString));
            }

            // Condiciones para ordenar
            switch (sortOrder)
            {
                case "nom_desc":
                    categorias = categorias.OrderByDescending(s => s.Nombre);
                    break;
                case "descr_desc":
                    categorias = categorias.OrderByDescending(s => s.Descripcion);
                    break;
                case "descr_asc":
                    categorias = categorias.OrderBy(s => s.Descripcion);
                    break;
                default:
                    categorias = categorias.OrderBy(s => s.Nombre);
                    break;
            }

            // Retorna la lista organizada a la vista
            return View(await Paginacion<Categoria>.CreateAsync(categorias.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaID == id);
        }
    }
}
