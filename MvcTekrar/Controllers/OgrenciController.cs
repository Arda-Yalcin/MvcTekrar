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

        public IActionResult Ekle()
        {
            //   Veritabanındaki kategorileri çekip dropdown için viewbag içine atıyoruz
            //   "Id" arka planda kaydedilecek değer, "Ad" ise kullanıcıya gösterilecek metindir.

            ViewBag.BolumListesi=new SelectList(_context.Bolumler,"Id","Ad");
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Ogrenci ogrenci)
        {
            //Model kurallarına uyuyorsa veri tabanına ekle
            if(ModelState.IsValid)
            {
                _context.Ogrenciler.Add(ogrenci);
                _context.SaveChanges();//Değişiklikleri Kaydet
                return RedirectToAction("Index");//Kayıt sonrası liste sayfasına yönlendir
            }
            //eğer bir hata varsa formu hatalarla birlikte tekrar göster
            ViewBag.BolumListesi=new SelectList(_context.Bolumler,"Id","Ad");
            return View(ogrenci);
        }

        public IActionResult Sil(int? id)
        {

            if(id==null) return NotFound();
            //Silinecek ürünü ve kategorisini buluyoruz
            var ogrenci = _context.Ogrenciler.Include(u => u.Bolum).FirstOrDefault(m => m.Id==id);

            if(ogrenci==null) return NotFound();
            return View(ogrenci);
        }



        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            var ogrenci = _context.Ogrenciler.Find(id);
            if(ogrenci != null)
            {
                _context.Ogrenciler.Remove(ogrenci);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}