using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Cloud23Demo.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cloud23Demo
{
    public static class ShoppingCartApi
    {
        static List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();

        [FunctionName("GetShoppingCartItems")]
        public static async Task<IActionResult> GetShoppingCartItems(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "shoppingcartitem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all items");

            return new OkObjectResult(shoppingCartItems);
        }

        [FunctionName("GetShoppingCartItemById")]
        public static async Task<IActionResult> GetShoppingCartItemById(
         [HttpTrigger(AuthorizationLevel.Function, "get", Route = "shoppingcartitem/{id}")] HttpRequest req,
         ILogger log, string id)
        {

            var shoppingCartItem = shoppingCartItems.FirstOrDefault(x => x.Id == id);
            if (shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(shoppingCartItem);


        }


        [FunctionName("CreateShoppingCartItem")]
        public static async Task<IActionResult> GetShoppingCartItem(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "shoppingcartitem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create");
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateShoppingCartItem>(requestData);

            var item = new ShoppingCartItem
            {
                ItemName = data.ItemName
            };

            shoppingCartItems.Add(item);
            return new OkObjectResult(item);

        }


        [FunctionName("PutShoppingCartList")]
        public static async Task<IActionResult> PutShoppingCartItem(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "shoppingcartitem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("updating item");

            var shoppingCartItem = shoppingCartItems.FirstOrDefault(x => x.Id == id);
            if (shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateShoppingCartItem>(requestData);


            shoppingCartItem.Collected = data.Collected;

            return new OkObjectResult(shoppingCartItem);


        }

        [FunctionName("DeleteShoppingCartList")]
        public static async Task<IActionResult> DeleteShoppingCartItem(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "shoppingcartitem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation("delete");
            var shoppingCartItem = shoppingCartItems.FirstOrDefault(x => x.Id == id);
            if (shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            shoppingCartItems.Remove(shoppingCartItem);

            return new OkResult();

        }
    }
}
