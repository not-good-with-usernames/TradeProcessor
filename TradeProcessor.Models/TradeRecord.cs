namespace TradeProcessor.Models
{
    public class TradeRecord
    {
        public int Id { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public float Lots { get; set; }
        public float Price { get; set; }
    }
}