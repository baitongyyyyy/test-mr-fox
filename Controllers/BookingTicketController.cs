using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Context;
using Project.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTicketController : ControllerBase
    {
        private readonly ApplicationDBContext dbContext = new ApplicationDBContext();

        public BookingTicketController(ApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpPut]
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> BookTicketAsync([FromBody] Model requestModel)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync()) 
            {
                try
                {
                    var currentDate = DateTime.Now;
                    var bookTicketToday = dbContext.ConcertTicket
                        .Where(x => x.ReservedBy.Contains(requestModel.ReservedName) &&
                        (x.ReservedDate.Value.Year == currentDate.Year &&
                        x.ReservedDate.Value.Month == currentDate.Month &&
                        x.ReservedDate.Value.Day == currentDate.Day &&
                        x.ReservedDate.Value.Hour == currentDate.Hour &&
                        x.ReservedDate.Value.Minute == currentDate.Minute))
                        .FirstOrDefault();


                    if (bookTicketToday != null)
                    {
                        return Ok("Unavaliable");
                    }

                    var concert = dbContext.Concert
                        .Where(x => x.Title.Contains(requestModel.ConcertName)).FirstOrDefault();

                    if(concert == null) return BadRequest($"Concert {requestModel.ConcertName} not found");

                    var bookTicket = dbContext.ConcertTicket
                        .Where(x => x.ConcertId == concert.Id && x.StatusId == 0).FirstOrDefault();

                    if (bookTicket == null)
                    {
                        return Ok("Tickets are fully booked.");
                    }

                    bookTicket.StatusId = 1;
                    bookTicket.ReservedBy = requestModel.ReservedName;
                    bookTicket.ReservedDate = DateTime.Now;



                    dbContext.SaveChanges();
                    transaction.Commit();

                    return Ok($"Ticket Id: { bookTicket.Id} is booked.");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
    }
}
