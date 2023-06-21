using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Models; // Zamień na właściwą przestrzeń nazw, w której znajduje się twoja klasa Ocena
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Controllers // Zamień na właściwą przestrzeń nazw
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
        }

        [HttpPost]
        public async Task<IActionResult> DodajOcene(Ocena ocenaViewModel)
        {
            var ocena = new Ocena
            {
                KursId = ocenaViewModel.KursId,
                StudentId = ocenaViewModel.StudentId,
                Data = ocenaViewModel.Data,
                Wartosc = ocenaViewModel.Wartosc
            };

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(ocena), Encoding.UTF8, "application/json");
                await httpClient.PostAsync("http://localhost:5000/Oceny", content);
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("/oceny");

            if (response.IsSuccessStatusCode)
            {
                var oceny = await response.Content.ReadAsAsync<List<Ocena>>();
                return View(oceny);
            }

            return NotFound();
        }
    }
}
