using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PbsUI.Models;

namespace PbsUI.Controllers
{
    public class EventsController : Controller
    {

        private readonly string _apiBaseUrl;

        public EventsController(IConfiguration configuration)
        {
            _apiBaseUrl = configuration.GetValue<string>("ApiBaseUrl");
        }

        public async Task<IActionResult> Index()
        {
            var dataModel = new List<EventModel>();
            using (var client = new HttpClient())
            {
                string endpoint = _apiBaseUrl + "/api/events/getall";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readerData = response.Content.ReadAsStringAsync();
                        dataModel = JsonConvert.DeserializeObject<List<EventModel>>(readerData.Result);
                    }
                }
            }

            return View(dataModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
                    string endpoint = _apiBaseUrl + "/api/events/post";
                    using (var response = await client.PostAsync(endpoint, content))
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var dataModel = new EventModel();
            using (var client = new HttpClient())
            {
                string endpoint = _apiBaseUrl + $"/api/events/get/{id}";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readerData = response.Content.ReadAsStringAsync();
                        dataModel = JsonConvert.DeserializeObject<EventModel>(readerData.Result);
                    }
                }
            }

            return View(dataModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    string endpoint = _apiBaseUrl + $"/api/events/put/{id}";
                    using (var response = await client.PutAsync(endpoint, content))
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var dataModel = new EventModel();
            using (var client = new HttpClient())
            {
                string endpoint = _apiBaseUrl + $"/api/events/get/{id}";
                using (var response = await client.GetAsync(endpoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readerData = response.Content.ReadAsStringAsync();
                        dataModel = JsonConvert.DeserializeObject<EventModel>(readerData.Result);
                    }
                }
            }
            return View(dataModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                string endpoint = _apiBaseUrl + $"/api/events/delete/{id}";
                using (var response = await client.DeleteAsync(endpoint))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
