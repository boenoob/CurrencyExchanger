using System;
using System.Collections.Generic;
using CurrencyExchanger.Entities;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace CurrencyExchanger.Repositories 
{
    public class InMemExchanges : ExchangesInterface
    {
        public DateTimeOffset lastUpdated;
        private Rates EuroRates;
        private const double updateInterval = 12;
        public List<Rate> rates = null;

        // Get lates exchangerates from fixer
        public void StoreRatesFromFixer() 
        {
            string resquestUri = String.Format("http://data.fixer.io/api/latest?access_key=9f7fd6307633c37c3716508ecef9c9f5");
            WebRequest requestObjectGet = WebRequest.Create(resquestUri);
            requestObjectGet.Method = "GET";
            HttpWebResponse responseObjectGet = null;
            responseObjectGet = (HttpWebResponse)requestObjectGet.GetResponse();
            
            string strResult = null;
            using (Stream stream = responseObjectGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strResult = sr.ReadToEnd();
                sr.Close();
            }

            var responseJSON = strResult;
            using (var sr = new StringReader(responseJSON))
			using (var jr = new JsonTextReader(sr))
			{
				var serial = new JsonSerializer();
				Response responseObject = serial.Deserialize<Response>(jr);
                EuroRates = responseObject.rates;
                rates = EuroRates.AsList();
                lastUpdated = DateTimeOffset.Now;
			}
        }

        // GET list of Rates
        public IEnumerable<Rate> GetExchangeRates()
        {
            return rates;
        }

        // GET specific rate
        public double GetRate(string currency1, string currency2)
        {
            if (rates is null || TimeSinceUpdate() > updateInterval) StoreRatesFromFixer(); 

            double c1 = getEuroRate(currency1);
            double c2 = getEuroRate(currency2);
            double rate = c2/c1;

            if (c1 < 0 || c2 < 0) return -1;

            return rate;
        }
        
        private double TimeSinceUpdate() 
        {
            double time = (DateTimeOffset.Now - lastUpdated).Hours;
            return time;
        }

        // GET euro rate
        private double getEuroRate(string currency) 
        {
            foreach (Rate rate in rates) 
            {
                if(rate.Currency == currency) 
                {
                    return rate.EuroRate;
                }
            }
            return -1;    
        }
    }    
}
