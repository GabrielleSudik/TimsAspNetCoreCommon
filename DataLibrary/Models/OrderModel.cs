using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Max name 20 characters.")]
        [MinLength(3, ErrorMessage = "You need at least 3 characters.")]
        [DisplayName("Customer's name")]
        public string OrderName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow; //sets a default, which we'll always use.

        [DisplayName("Meal")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a meal from the list.")]
        public int FoodId { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "You can purchase up to 10 meals.")] 
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        //FYI: shift-enter will "Enter" key from the middle of a line
        //ctrl-enter will create an empty row above your loc.

        //On decorating models for validation purposes:
        //Tim usually does validation elsewhere, specifically the UI
        //because that's part of the UI's job.
        //The job of the model is just to transport data.
        //Also, you might want different UIs to validate in different ways.
        //And it makes the model harder to read.
        //But we'll do validation here with decorating to save time.
    }
}
