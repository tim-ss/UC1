using UC1.Controllers;
using UC1.Models;

namespace UC1.Tests
{
    [TestClass]
    public class CountriesTest
    {
        #region FilterByName Tests
        [TestMethod]
        public void TestFilterByNameWithSearchStringResultsFoundShouldReturnTheResult()
        {
            //Arrange
            var searchString = "land";
            var countries = getCountries();

            //Act
            CountriesController.filterByName(searchString, ref countries);

            //Assert
            Assert.AreEqual(2, countries.Count());
            Assert.IsNotNull(countries.Where(c => c.Name?.Common?.ToLower() == "poland").Single());
            Assert.IsNotNull(countries.Where(c => c.Name?.Common?.ToLower() == "finland").Single());
        }

        [TestMethod]
        public void TestFilterByNameWithSearchStringResultsNotFoundShouldReturnEmptyList()
        {
            //Arrange
            var searchString = "test";
            var countries = getCountries();

            //Act
            CountriesController.filterByName(searchString, ref countries);

            //Assert
            Assert.IsFalse(countries.Any());
        }


        [TestMethod]
        public void TestFilterByNameWithEmptyStringShouldReturnAllTheCountries()
        {
            //Arrange
            var searchString = string.Empty;
            var countries = getCountries();
            var countriesCount = countries.Count();

            //Act
            CountriesController.filterByName(searchString, ref countries);

            //Assert
            Assert.AreEqual(countriesCount, countries.Count());
        }
        #endregion

        #region FilterByPopulation Tests
        [TestMethod]
        public void TestFilterByPopulationShouldReturnTheResult()
        {
            //Arrange
            var population = 40;
            var countries = getCountries();

            //Act
            CountriesController.filterByPopulation(population, ref countries);

            //Assert
            Assert.AreEqual(3, countries.Count());
        }

        [TestMethod]
        public void TestFilterByPopulationWithZeroValueShouldEmptyResult()
        {
            //Arrange
            var population = 0;
            var countries = getCountries();

            //Act
            CountriesController.filterByPopulation(population, ref countries);

            //Assert
            Assert.IsFalse(countries.Any());
        }

        [TestMethod]
        public void TestFilterByPopulationWithNegativeValueShouldReturnEmptyResult()
        {
            //Arrange
            var population = -40;
            var countries = getCountries();

            //Act
            CountriesController.filterByPopulation(population, ref countries);

            //Assert
            Assert.IsFalse(countries.Any());
        }

        [TestMethod]
        public void TestFilterByPopulationWithLargeNumberShouldReturnAllTheCountries()
        {
            //Arrange
            var population = int.MaxValue;
            var countries = getCountries();
            var countriesCount = countries.Count();

            //Act
            CountriesController.filterByPopulation(population, ref countries);

            //Assert
            Assert.AreEqual(countriesCount, countries.Count());
        }
        #endregion

        #region SortByName Tests
        [TestMethod]
        public void TestSortByNameAscendShouldReturnSortedList()
        {
            //Arrange
            var direction = "ascend";
            var countries = getCountries();

            //Act
            CountriesController.sortByCountryName(direction, ref countries);

            //Assert
            var result = countries.ToList();
            Assert.AreEqual(0, result.FindIndex(c => c.Name?.Common?.ToLower() == "australia"));
            Assert.AreEqual(1, result.FindIndex(c => c.Name?.Common?.ToLower() == "finland"));
            Assert.AreEqual(2, result.FindIndex(c => c.Name?.Common?.ToLower() == "france"));
            Assert.AreEqual(3, result.FindIndex(c => c.Name?.Common?.ToLower() == "poland"));
            Assert.AreEqual(4, result.FindIndex(c => c.Name?.Common?.ToLower() == "spain"));
            Assert.AreEqual(5, result.FindIndex(c => c.Name?.Common?.ToLower() == "ukraine"));

        }

