public class CurrencyConverterServices : ICurrencyConverterServices
{
    private Dictionary<string, Dictionary<string, double>> conversionGraph = new Dictionary<string, Dictionary<string, double>>();

    private object lockObject = new object();

    public void ClearConfiguration()
    {
        lock (lockObject)
        {
            conversionGraph.Clear();
        }
    }

    public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
    {
        lock (lockObject)
        {
            
            
            //Thread.Sleep(30000);// For Chcck Lock
            
            
            foreach (var rate in conversionRates)
            {
                string fromCurrency = rate.Item1;
                string toCurrency = rate.Item2;
                double rateValue = rate.Item3;

                if (!conversionGraph.ContainsKey(fromCurrency))
                {
                    conversionGraph[fromCurrency] = new Dictionary<string, double>();
                }
                conversionGraph[fromCurrency][toCurrency] = rateValue;
                if (!conversionGraph.ContainsKey(toCurrency))
                {
                    conversionGraph[toCurrency] = new Dictionary<string, double>();
                }
                conversionGraph[toCurrency][fromCurrency] = 1 / rateValue;
            }
        }
    }

    public double Convert(string fromCurrency, string toCurrency, double amount)
    {
        lock (lockObject)
        {
            if (!conversionGraph.ContainsKey(fromCurrency))
                throw new ArgumentException("path not found.");
            if (fromCurrency == toCurrency)
                return amount;

            var visited = new HashSet<string>();
            var queue = new Queue<Tuple<string, double>>();
            queue.Enqueue(Tuple.Create(fromCurrency, amount));
            while (queue.Count > 0)
            {
                var (currentCurrency, currentAmount) = queue.Dequeue();
                foreach (var nextCurrency in conversionGraph[currentCurrency].Keys)
                {
                    if (visited.Contains(nextCurrency))
                        continue;

                    var conversionRate = conversionGraph[currentCurrency][nextCurrency];
                    var convertedAmount = currentAmount * conversionRate;

                    if (nextCurrency == toCurrency)
                        return convertedAmount;

                    queue.Enqueue(Tuple.Create(nextCurrency, convertedAmount));
                    visited.Add(nextCurrency);
                }
            }
            throw new ArgumentException("No conversion path found.");
        }
    }
}
