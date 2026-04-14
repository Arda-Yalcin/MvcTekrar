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
    public class BolumController : Controller
    {
        private readonly AppDbContext _context;
  
        public BolumController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var bolumler = _context.Bolumler.ToList();
            return View (bolumler);
        }

        public IActionResult Ekle()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Ekle(Bolum bolum)
        {
            //Model kurallarına uyuyorsa veri tabanına ekle
            if(ModelState.IsValid)
            {
                _context.Bolumler.Add(bolum);
                _context.SaveChanges();//Değişiklikleri Kaydet
                return RedirectToAction("Index");//Kayıt sonrası liste sayfasına yönlendir
            }
            //eğer bir hata varsa formu hatalarla birlikte tekrar göster
            ViewBag.BolumListesi=new SelectList(_context.Bolumler,"Id","Ad");
            return View(bolum);
        }
        [HttpGet]
        public IActionResult Sil(int? id)
        {

            if(id==null) return NotFound();
            //Silinecek ürünü ve kategorisini buluyoruz
            var bolum = _context.Bolumler.FirstOrDefault(m => m.Id==id);

            if(bolum==null) return NotFound();
            return View(bolum);
        }

        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            bool bagliOgrenciVarmi= _context.Ogrenciler.Any(u => u.BolumId == id);
            if (bagliOgrenciVarmi)
            {
                TempData["HataMesaji"]="Bu Kategori Silinemez! Çünkü Bu Kategorinin Içerisinde Hala Ürün Bulunmakta";
                return RedirectToAction(nameof(Sil), new{id=id});
            }
            var bolum = _context.Bolumler.Find(id);

            if(bolum != null)
            {
                _context.Bolumler.Remove(bolum);
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); //nameof ile aynı anlama geliyor
        }
    }
}