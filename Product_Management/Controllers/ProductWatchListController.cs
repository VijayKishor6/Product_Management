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
    public class ProductWatchListController : ApiController
    {
        Users_context context = new Users_context();

        [HttpGet]
        [Route("api/productwatchlist/productwatchlists")]
        public IHttpActionResult GetProductWatchLists()
        {
            var productWatchLists = context.ProductWatchLists.ToList();
            return Ok(productWatchLists);
        }


        [HttpPost]
        public HttpResponseMessage Post(ProductWatchLists ProductWatchLists)
        {
            var validator = new InlineValidator<ProductWatchLists>();

            validator.RuleFor(x => x.ListName).NotEmpty().MaximumLength(255);
            validator.RuleFor(x => x.Status).IsInEnum(); // Assuming Status is an enum.
            // Add more rules for other properties as needed...

            var validationResult = validator.Validate(ProductWatchLists);

            if (!validationResult.IsValid)
            {
                // If the validation fails, return a BadRequest response with the validation errors.
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }

            // Validation passed, proceed with saving to the database.
            context.ProductWatchLists.Add(ProductWatchLists);
            context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, ProductWatchLists.ProductWatchListId);
        }

        public IHttpActionResult Delete(int id)
        {
            ProductWatchLists Prowatch = context.ProductWatchLists.Find(id);
            context.ProductWatchLists.Remove(Prowatch);
            context.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}