using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODataSample.Models;

namespace ODataSample.Repositories {
    public class BookStoreDB : DbContext {
        public BookStoreDB(DbContextOptions options, ILogger<BookStoreDB> logger): base(options) {
			_logger = logger;
			_logger.LogInformation("BookStoreDB ctor");
        }

        public DbSet<Book> Books { get; set; }
        
        public void SeedForTest() {
			_logger.LogInformation("Seeding test data");
            int bookId = 1;
			Books.Add(
				new Book {
					Id = bookId++,
					Author = "Stephen King",
					Price = 25,
					Title = "IT",
					PublishYear = 1986
				});

			Books.Add(
				new Book {
					Id = bookId++,
					Author = "Isaac Asimov",
					Price = 20,
					Title = "I, Robot",
					PublishYear = 1950 
				});

			Books.Add(
				new Book {
					Id = bookId++,
					Author = "Isaac Asimov",
					Price = 20,
					Title = "Robot Dreams",
					PublishYear = 1986
				});

			Books.Add(
				new Book {
					Id = bookId++,
					Author = "Yuval Harai",
					Price = 20,
					Title = "Homo Deus",
					PublishYear = 2016
				});

			Books.Add(
				new Book {
					Id = bookId++,
					Author = "Linus Torvalds",
					Price = 20,
					Title = "Just for fun",
					PublishYear = 2002
				});
            this.SaveChanges();
        }

		private readonly ILogger<BookStoreDB> _logger;        
    }
}