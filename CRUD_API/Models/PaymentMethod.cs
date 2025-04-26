using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CRUD_API.Models
{
    [Table("paymentmethods")]
    public class PaymentMethod
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("method_name")]
        public string MethodName { get; set; } = "";

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}