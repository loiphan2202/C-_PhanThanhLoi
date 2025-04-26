using System.ComponentModel.DataAnnotations;

namespace Clients.Models
{
    public class RoomDto
    {
        [Required(ErrorMessage = "Tenant name is required")]
        [StringLength(50, MinimumLength = 5)]
        public string TenantName { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; } = "";

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int PaymentMethodId { get; set; }
    }
}
