using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PorLaChamba.Models;
using RestSharp;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Net.WebRequestMethods;


namespace PorLaChamba.Controllers
{
    [ApiController]
    [Route("api/xcaret")]
    public class XcaretController: ControllerBase
    {




        private readonly IConfiguration _configuration;

        public XcaretController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet("filtrado")]
        public async Task<ActionResult<List<Entry>>> Get(bool https)
        {

            string Uri = _configuration.GetValue<string>("Parametro:Uri");

            var client = new RestClient(Uri);

            RestRequest request = Peticion.crearPeticion(Uri, Method.Get);
            var response = client.Execute(request);

            CatFacts DataGeneral = JsonConvert.DeserializeObject<CatFacts>(response.Content);

            //El primero debe permitir consultar los elementos que tengan el nodo “HTTPS” en true o false.
            List<Entry> Filter = DataGeneral.entries.Where(X => X.HTTPS == https).ToList();

            return await Task.FromResult(Filter);
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<string>>> categoriasList()
        {

            string Uri = _configuration.GetValue<string>("Parametro:Uri");

            var client = new RestClient(Uri);

            RestRequest request = Peticion.crearPeticion(Uri, Method.Get);
            var response = client.Execute(request);

            CatFacts DataGeneral = JsonConvert.DeserializeObject<CatFacts>(response.Content);

            //El primero debe permitir consultar los elementos que tengan el nodo “HTTPS” en true o false.
            var objCategorias = DataGeneral.entries.GroupBy(X => X.Category).ToList();

            List<string> Categ = new List<string>();
            foreach (var entry in objCategorias) {
                Categ.Add(entry.Key);
            }

            return await Task.FromResult(Categ);
        }



    }
}
