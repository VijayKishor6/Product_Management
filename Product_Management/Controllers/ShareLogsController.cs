using Product_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using FluentValidation;

namespace Product_Management.Controllers
{
    public class ShareLogsController : ApiController
    {
        Users_context context = new Users_context();

        [HttpPost]
        public HttpResponseMessage Post(ProductWatchListShareLogs ProductWatchListShareLogs)
        {
            var validator = new InlineValidator<ProductWatchListShareLogs>();

            validator.RuleFor(x => x.Email).NotEmpty().EmailAddress();
           // validator.RuleFor(x => x.Status).IsInEnum(); 

            var validationResult = validator.Validate(ProductWatchListShareLogs);

            if (!validationResult.IsValid)
            {
                // If the validation fails, return a BadRequest response with the validation errors.
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }

            // Validation passed, proceed with saving to the database.
            context.ProductWatchListShareLogs.Add(ProductWatchListShareLogs);
            context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, ProductWatchListShareLogs.ProductWatchListShareLogId);
        }


        [HttpGet]
        [Route("api/productwatchlistsahrelogs/productwatchlistsahrelogs")]
        public IHttpActionResult GetProductWatchListShareLogs()
        {
            var prosharelogs = context.ProductWatchListShareLogs.ToList();
            return Ok(prosharelogs);
        }


    }
}