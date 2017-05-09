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

using System;
using System.Windows;
using NUnit.Framework;

namespace ResourceProvider.Tests
{
    /// <summary>
    /// Тесты для проверки содержимого тестовых словарей
    /// </summary>
    [TestFixture]
    public class TestDictionaryTests
    {
        /// <summary>
        /// Получить словарь ресурсов
        /// </summary>
        /// <param name="path">Путь к словарю</param>
        private static ResourceDictionary GetResourceDictionary(string path)
        {
            return new ResourceDictionary { Source = new Uri(path) };
        }

        [Test]
        public void TestDictionary1RuRuExists()
        {
            GetResourceDictionary(Constants.RuRu.Dictionary1Path);
        }

        [Test]
        public void TestDictionary1RuRuContent()
        {
            var testDictionary = GetResourceDictionary(Constants.RuRu.Dictionary1Path);
            Assert.Contains(Constants.DictionaryKey1, testDictionary.Keys);
            Assert.AreEqual(Constants.RuRu.Dictionary1Value1, testDictionary[Constants.DictionaryKey1]);
            Assert.That(testDictionary.Keys, Has.No.Contain(Constants.DictionaryKey2));
        }

        [Test]
        public void TestDictionary1EnUsExists()
        {
            GetResourceDictionary(Constants.EnUs.Dictionary1Path);
        }

        [Test]
        public void TestDictionary1EnUsContent()
        {
            var testDictionary = GetResourceDictionary(Constants.EnUs.Dictionary1Path);
            Assert.Contains(Constants.DictionaryKey1, testDictionary.Keys);
            Assert.AreEqual(Constants.EnUs.Dictionary1Value1, testDictionary[Constants.DictionaryKey1]);
            Assert.That(testDictionary.Keys, Has.No.Contain(Constants.DictionaryKey2));
        }

        [Test]
        public void TestDictionary2Exists()
        {
            GetResourceDictionary(Constants.Default.Dictionary2Path);
        }

        [Test]
        public void TestDictionary2Content()
        {
            var testDictionary = GetResourceDictionary(Constants.Default.Dictionary2Path);
            Assert.Contains(Constants.DictionaryKey1, testDictionary.Keys);
            Assert.AreEqual(Constants.Default.Dictionary2Value1, testDictionary[Constants.DictionaryKey1]);
            Assert.Contains(Constants.DictionaryKey2, testDictionary.Keys);
            Assert.AreEqual(Constants.Default.Dictionary2Value2, testDictionary[Constants.DictionaryKey2]);
        }
    }
}
