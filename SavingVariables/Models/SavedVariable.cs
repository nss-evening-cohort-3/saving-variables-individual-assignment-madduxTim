using System.ComponentModel.DataAnnotations; // <---- The System.ComponentModel.DataAnnotations namespace provides attribute classes that are used to define metadata for ASP.NET MVC and ASP.NET data controls.

namespace SavingVariables.Models
{
    public class SavedVariable
    {
        [Key]
        public int ID { get; set; }

        [Required]
        //[Display(something = "somethingElse")] <--- what's this again? 
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
