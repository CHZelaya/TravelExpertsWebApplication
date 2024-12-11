namespace TravelExpertsMVC.EmailService
{
    //this secures the email credientials of travelExperts
    public class EmailSettings
    {
        public string FromEmail { get; set; }
        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public bool ServerUseSsl { get; set; }
        public string Password { get; set; }
    }
}
