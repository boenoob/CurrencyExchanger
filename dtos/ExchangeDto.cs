using System;

namespace CurrencyExchanger.Dtos {

    public record ExchangeDto {
        //public Guid Id { get; init; }
        public DateTimeOffset Date { get; init; }
        public string From { get; init; }

        public string To { get; init; }

        public double Amount { get; init; }

        public double ExchangeRate { get; init; }

        public double ConvertResult { get; init; }

    }
}