namespace Back_horario.Services.Interface.Email
{
    public interface IEmailServices
    {
        public Task<bool> SendResetToken(string toEmail, string token, DateTime? expiration);

    }
}
