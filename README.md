# CurrencyExchanger

API for converting currencies. Uses Fixer.io to get latest exchange rates. Rates are stored in memory to reduce calls to Fixer. Persistent storage is not implemented.

API key from fixer is required https://fixer.io/. Add key in appsettings.json

Application uses https://localhost:5001 as base path.

Endpoints

GET - /exchange<br>
Response containing all exchange rates with EUR as base<br>

GET - /exchange/convert<br>
Converts given amount from one currency to another<br>
Example request: exchange/convert?currency1=NOK&currency2=EUR&amount=100<br>
Response:<br>
{<br>
  "date":"2021-08-27T00:41:08.36495+02:00",<br>
  "from":"NOK",<br>
  "to":"EUR",<br>
  "amount":100,<br>
  "exchangeRate":0.096275727521976370<br>
  "convertResult":9.627572752197636}<br>
]<br>

GET - /exchange/rate<br>
Same response as /convert but amount is not required and is 1 by default<br>
Example request: exchange/convert?currency1=NOK&currency2=EUR<br>
