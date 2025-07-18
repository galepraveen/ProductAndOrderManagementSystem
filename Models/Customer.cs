﻿namespace InventoryAndOrderManagementAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
