using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CRUD_API.Models
{
    public class Room
    {
        [Key]
        [Column("room_id")]
        public int RoomId { get; set; }

        [Required]
        [Column("tenant_name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Tenant name must be between 5-50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Tenant name cannot contain numbers or special characters")]
        public string TenantName { get; set; } = "";

        [Required]
        [Column("phone_number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; } = "";

        [Required]
        [Column("start_date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Column("payment_method_id")]
        public int? PaymentMethodId { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethod? PaymentMethod { get; set; }
    }
}