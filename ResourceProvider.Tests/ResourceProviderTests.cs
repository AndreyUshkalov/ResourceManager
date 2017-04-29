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
            resourceProvider.RegisterDictionary(ResourceProviderTestsConstants.Dictionary1.Path, ResourceProviderTestsConstants.Dictionary1.Name);
        }

        /// <summary>
        /// Регистрация второго словаря
        /// </summary>
        /// <param name="resourceProvider">провайдер ресурсов</param>
        private static void RegisterSecondDictionary(IResourceProvider resourceProvider)
        {
            resourceProvider.RegisterDictionary(ResourceProviderTestsConstants.Dictionary2.Path, ResourceProviderTestsConstants.Dictionary2.Name);
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
            Assert.Contains(ResourceProviderTestsConstants.Dictionary1.Name, dictionaryNames);
            Assert.Contains(ResourceProviderTestsConstants.Dictionary2.Name, dictionaryNames);
        }

        [Test]
        public void RegisterDictionary_TrhowsIfDictionaryRegisteredWithOtherName()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);

            Assert.Throws<DictionaryAlreadyRegisteredWithOtherNameException>(
                () => resourceProvider.RegisterDictionary(
                            ResourceProviderTestsConstants.Dictionary1.Path,
                            ResourceProviderTestsConstants.Dictionary3Name));
        }

        [Test]
        public void GetResource()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            var actual = resourceProvider.GetResource<string>(
                            ResourceProviderTestsConstants.DictionaryKey1,
                            ResourceProviderTestsConstants.Dictionary1.Name);

            Assert.AreEqual(ResourceProviderTestsConstants.Dictionary1Value1, actual);
        }

        [Test]
        public void GetResource_DictionaryNotRegistered_Throws()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            Assert.Throws<DictionaryNotRegisteredException>(
                () => resourceProvider.GetResource<string>(
                            ResourceProviderTestsConstants.DictionaryKey1,
                            ResourceProviderTestsConstants.Dictionary2.Name));
        }

        [Test]
        public void GetResource_ResourceNotFound_Throws()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            Assert.Throws<ResourceNotFoundException>(
                () => resourceProvider.GetResource<string>(
                            ResourceProviderTestsConstants.DictionaryKey2,
                            ResourceProviderTestsConstants.Dictionary1.Name));
        }

        [Test]
        public void GetDictionaryNames_OneDictionary()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);

            var actual = resourceProvider.GetDictionaryNames();

            Assert.AreEqual(1, actual.Count);
            Assert.Contains(ResourceProviderTestsConstants.Dictionary1.Name, actual);
        }

        [Test]
        public void GetDictionaryNames_TwoDictionaries()
        {
            IResourceProvider resourceProvider = new ResourceProvider();
            RegisterFirstDictionary(resourceProvider);
            RegisterSecondDictionary(resourceProvider);

            var actual = resourceProvider.GetDictionaryNames();

            Assert.AreEqual(2, actual.Count);
            Assert.Contains(ResourceProviderTestsConstants.Dictionary1.Name, actual);
            Assert.Contains(ResourceProviderTestsConstants.Dictionary2.Name, actual);
        }
    }
}