namespace sistema_reconocimiento.Models
{
    public class SubmitPurchaseViewModel
    {
        public Purchases Purchases { get; set; }
        public List<Rewards> Rewards { get; set; }
        public List<string> Phrases { get; set; }
    }
}
