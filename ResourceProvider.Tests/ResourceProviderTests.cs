// Copyright (c) 2017 Andrey Ushkalov

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Globalization;
using NUnit.Framework;
using ResourceProvider.Exceptions;
using ResourceProvider.Interfaces;

namespace ResourceProvider.Tests
{
    /// <summary>
    /// Тесты провайдера ресурсов
    /// </summary>
    [TestFixture]
    public class ResourceProviderTests
    {
        /// <summary>
        /// Регистрация первого словаря
        /// </summary>
        /// <param name="resourceProvider">Провайдер ресурсов</param>
        private static void RegisterFirstDictionary(IResourceProvider resourceProvider)
        {
            resourceProvider.RegisterDictionary(Constants.Dictionary1);
        }

        /// <summary>
        /// Регистрация второго словаря
        /// </summary>
        /// <param name="resourceProvider">провайдер ресурсов</param>
        private static void RegisterSecondDictionary(IResourceProvider resourceProvider)
        {
            resourceProvider.RegisterDictionary(Constants.Dictionary2);
        }

        [Test]
        public void RegisterDictionary_DoubleRegistration_DoNotThrows()
        {
            IResourceProvider resourceProvider = new ResourceProvider();

            RegisterFirstDictionary(resourceProvider);

            Assert.DoesNotThrow(() => RegisterFirstDictionary(resourceProvider));
        }

        [Test]
        public void RegisterDictionary_UniqueDictionariesRegistered()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);
            RegisterFirstDictionary(resourceProvider);

            var dictionaryNames = resourceProvider.GetDictionaryNames();

