using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdieBook.Models
{
    public class TagMap
    {
        [Key, Column(Order =0)] //Key = TagID+Thing
        [ForeignKey("Tag")]
        public string TagId { get; set; }
        [Key, Column(Order =1)]
        public string Thing { get; set; } //Refers to the foreign key in the tagged table
    }
}