using System.Collections.Generic;
using CurrencyExchanger.Entities;

namespace CurrencyExchanger.Repositories 
{
    public interface ExchangesInterface
    {
        void SetApiKey (string key);
        IEnumerable<Rate> GetExchangeRates();
        double GetRate(string currency1, string currency2);
        void StoreRatesFromFixer();
    }
}