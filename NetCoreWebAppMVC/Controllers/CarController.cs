using Microsoft.AspNetCore.Mvc;
using NetCoreWebAppMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace NetCoreWebAppMVC.Controllers
{
    public class CarController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44335/api/");

        //https://localhost:44335/api/Car

        HttpClient client;

        public CarController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<CarViewModel> modelList = new List<CarViewModel>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress+ "Car").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList =  JsonConvert.DeserializeObject<List<CarViewModel>>(data); 

            }
            return View(modelList);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarViewModel carViewModel)
        {
            string data = JsonConvert.SerializeObject(carViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8,"application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress+ "Car", content).Result;

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            CarViewModel model = new CarViewModel();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "Car"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CarViewModel>(data);

            }
            return View("Create",model);
        }

        [HttpPut]
        public IActionResult Edit(CarViewModel carViewModel)
        {
            string data = JsonConvert.SerializeObject(carViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "Car"+ carViewModel.Id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View("Create", carViewModel);
        }

    }
}
