using HACKATHON.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace HACKATHON.Controllers
{
	public class HomeController : Controller
	{
		private readonly Context DB = new();
		[AllowAnonymous]
		public IActionResult Index()
		{
			var Datas = DB.VeriGirisleri
						.Include(x => x.DegerTuru)       
						.ThenInclude(x => x.Grup)          
						.ThenInclude(x => x.Kategori)
						.Where(x=>x.DegerTuru.Status==true)
						/*.Where(x => x.DegerTuru != null && x.DegerTuru.Grup != null && x.DegerTuru.Grup.Kategori != null)*/ // NULL kontrolü
						.ToList();

			var Others = DB.VeriGirisleri
						.Include(x => x.DegerTuru)
						.ThenInclude(x => x.Grup)
						.ThenInclude(x => x.Kategori)
						.Where(x => x.DegerTuru.Status == false)
						.ToList();
			ViewBag.Others = Others;
			return View(Datas);
		}

		[HttpGet]
		public IActionResult KategoriEkle()
		{
			return View();
		}
		[HttpPost]
		public IActionResult KategoriEkle(Kategori kategori)
		{
			DB.Kategoriler.Add(kategori);
			DB.SaveChanges();
			return RedirectToAction("GrupEkle");
		}

		[HttpGet]
		public IActionResult GrupEkle()
		{
			List<SelectListItem> Kategoriler = (from x in DB.Kategoriler.ToList()
												select new SelectListItem
												{
													Text = x.KategoriAdi,
													Value = x.KategoriId.ToString()
												}
												).ToList();
			ViewBag.Kategoriler = Kategoriler;
			return View();
		}

		[HttpPost]
		public IActionResult GrupEkle(Grup grup)
		{
			DB.Gruplar.Add(grup);
			DB.SaveChanges();
			return RedirectToAction("DegerTuruEkle");
		}


		[HttpGet]
		public IActionResult DegerTuruEkle()
		{
			List<SelectListItem> Gruplar = (from x in DB.Gruplar.Include(k => k.Kategori).ToList()
											select new SelectListItem
											{
												Text = x.GrupAdi + " " + "(" + x.Kategori.KategoriAdi + ")",
												Value = x.GrupId.ToString()
											}).ToList();
			ViewBag.Gruplar = Gruplar;
			return View();
		}

		[HttpPost]
		public IActionResult DegerTuruEkle(VeriGirisi veri)
		{
			veri.Donem = DonemHesapla(veri.DegerTuru.VeriPeriyodu);
			veri.Yil = YilHesapla(veri.DegerTuru.VeriPeriyodu);
			veri.DegerTuru.Status = true;
			DB.VeriGirisleri.Add(veri);
			DB.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult DegerDuzenle(int id)
		{
			
			var DegerTuru = DB.VeriGirisleri.Include(x => x.DegerTuru)
				.ThenInclude(g => g.Grup).
				ThenInclude(x => x.Kategori).
				Where(x => x.DegerTuruId == id).FirstOrDefault();

			List<SelectListItem> GrupListesi = (from x in DB.Gruplar.Include(x => x.Kategori).ToList()
												select new SelectListItem
												{
													Text = x.GrupAdi + "(" + x.Kategori.KategoriAdi + ")",
													Value = x.GrupId.ToString()
												}).ToList();

			List<SelectListItem> KategoriListesi = (from x in DB.Kategoriler.ToList()
													select new SelectListItem
													{
														Text = x.KategoriAdi,
														Value = x.KategoriId.ToString()
													}).ToList();


			

			ViewBag.KategoriListesi = KategoriListesi;
			ViewBag.GrupListesi = GrupListesi;
			return View(DegerTuru);
		}

		[HttpPost]
		public IActionResult DegerDuzenle(VeriGirisi veri)
		{
			veri.Donem = DonemHesapla(veri.DegerTuru.VeriPeriyodu);
			veri.Yil = YilHesapla(veri.DegerTuru.VeriPeriyodu);
			veri.DegerTuru.Status = true;
			//var data = DB.VeriGirisleri.Include(x => x.DegerTuru).Where(x => x.DegerTuruId == veri.DegerTuruId).FirstOrDefault();
			var data = DB.VeriGirisleri
					   .Include(x=>x.DegerTuru)
					   .Where(x=>x.DegerTuruId==veri.DegerTuru.DegerTuruId)
					   .FirstOrDefault();
			DB.SaveChanges();
			return RedirectToAction("Index");
		}


		public IActionResult StateDegistir(int id)
		{
			
			var Deger = DB.VeriGirisleri
				.Include(x => x.DegerTuru)  
				.FirstOrDefault(x => x.DegerTuruId == id);

			if (Deger != null && Deger.DegerTuru != null)  
			{
				
				Deger.DegerTuru.Status = !Deger.DegerTuru.Status;

				DB.SaveChanges();

				return RedirectToAction("Index");
			}

			
			return NotFound("İlgili Değer Türü bulunamadı.");
		}

		[HttpGet]
		public IActionResult SorumluAta(int id)
		{

			var AtanacakDeger = DB.DegerTurleri
								.Include(x => x.Grup)
								.ThenInclude(x => x.Kategori)
								.Where(x => x.DegerTuruId == id)
								.FirstOrDefault();
			var SorumluListesi = (from x in DB.Sorumlular.ToList()
								  select new SelectListItem
								  {
									  Text = x.AdSoyad + " " + "(" + x.Email + ")",
									  Value = x.SorumluId.ToString()
								  }).ToList();

			ViewBag.AtanacaKDeger = AtanacakDeger;
			ViewBag.SorumluListesi = SorumluListesi;
			return View();
		}


		[HttpPost]
	
		public IActionResult SorumluAta(SorumluAtama sorumluAtama)
		{
			
			DB.SorumluAtamalari.Add(sorumluAtama);
			DB.SaveChanges();  
			var ChangeState = DB.DegerTurleri
				.FirstOrDefault(x => x.DegerTuruId == sorumluAtama.DegerTuruId);

			
			if (ChangeState != null)
			{
				ChangeState.Status = false;
				DB.SaveChanges();
			}

			
			return RedirectToAction("Index");
		}







		// Metotlar
		public string DonemHesapla(int veriPeriyoduAy)
		{
			DateTime bugun = DateTime.Now;
			DateTime baslangicTarihi = bugun.AddMonths(-veriPeriyoduAy);

			
			return $"{AyIsmiGetir(baslangicTarihi.Month)} {baslangicTarihi.Year} - {AyIsmiGetir(bugun.Month)} {bugun.Year}";
		}

		private string AyIsmiGetir(int ay)
		{
			string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
			return aylar[ay - 1];
		}

		public string YilHesapla(int veriPeriyoduAy)
		{
			DateTime VeriGirişYili = DateTime.Now;
			DateTime VeriBaslangicYili = VeriGirişYili.AddMonths(-veriPeriyoduAy);
			return VeriBaslangicYili.Year.ToString();
		}
	}
}
