using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcTekrar.Data;
using MvcTekrar.Models;

namespace MvcTekrar.Controllers
{
    public class OgrenciController : Controller
    {
        
        private readonly AppDbContext _context;
  
        public OgrenciController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        { 
            // Ürünleri çekerken bağlı olduğu Kategoriyi de dahil ediyoruz(Include)
            var ogrenciler = _context.Ogrenciler.Include(u => u.Bolum).ToList();
            return View (ogrenciler);
        }

  
    }
}