using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kreipiniai.Tests
{
    [TestClass]
    public class Tests
    {
        private readonly Kreipiniai _sut;

        public Tests()
        {
            _sut = new Kreipiniai();
        }

        [TestMethod]
        public async Task ReturnsNullForNullQuery()
        {
            var res = await _sut.GetFor(null);
            Assert.IsNull(res);
        }

        [TestMethod]
        [DataRow("Martynas", "Martynai")]
        [DataRow("Vytautas", "Vytautai")]
        public async Task ReturnsCorrectlyForMaleNamesEndingWithAs(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Abdula", "Abdula")]
        public async Task ReturnsCorrectlyForMaleNamesEndingWithA(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Gytis", "Gyti")]
        [DataRow("Genovaitis", "Genovaiti")]
        public async Task ReturnsCorrectlyForMaleNamesEndingWithIs(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Adolius", "Adoliau")]
        [DataRow("Vilius", "Viliau")]
        public async Task ReturnsCorrectlyForMaleNamesEndingWithIus(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Adrijus", "Adrijau")]
        public async Task ReturnsCorrectlyForMaleNamesEndingWithUs(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Inga", "Inga")]
        [DataRow("Raminta", "Raminta")]
        public async Task ReturnsCorrectlyForFemaleNamesEndingWithA(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Gabrielė", "Gabriele")]
        [DataRow("Skaistė", "Skaiste")]
        public async Task ReturnsCorrectlyForFemaleNamesEndingWithE(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("Žygimantas", "Žygimantai")]
        [DataRow("Ąžuolas", "Ąžuolai")]
        [DataRow("Česlovas", "Česlovai")]
        [DataRow("Kęstutis", "Kęstuti")]
        [DataRow("Mėnulis", "Mėnuli")]
        [DataRow("Jokūbas", "Jokūbai")]
        [DataRow("Rūta", "Rūta")]
        [DataRow("Živilė", "Živile")]
        public async Task ReturnsCorrectlyNamesWithLithuanianLetters(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("saulius", "sauliau")]
        [DataRow("renaldas", "renaldai")]
        [DataRow("agnė", "agne")]
        public async Task ReturnsCorrectlyForLowercaseNames(string name, string expected)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(expected, res);
        }

        [TestMethod]
        [DataRow("fargaregaergae")]
        [DataRow("agraergijoijas")]
        public async Task ReturnsSameStringForNonExistantName(string name)
        {
            var res = await _sut.GetFor(name);
            Assert.AreEqual(name, res);
        }
    }
}
