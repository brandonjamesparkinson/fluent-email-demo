using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,

                // Sends via SMTP (Papercut)
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25

                // Stores as file in chosen directory
                //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation = @"C:\Demos"
            });

            StringBuilder template = new StringBuilder();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it.</p>");
            template.AppendLine("- Brandon Ltd");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("brandonjamesparkinson@gmail.com")
                .To("test@test.com", "Bethany")
                .Subject("Thanks!")
                .UsingTemplate(template.ToString(), new { FirstName = "Brandon", ProductName = "Best-Product" })
                //.Body("Thanks for buying our product.")
                .SendAsync();
        }
    }
}
