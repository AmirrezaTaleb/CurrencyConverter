﻿public interface ICurrencyConverterServices
{
    void ClearConfiguration();

    void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates);

    double Convert(string fromCurrency, string toCurrency, double amount);
}
