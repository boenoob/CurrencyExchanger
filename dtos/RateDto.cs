namespace CurrencyExchanger.Entities {
    public record RateDto
    {
        public string Currency { get; set; }
        public double EuroRate { get; set; }
    }
}