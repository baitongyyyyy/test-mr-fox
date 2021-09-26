using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Context;
using Project.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomGenerateController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext = new ApplicationDBContext();

        public RandomGenerateController(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public IActionResult GenerateWord(int length)
        {
            try
            {
                List<WordModel> listData = new List<WordModel>() 
                { 
                    new WordModel
                    {
                        Word = "nation" 
                    },
                    new WordModel
                    {
                        Word = "historian"
                    },
                    new WordModel
                    {
                        Word = "chocolate"
                    },
                    new WordModel
                    {
                        Word = "negotiation"
                    },
                    new WordModel
                    {
                        Word = "recommendation"
                    }
                };

                List<WordModel> myList = new List<WordModel>();

                // add items to the list
                Random r = new Random();
                if (length == 0) { length = 10000; }

                for (int i = 0; i < length; i++)
                {
                    int index = r.Next(listData.Count);
                    var randomData = listData[index];
                    randomData.Order = index;
                    myList.Add(randomData);
                }

                myList = myList.ToList().OrderByDescending(x => x.Order).ToList();

                var groupByWord = myList.GroupBy(x => x.Word).Select(x => new {
                    prefix = x.Key,
                    count = x.ToList().Count()
                }).ToList();

                var groupSameNumber = groupByWord.GroupBy(x => x.count).Select(x => new {
                    prefix = string.Format("{0} : {1}", x.ToList().Count() > 1 
                        ? string.Join(' ', x.Select(o => o.prefix)) 
                        : x.FirstOrDefault().prefix, x.Key),
                    count = x.Key
                }).ToList();

                return Ok(groupSameNumber.OrderByDescending(x => x.count).Select(x => x.prefix));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }

}