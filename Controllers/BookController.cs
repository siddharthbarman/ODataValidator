using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.Logging;
using ODataSample.Attributes;
using ODataSample.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Query;

namespace ODataSample.Controllers {

	[ODataRoutePrefix("Books")]
	public class BookController : ODataController {		
		public BookController(ILogger<BookController> logger) {
			_logger = logger;
			InitializeData();
		}				

		[ODataRoute]		
		[RestrictedQueryEnableAttribute(
			DisallowedFilterProperties = "Title,Price", 
			AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select,
			AllowedOrderByProperties = "Id, PublishYear"			
		)]
		public IQueryable<Book> Get() {
			return books.AsQueryable();
		}

		private List<Book> books = new List<Book>();

		private void InitializeData() {
			_logger.LogInformation("Loading data");
			int bookId = 1;
			books.Add(
				new Book {
					Id = bookId++,
					Author = "Stephen King",
					Price = 25,
					Title = "IT",
					PublishYear = 1986
				});

			books.Add(
				new Book {
					Id = bookId++,
					Author = "Isaac Asimov",
					Price = 20,
					Title = "I, Robot",
					PublishYear = 1950 
				});

			books.Add(
				new Book {
					Id = bookId++,
					Author = "Isaac Asimov",
					Price = 20,
					Title = "Robot Dreams",
					PublishYear = 1986
				});

			books.Add(
				new Book {
					Id = bookId++,
					Author = "Yuval Harai",
					Price = 20,
					Title = "Homo Deus",
					PublishYear = 2016
				});

			books.Add(
				new Book {
					Id = bookId++,
					Author = "Linus Torvals",
					Price = 20,
					Title = "Just for fun",
					PublishYear = 2002
				});
		}

		private readonly ILogger<BookController> _logger;
	}
}
