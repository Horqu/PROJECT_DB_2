namespace WebApp.Models
{
    public class Ocena
    {
        public int OcenaId { get; set; }
        public string Wartosc { get; set; }
        public DateTime Data { get; set; }
        public int StudentId { get; set; }
        public int KursId { get; set; }
    }
}
