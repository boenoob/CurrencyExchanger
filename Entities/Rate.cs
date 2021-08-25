namespace CurrencyExchanger.Entities {
    public record Rate
    {
        public string Currency { get; set; }
        public double EuroRate { get; set; }
    }
}