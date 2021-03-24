using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;
using TaskScheduler;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpPost]
        [Route("api/AddItemFila")]
        public IHttpActionResult AddItemFila(CoinModel coinModel)
        {

            if (ModelState.IsValid)
            {
                var cache = MemoryCache.Default;

                var result = (List<CoinModel>)cache.Get("CoinsAdded");

                if (result == null)
                {
                    List<CoinModel> ListCoins = new List<CoinModel>();
                    ListCoins.Add(coinModel);
                    //criando politica cache
                    var policy = new CacheItemPolicy().AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                    cache.Set("CoinsAdded", ListCoins, policy);
                }
                else
                {
                    result.Add(coinModel);
                    var policy = new CacheItemPolicy().AbsoluteExpiration = DateTime.Now.AddMinutes(30);
                    cache.Set("CoinsAdded", result, policy);
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Item criado com sucesso !!"));
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState.Select(x => x.Value.Errors.Select(l=>l.ErrorMessage).FirstOrDefault()).FirstOrDefault().ToString()));
            }
        }

        [HttpPost]
        [Route("api/DeleteTask")]
        public IHttpActionResult DeleteTask()
        {
            Routine routine = new Routine();

            routine.DeletaTarefa();
            System.Diagnostics.Debug.WriteLine("Tarefa deletada !");


            return Ok("Tarefa deletada !");
        }

        [HttpPost]
        [Route("api/CreateTask")]
        public IHttpActionResult CreateTask()
        {
            Routine routine = new Routine();

            routine.CriarTarefa();
            System.Diagnostics.Debug.WriteLine("Tarefa criada !");


            return Ok("Tarefa criada !");
        }

    }
}
