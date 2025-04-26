namespace Clients.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string TenantName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime StartDate { get; set; }
        public int? PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
    }
}
