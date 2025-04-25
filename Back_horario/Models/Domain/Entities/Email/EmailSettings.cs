using System.ComponentModel.DataAnnotations;

namespace Back_horario.Models.Domain.Entities.Email
{
    public class EmailSettings
    {
        [Required]
        public string SmtpServer { get; set; } = null!;
        [Required]

        public int Port { get; set; }
        [Required]

        public string SenderName { get; set; } = null!;
        [Required]

        public string SenderEmail { get; set; } = null!;
        [Required]

        public string Password { get; set; } = null!;
    }
}
