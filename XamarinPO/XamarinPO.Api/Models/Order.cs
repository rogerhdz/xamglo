using System;

namespace XamarinPO.Api.Models
{
    public class Order
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Guid ClientId { get; set; }
        public string DeliveryInformation { get; set; }
        public int Status { get; set; }
    }
}