            Assert.AreEqual(2, dictionaryNames.Count);
            Assert.Contains(Constants.Dictionary1.Name, dictionaryNames);
            Assert.Contains(Constants.Dictionary2.Name, dictionaryNames);
        }

        [Test]
        public void GetDictionaryNames_OneDictionary()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            var actual = resourceProvider.GetDictionaryNames();

            Assert.AreEqual(1, actual.Count);
            Assert.Contains(Constants.Dictionary1.Name, actual);
        }

        [Test]
        public void GetDictionaryNames_TwoDictionaries()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);

            var actual = resourceProvider.GetDictionaryNames();

            Assert.AreEqual(2, actual.Count);
            Assert.Contains(Constants.Dictionary1.Name, actual);
            Assert.Contains(Constants.Dictionary2.Name, actual);
        }

        [Test]
        public void CultureInfo_NullByDefault()
        {
            IResourceProvider resourceProvider = new ResourceProvider();

            Assert.IsNull(resourceProvider.CultureInfo);
        }

        [Test]
        public void CultureInfo_Changed_RaisedEvent()
        {
            var eventRaised = false;
            IResourceProvider resourceProvider = new ResourceProvider();
            resourceProvider.CultureInfoChanged += delegate { eventRaised = true; };

            resourceProvider.CultureInfo = CultureInfo.GetCultureInfo("ru-RU");

            Assert.IsTrue(eventRaised);

            eventRaised = false;
            resourceProvider.CultureInfo = new CultureInfo("ru-RU");

            Assert.IsFalse(eventRaised);
        }

        [Test]
        public void GetResource_DefaultCultureInfo()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);

            var actual = resourceProvider.GetResource<string>(Constants.DictionaryKey1, Constants.Dictionary1.Name);
            var actual2 = resourceProvider.GetResource<string>(Constants.DictionaryKey1, Constants.Dictionary2.Name);

            Assert.AreEqual(Constants.RuRu.Dictionary1Value1, actual);
            Assert.AreEqual(Constants.Default.Dictionary2Value1, actual2);
        }

        [Test]
        public void GetResource_ChangedCultureInfo()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);
            resourceProvider.CultureInfo = CultureInfo.GetCultureInfo("en-US");

            var actual = resourceProvider.GetResource<string>(Constants.DictionaryKey1, Constants.Dictionary1.Name);
            var actual2 = resourceProvider.GetResource<string>(Constants.DictionaryKey1, Constants.Dictionary2.Name);

            Assert.AreEqual(Constants.EnUs.Dictionary1Value1, actual);
            Assert.AreEqual(Constants.Default.Dictionary2Value1, actual2);
        }

        [Test]
        public void GetResource_UnknownCultureInfo()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);
            resourceProvider.CultureInfo = CultureInfo.GetCultureInfo("fr-FR");

            var actual = resourceProvider.GetResource<string>(Constants.DictionaryKey1, Constants.Dictionary1.Name);
            var actual2 = resourceProvider.GetResource<string>(Constants.DictionaryKey1, Constants.Dictionary2.Name);

            Assert.AreEqual(Constants.RuRu.Dictionary1Value1, actual);
            Assert.AreEqual(Constants.Default.Dictionary2Value1, actual2);
        }

        [Test]
        public void GetResource_DictionaryNotRegistered_Throws()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);

            Assert.Throws<DictionaryNotRegisteredException>(
                () => resourceProvider.GetResource<string>(
                            Constants.DictionaryKey1,
                            Constants.Dictionary3Name));
        }

        [Test]
        public void GetResource_ResourceNotFound_Throws()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            Assert.Throws<ResourceNotFoundException>(
                () => resourceProvider.GetResource<string>(
                            Constants.DictionaryKey2,
                            Constants.Dictionary1.Name));
        }

        [Test]
        public void GetDictionary_DefaultCultureInfo_ReturnsRegisteredInstance()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            var expected = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var actual = resourceProvider.GetDictionary(Constants.Dictionary1.Name);

            Assert.AreEqual(Constants.RuRu.Dictionary1Path, actual.Source.AbsoluteUri);
            Assert.AreEqual(Constants.RuRu.Dictionary1Path, expected.Source.AbsoluteUri);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetDictionary_ChangedCultureInfo_ReturnsRegisteredInstance()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);
            var defaultDictionary1 = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var defaultDictionary2 = resourceProvider.GetDictionary(Constants.Dictionary2.Name);

            resourceProvider.CultureInfo = CultureInfo.GetCultureInfo("en-US");
            var enUsDictionary1 = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var enUsDictionary2 = resourceProvider.GetDictionary(Constants.Dictionary2.Name);

            var actual1 = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var actual2 = resourceProvider.GetDictionary(Constants.Dictionary2.Name);

            Assert.AreEqual(Constants.RuRu.Dictionary1Path, defaultDictionary1.Source.AbsoluteUri);
            Assert.AreEqual(Constants.EnUs.Dictionary1Path, enUsDictionary1.Source.AbsoluteUri);
            Assert.AreEqual(Constants.EnUs.Dictionary1Path, actual1.Source.AbsoluteUri);
            Assert.AreNotEqual(defaultDictionary1, enUsDictionary1);
            Assert.AreNotEqual(defaultDictionary1, actual1);
            Assert.AreEqual(enUsDictionary1, actual1);

            Assert.AreEqual(Constants.Default.Dictionary2Path, defaultDictionary2.Source.AbsoluteUri);
            Assert.AreEqual(Constants.Default.Dictionary2Path, enUsDictionary2.Source.AbsoluteUri);
            Assert.AreEqual(Constants.Default.Dictionary2Path, actual2.Source.AbsoluteUri);
            Assert.AreEqual(defaultDictionary2, enUsDictionary2);
            Assert.AreEqual(defaultDictionary2, actual2);
            Assert.AreEqual(enUsDictionary2, actual2);
        }
        
        [Test]
        public void GetDictionary_UnknownCultureInfo_ReturnsRegisteredInstance()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);
            var defaultDictionary1 = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var defaultDictionary2 = resourceProvider.GetDictionary(Constants.Dictionary2.Name);

            resourceProvider.CultureInfo = CultureInfo.GetCultureInfo("fr-FR");
            var frDictionary1 = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var frDictionary2 = resourceProvider.GetDictionary(Constants.Dictionary2.Name);

            var actual1 = resourceProvider.GetDictionary(Constants.Dictionary1.Name);
            var actual2 = resourceProvider.GetDictionary(Constants.Dictionary2.Name);

            Assert.AreEqual(Constants.RuRu.Dictionary1Path, defaultDictionary1.Source.AbsoluteUri);
            Assert.AreEqual(Constants.RuRu.Dictionary1Path, frDictionary1.Source.AbsoluteUri);
            Assert.AreEqual(Constants.RuRu.Dictionary1Path, actual1.Source.AbsoluteUri);
            Assert.AreEqual(defaultDictionary1, frDictionary1);
            Assert.AreEqual(defaultDictionary1, actual1);
            Assert.AreEqual(frDictionary1, actual1);

            Assert.AreEqual(Constants.Default.Dictionary2Path, defaultDictionary2.Source.AbsoluteUri);
            Assert.AreEqual(Constants.Default.Dictionary2Path, frDictionary2.Source.AbsoluteUri);
            Assert.AreEqual(Constants.Default.Dictionary2Path, actual2.Source.AbsoluteUri);
            Assert.AreEqual(defaultDictionary2, frDictionary2);
            Assert.AreEqual(defaultDictionary2, actual2);
            Assert.AreEqual(frDictionary2, actual2);
        }

        [Test]
        public void GetDictionary_DictionaryNotRegistered_Throws()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            Assert.Throws<DictionaryNotRegisteredException>(
                () => resourceProvider.GetDictionary(Constants.Dictionary2.Name));
        }
    }
}
