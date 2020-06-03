namespace TrumpEngine.Api.Configuration
{
    public class Secrets
    {
        public FirebaseSecrets Firebase { get; set; }
    }

    public class FirebaseSecrets
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
    }
}
