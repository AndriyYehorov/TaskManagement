namespace TaskManagement.Infrastructure.Authentication
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }

        public int ExpiredHours { get; set; }
    }
}
