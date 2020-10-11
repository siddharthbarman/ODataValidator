using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.Logging;
using ODataSample.Attributes;
using ODataSample.Models;
using ODataSample.Validators;
using System.Collections.Generic;
using System.Linq;

namespace ODataSample.Controllers {

	[ODataRoutePrefix("Books")]
	public class BookController : ODataController {		
		public BookController(ILogger<BookController> logger) {
			_logger = logger;
			InitializeData();
		}
				

		[ODataRoute]
		[RestrictedQueryEnableAttribute(DisallowedFilterProperties = "Title,Price", AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.Select)]
		public IQueryable<Book> Get() {
			return books.AsQueryable();
		}

		private List<Book> books = new List<Book>();

		private void InitializeData() {
			_logger.LogInformation("Loading data");
			books.Add(
				new Book {
					Id = 1,
					Author = "Stephen King",
					Price = 25,
					Title = "IT"
				});

			books.Add(
				new Book {
					Id = 2,
					Author = "Isaac Asimov",
					Price = 20,
					Title = "I, Robot"
				});
		}

		private readonly ILogger<BookController> _logger;

	}
}
