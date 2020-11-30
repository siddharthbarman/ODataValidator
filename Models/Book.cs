using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODataSample.Models
{
    [Table("Book")]	
	public class Book {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Price { get; set; }
		public int PublishYear { get;set; }
	}
}
