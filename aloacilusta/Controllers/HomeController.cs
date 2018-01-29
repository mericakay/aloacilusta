using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace aloacilusta.Controllers
{

    public class HomeController : Controller
    {

        public class PatOBInfo
        {
            public string id { get; set; }
            public string language { get; set; }
            public string language_eng { get; set; }
            public string language_main_code { get; set; }
            public string url { get; set; }
            
        }
        public class katInfo
        {
            public string id { get; set; }
            public string name { get; set; }
           

        }

        public ActionResult Index()
        {
            PatOBInfo jobject = new PatOBInfo();
            jobject.id = "1";
            jobject.language = "1";
            jobject.language_eng = "1";
            jobject.language_main_code = "";
            jobject.url = "1";
            katInfo katobje = new katInfo();
            katobje.id = "";
            katobje.name = "";
            ViewData["katlist"] = aloacilusta(katobje);
            ViewData["list"] = aloacilusta(jobject);
            return View();
        }
        public List<PatOBInfo> aloacilusta(PatOBInfo product)
        {
            List<PatOBInfo> list = new List<PatOBInfo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://185.86.4.73");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(product), System.Text.Encoding.UTF8, "application/json");
                // HTTP POST
                HttpResponseMessage response = client.GetAsync("ustalar/Slim_Proxy_usta/SlimProxyBoot.php?url=fillComboBox_syslanguage").Result;
                string data1 = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<PatOBInfo>>(data);
                }
            }
            ViewBag.Message = list;
            return list;
        }

        public List<katInfo> aloacilusta(katInfo product)
        {
            List<katInfo> katlist = new List<katInfo>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://185.86.4.73");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                StringContent content = new StringContent(JsonConvert.SerializeObject(product), System.Text.Encoding.UTF8, "application/json");
                // HTTP POST
                HttpResponseMessage response = client.GetAsync("ustalar/Slim_Proxy_usta/SlimProxyBoot.php?url=FillMainCities_infocentercities&lid=647").Result;
                string data1 = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    katlist = JsonConvert.DeserializeObject<List<katInfo>>(data);
                }
            }
            ViewBag.Message = katlist;
            return katlist;

        }      

        public ActionResult About()
        {
            ViewBag.Message = "Hakkımızda.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "İletişim.";

            return View();
        }
    }
}