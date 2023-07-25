using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearningApp.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        // public int CategoryId { get; set; } ==>Id primary Key

        //[Required] => Required in DB!
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        //Valdition [Range(0,100),ErrorMessgage=] Custom ErrorMessgage=  [Range(0,100)]

        [DisplayName("Display Order")]
        [Range(0, 100)]
        public int DisplayOrder { get; set; }
    }
}
