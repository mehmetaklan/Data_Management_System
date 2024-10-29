using HACKATHON.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HACKATHON.Controllers
{
	
	public class LoginController : Controller
    {
		


		private readonly ILogger<LoginController> _logger;

       
		public LoginController(ILogger<LoginController> logger)
		{
			_logger = logger;  
		}


		Context DB = new();

		

		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Sorumlu sorumlu)
        {
            var AdminValue = DB.Sorumlular.FirstOrDefault(x => x.Email == sorumlu.Email && x.Parola== sorumlu.Parola);

            if (AdminValue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sorumlu.Email)
                };
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("SorumluUI",AdminValue);
            }
            ViewBag.ErrorMessage = "Email veya şifrenizi hatalı girdiniz. \n Lütfen kontrol edip tekrar deneyin.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult SorumluUI(Sorumlu sorumlu)
        {
            var SorumluVerileri = DB.VeriGirisleri
    .Include(x => x.DegerTuru)
        .ThenInclude(x => x.SorumluAtamalari)
    .Include(x => x.DegerTuru)
        .ThenInclude(x => x.Grup)
        .ThenInclude(x => x.Kategori)
    .Where(x => x.DegerTuru.SorumluAtamalari
        .Any(sa => sa.SorumluId == sorumlu.SorumluId)).ToList();

            ViewBag.Sorumlu = sorumlu;
			return View(SorumluVerileri);
        }

        [HttpPost]
        public IActionResult SorumluUI(float deger, int id)
        {
            var value = DB.VeriGirisleri.Find(id);
            value.Deger = deger;
            DB.SaveChanges();
            //         if (ModelState.IsValid)
            //         {   
            //	DB.VeriGirisleri.Update(veri);
            //	DB.SaveChanges();
            //	_logger.LogInformation($"Veri kaydedildi: VeriGirisiID: {veri.VeriGirisiId} - Veri Degeri: {veri.Deger}");
            //}
            //if (!ModelState.IsValid)
            //{
            //	_logger.LogWarning("Veri girişinde model geçerli değil.");
            //}
            var DegerTuruID = value.DegerTuruId;

			var CheckSorumluId = DB.Sorumlular
				.Include(x => x.SorumluAtamalari)  // Sorumlu ile ilişkili SorumluAtamalari tablosunu yükler
					.ThenInclude(sa => sa.DegerTuru)  // SorumluAtamalari ile ilişkili DegerTuru tablosunu yükler
				.Where(s => s.SorumluAtamalari.Any(sa => sa.DegerTuru.DegerTuruId == DegerTuruID))  // DegerTuruId ile filtreleme
				.FirstOrDefault();  // İlk eşl

			return RedirectToAction("SorumluUI",CheckSorumluId);
        }
    }
}
