using Microsoft.EntityFrameworkCore;
using shopping_website.Models;
using System.Linq;

namespace shopping_website.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if we already have data
            if (context.Products.Any())
            {
                return; // DB has been seeded
            }

            // Seed Products
            var products = new Product[]
            {
                new Product
                {
                    Name = "Wireless Bluetooth Headphones",
                    Description = "High-quality wireless headphones with noise cancellation and 30-hour battery life. Perfect for music lovers and professionals.",
                    Price = 129.99m,
                    Stock = 25
                },
                new Product
                {
                    Name = "Smartphone Case",
                    Description = "Durable protective case for smartphones with shockproof design and wireless charging compatibility.",
                    Price = 24.99m,
                    Stock = 50
                },
                new Product
                {
                    Name = "USB-C Cable 6ft",
                    Description = "Fast charging USB-C cable with data transfer speeds up to 480Mbps. Compatible with most modern devices.",
                    Price = 15.99m,
                    Stock = 75
                },
                new Product
                {
                    Name = "Wireless Charging Pad",
                    Description = "10W fast wireless charging pad compatible with Qi-enabled devices. Includes LED indicator and safety features.",
                    Price = 39.99m,
                    Stock = 30
                },
                new Product
                {
                    Name = "Bluetooth Speaker",
                    Description = "Portable waterproof Bluetooth speaker with 360-degree sound and 12-hour battery life. Perfect for outdoor activities.",
                    Price = 79.99m,
                    Stock = 20
                },
                new Product
                {
                    Name = "Gaming Mouse",
                    Description = "High-precision gaming mouse with customizable RGB lighting and programmable buttons. 16000 DPI sensor.",
                    Price = 69.99m,
                    Stock = 15
                },
                new Product
                {
                    Name = "Mechanical Keyboard",
                    Description = "Compact mechanical keyboard with tactile switches and customizable backlighting. Perfect for gaming and productivity.",
                    Price = 149.99m,
                    Stock = 12
                },
                new Product
                {
                    Name = "USB Flash Drive 128GB",
                    Description = "High-speed USB 3.0 flash drive with 128GB storage capacity. Compact design with keychain attachment.",
                    Price = 29.99m,
                    Stock = 40
                },
                new Product
                {
                    Name = "Laptop Stand",
                    Description = "Adjustable aluminum laptop stand with ergonomic design. Improves airflow and reduces neck strain.",
                    Price = 49.99m,
                    Stock = 18
                },
                new Product
                {
                    Name = "Smart Watch",
                    Description = "Fitness tracking smartwatch with heart rate monitor, GPS, and 5-day battery life. Water resistant up to 50m.",
                    Price = 199.99m,
                    Stock = 8
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
