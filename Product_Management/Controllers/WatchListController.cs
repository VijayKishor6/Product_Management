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
    public class WatchListController : ApiController
    {
        Users_context context = new Users_context();


        [HttpPost]
        public HttpResponseMessage Post(WatchLists WatchLists)
        {

            var validator = new InlineValidator<WatchLists>();

            validator.RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            validator.RuleFor(x => x.CreatedBy).NotEmpty().MaximumLength(255);
            validator.RuleFor(x => x.LastModifiedBy).NotEmpty().MaximumLength(255);
            validator.RuleFor(x => x.Type).NotEmpty().MaximumLength(255);
            // Add more rules for other properties as needed...

            var validationResult = validator.Validate(WatchLists);

            if (!validationResult.IsValid)
            {
                // If the validation fails, return a BadRequest response with the validation errors.
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }

            // Validation passed, proceed with saving to the database.
            context.WatchLists.Add(WatchLists);
            context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, WatchLists.Id);
        }

        [HttpGet]
        [Route("api/watchlist/watchlists")]
        public IHttpActionResult GetWatchLists()
        {
            var watchLists = context.WatchLists.ToList().Select(s => new WatchLists
            {
                Id = s.Id,
                Name = s.Name,
                UserId = s.UserId,
                LastModified = s.LastModified
            }).AsEnumerable();
            return Ok(watchLists);
        }

        public IHttpActionResult Delete(int id)
        {
            WatchLists watch = context.WatchLists.Find(id);
            context.WatchLists.Remove(watch); 
            context.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }
}