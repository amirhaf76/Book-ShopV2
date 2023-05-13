﻿namespace BookShop.Models.DataBaseLayer.DataBaseModels
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; }


    }
}
