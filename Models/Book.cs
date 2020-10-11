using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataSample.Models {
	public class Book {
		public int Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public float Price { get; set; }
	}
}
