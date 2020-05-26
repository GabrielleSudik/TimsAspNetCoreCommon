using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

//A model is just a class whose purpose is to transport data.
//We're just making properties that match the table's columns.