using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UdemyClass.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime Dateadded { get; set; }

        public int NumberInStock { get; set; }

    }
}