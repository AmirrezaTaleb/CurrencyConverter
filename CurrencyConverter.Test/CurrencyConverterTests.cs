using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Test
{
     [TestFixture]
    public class CurrencyConverterTests
    {
        private ICurrencyConverterServices converter;

        [SetUp]
        public void SetUp()
        {
            converter = new CurrencyConverterServices();
        }

        [Test]
        public void TestConvert_USD_to_EUR()
        {
            // Arrange
            converter.UpdateConfiguration(new List<Tuple<string, string, double>>
            {
                Tuple.Create("USD", "CAD", 1.34),
                Tuple.Create("CAD", "GBP", 0.58),
                Tuple.Create("USD", "EUR", 0.86)
            });

            // Act
            double result = converter.Convert("USD", "EUR", 100);

            // Assert
            Assert.AreEqual(86, result);
        }

        [Test]
        public void TestConvert_CAD_to_GBP()
        {
            // Arrange
            converter.UpdateConfiguration(new List<Tuple<string, string, double>>
            {
                Tuple.Create("USD", "CAD", 1.34),
                Tuple.Create("CAD", "GBP", 0.58),
                Tuple.Create("USD", "EUR", 0.86)
            });

            // Act
            double result = converter.Convert("CAD", "GBP", 100);

            // Assert
            Assert.AreEqual(57.999999999999993d, result);
        }
        [Test]
        public void TestConvert_EUR_to_USD()
        {
            // Arrange
            converter.UpdateConfiguration(new List<Tuple<string, string, double>>
            {
                Tuple.Create("USD", "CAD", 1.34),
                Tuple.Create("CAD", "GBP", 0.58),
                Tuple.Create("USD", "EUR", 0.86)
            });

            // Act
            double result = converter.Convert("EUR", "USD", 100);

            // Assert
            Assert.AreEqual(116.27906976744187, result);
        }
        [Test]
        public void TestConvert_EUR_to_GBP()
        {
            // Arrange
            converter.UpdateConfiguration(new List<Tuple<string, string, double>>
            {
                Tuple.Create("USD", "CAD", 1.34),
                Tuple.Create("CAD", "GBP", 0.58),
                Tuple.Create("USD", "EUR", 0.86)
            });

            // Act
            double result = converter.Convert("USD", "GBP", 100);

            // Assert
            Assert.AreEqual(77.719999999999999d, result);
        }
        [Test]
        public void TestConvert_InvalidCurrencies()
        {
            // Arrange
            converter.UpdateConfiguration(new List<Tuple<string, string, double>>
            {
                Tuple.Create("USD", "CAD", 1.34),
                Tuple.Create("CAD", "GBP", 0.58),
                Tuple.Create("USD", "EUR", 0.86)
            });

            // Act & Assert
            Assert.Throws<ArgumentException>(() => converter.Convert("EUR", "IR", 100));
        }
    }
}
