using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Script.Serialization;
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
            try
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
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState.Select(x => x.Value.Errors.Select(l => l.ErrorMessage).FirstOrDefault()).FirstOrDefault().ToString()));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }
        [HttpGet]
        [Route("api/GetItemFila")]
        public IHttpActionResult GetItemFila()
        {
            try
            {

                var cache = MemoryCache.Default;
                var serializedData = "";
                var result = (List<CoinModel>)cache.Get("CoinsAdded");


                if (result == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe item adicionados a lista !!"));
                }
                else
                {
                    CoinModel coin = result[result.Count - 1];
                    if (result.Count() == 0)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Não existe item adicionados a lista !!"));
                    }
                    else
                    {
                        CoinModel coinModel = new CoinModel();
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();


                        serializedData = JsonConvert.SerializeObject(coin,
                 new JsonSerializerSettings
                 {
                     DateFormatHandling = DateFormatHandling.IsoDateFormat,
                     Formatting = Formatting.Indented,
                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                 });

                        var deserializedResult = JsonConvert.DeserializeObject<CoinModel>(serializedData);


                        if (coinModel.RemoveItem())
                        {
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, serializedData));
                        }
                        else
                        {
                            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Erro ao remover item da lista !!"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("api/DeleteTask")]
        public IHttpActionResult DeleteTask()
        {
            Routine routine = new Routine();

            routine.DeletaTarefa();

            return Ok("Tarefa deletada !");
        }

        [HttpPost]
        [Route("api/CreateTask")]
        public IHttpActionResult CreateTask()
        {
            Routine routine = new Routine();

            routine.CriarTarefa();

            return Ok("Tarefa criada !");
        }

    }
}
