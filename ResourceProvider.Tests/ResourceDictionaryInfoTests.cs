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

using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace ResourceProvider.Tests
{
    /// <summary>
    /// Тесты для класса хранения информации о словаре ресурсов
    /// </summary>
    [TestFixture]
    public class ResourceDictionaryInfoTests
    {
        [Test]
        public void GetPath_NullCultureInfo_ReturnsDefaultPath()
        {
            var info = new ResourceDictionaryInfo("Name", Constants.Default.Dictionary2Path);

            var actual = info.GetPath(null);

            Assert.AreEqual(Constants.Default.Dictionary2Path, actual);
        }

        [Test]
        public void GetPath_NullCultureInfo_ReturnsDefaultPath_Test2()
        {
            var resourceDictionaries = new Dictionary<CultureInfo, string>
            {
                {CultureInfo.GetCultureInfo("ru-RU"), Constants.RuRu.Dictionary1Path},
                {CultureInfo.GetCultureInfo("en-US"), Constants.EnUs.Dictionary1Path}
            };
            var info = new ResourceDictionaryInfo("Name", CultureInfo.GetCultureInfo("ru-RU"), resourceDictionaries);

            var actual = info.GetPath(null);

            Assert.AreEqual(Constants.RuRu.Dictionary1Path, actual);
        }

        [Test]
        public void GetPath_UnknownCultureInfo_ReturnsDefaultPath()
        {
            var info = new ResourceDictionaryInfo("Name", Constants.Default.Dictionary2Path);

            var actual = info.GetPath(CultureInfo.GetCultureInfo("fr-FR"));

            Assert.AreEqual(Constants.Default.Dictionary2Path, actual);
        }

        [Test]
        public void GetPath_UnknownCultureInfo_ReturnsDefaultPath_Test2()
        {
            var resourceDictionaries = new Dictionary<CultureInfo, string>
            {
                {CultureInfo.GetCultureInfo("ru-RU"), Constants.RuRu.Dictionary1Path},
                {CultureInfo.GetCultureInfo("en-US"), Constants.EnUs.Dictionary1Path}
            };
            var info = new ResourceDictionaryInfo("Name", CultureInfo.GetCultureInfo("ru-RU"), resourceDictionaries);

            var actual = info.GetPath(CultureInfo.GetCultureInfo("fr-FR"));

            Assert.AreEqual(Constants.RuRu.Dictionary1Path, actual);
        }

        [Test]
        public void GetPath_ReturnsPath()
        {
            var resourceDictionaries = new Dictionary<CultureInfo, string>
            {
                {CultureInfo.GetCultureInfo("ru-RU"), Constants.RuRu.Dictionary1Path},
                {CultureInfo.GetCultureInfo("en-US"), Constants.EnUs.Dictionary1Path}
            };
            var info = new ResourceDictionaryInfo("Name", CultureInfo.GetCultureInfo("ru-RU"), resourceDictionaries);

            var actual = info.GetPath(CultureInfo.GetCultureInfo("en-US"));

            Assert.AreEqual(Constants.EnUs.Dictionary1Path, actual);
        }

        [Test]
        public void GetPath_ReturnsPath_Test2()
        {
            var resourceDictionaries = new Dictionary<CultureInfo, string>
            {
                {CultureInfo.GetCultureInfo("ru-RU"), Constants.RuRu.Dictionary1Path},
                {CultureInfo.GetCultureInfo("en-US"), Constants.EnUs.Dictionary1Path}
            };
            var info = new ResourceDictionaryInfo("Name", CultureInfo.GetCultureInfo("ru-RU"), resourceDictionaries);

            var actual = info.GetPath(CultureInfo.GetCultureInfo("ru-RU"));

            Assert.AreEqual(Constants.RuRu.Dictionary1Path, actual);
        }
    }
}
