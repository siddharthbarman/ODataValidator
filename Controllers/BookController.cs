using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.Logging;
using ODataSample.Attributes;
using ODataSample.Models;
using System.Linq;
using Microsoft.AspNet.OData.Query;
using ODataSample.Repositories;

namespace ODataSample.Controllers {

	[ODataRoutePrefix("Books")]
	public class BookController : ODataController {		
		public BookController(ILogger<BookController> logger, BookStoreDB repo) {
			_logger = logger;			
			_repo = repo;
			_logger.LogInformation("BookController ctor");
		}				

		[ODataRoute]		
		[RestrictedQueryEnableAttribute(
			DisallowedFilterProperties = "Title,Price", 
			AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Select,
			AllowedOrderByProperties = "Id, PublishYear"			
		)]
		public IQueryable<Book> Get() {
			_logger.LogInformation("Get called");
			return _repo.Books;
		}

		private readonly ILogger<BookController> _logger;
		private BookStoreDB _repo;
	}
}