        [TestMethod]
        public void TestSortByNameDescendShouldReturnSortedList()
        {
            //Arrange
            var direction = "descend";
            var countries = getCountries();

            //Act
            CountriesController.sortByCountryName(direction, ref countries);

            //Assert
            var result = countries.ToList();
            Assert.AreEqual(5, result.FindIndex(c => c.Name?.Common?.ToLower() == "australia"));
            Assert.AreEqual(4, result.FindIndex(c => c.Name?.Common?.ToLower() == "finland"));
            Assert.AreEqual(3, result.FindIndex(c => c.Name?.Common?.ToLower() == "france"));
            Assert.AreEqual(2, result.FindIndex(c => c.Name?.Common?.ToLower() == "poland"));
            Assert.AreEqual(1, result.FindIndex(c => c.Name?.Common?.ToLower() == "spain"));
            Assert.AreEqual(0, result.FindIndex(c => c.Name?.Common?.ToLower() == "ukraine"));

        }

        [TestMethod]
        public void TestSortByNameUnknownDirectionShouldReturnUnchangedList()
        {
            //Arrange
            var direction = "test";
            var countries = getCountries();

            //Act
            CountriesController.sortByCountryName(direction, ref countries);

            //Assert
            var result = countries.ToList();
            Assert.AreEqual(4, result.FindIndex(c => c.Name?.Common?.ToLower() == "australia"));
            Assert.AreEqual(5, result.FindIndex(c => c.Name?.Common?.ToLower() == "finland"));
            Assert.AreEqual(2, result.FindIndex(c => c.Name?.Common?.ToLower() == "france"));
            Assert.AreEqual(1, result.FindIndex(c => c.Name?.Common?.ToLower() == "poland"));
            Assert.AreEqual(3, result.FindIndex(c => c.Name?.Common?.ToLower() == "spain"));
            Assert.AreEqual(0, result.FindIndex(c => c.Name?.Common?.ToLower() == "ukraine"));

        }
        #endregion

        #region Paginate Tests
        [TestMethod]
        public void TestPaginateShouldReturnFirstNItems()
        {
            //Arrange
            var limitNumber = 3;
            var countries = getCountries();

            //Act
            CountriesController.paginate(limitNumber, ref countries);

            //Assert
            Assert.AreEqual(3, countries.Count());
            Assert.IsNotNull(countries.Single(c => c.Name?.Common?.ToLower() == "ukraine"));
            Assert.IsNotNull(countries.Single(c => c.Name?.Common?.ToLower() == "poland"));
            Assert.IsNotNull(countries.Single(c => c.Name?.Common?.ToLower() == "france"));
        }

        [TestMethod]
        public void TestPaginateWithLargeNumberShouldReturnAllTheItems()
        {
            //Arrange
            var limitNumber = int.MaxValue;
            var countries = getCountries();
            var countriesCount = countries.Count();

            //Act
            CountriesController.paginate(limitNumber, ref countries);

            //Assert
            Assert.AreEqual(countriesCount, countries.Count());
        }

        [TestMethod]
        public void TestPaginateWithNegativeNumberShouldReturnEmptyResult()
        {
            //Arrange
            var limitNumber = -3;
            var countries = getCountries();

            //Act
            CountriesController.paginate(limitNumber, ref countries);

            //Assert
            Assert.AreEqual(0, countries.Count());
        }
        #endregion

        #region Private methods
        private static IEnumerable<Country> getCountries()
        {
            return new List<Country>
            {
                new Country { Name = new CountryName { Common = "Ukraine"}, Population = 43733762},
                new Country { Name = new CountryName { Common = "Poland"}, Population = 37846611},
                new Country { Name = new CountryName { Common = "France"}, Population = 65273511},
                new Country { Name = new CountryName { Common = "Spain"}, Population = 46754778},
                new Country { Name = new CountryName { Common = "Australia"}, Population = 25499884},
                new Country { Name = new CountryName { Common = "Finland"}, Population = 5540720},
            };
        }
        #endregion
    }
}