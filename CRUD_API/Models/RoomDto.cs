using System;
using System.ComponentModel.DataAnnotations;
namespace CRUD_API.Models
{
    public class RoomDto
    {
        [Required(ErrorMessage = "Tenant name is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Tenant name must be between 5-50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Tenant name cannot contain numbers or special characters")]
        public string TenantName { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Payment method is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid payment method")]
        public int PaymentMethodId { get; set; }
    }
}