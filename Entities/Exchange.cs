using System;

namespace CurrencyExchanger.Entities {
    public record Exchange {

        public DateTimeOffset Date { get; init; }

        public string Currency1 { get; init; }

        public string Currency2 { get; init; }

        public double Amount { get; init; }

        public double ExchangeRate { get; init; }

        public double ConvertResult { get; init; }
        
    }
}