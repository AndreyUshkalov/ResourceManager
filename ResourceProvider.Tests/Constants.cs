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

namespace ResourceProvider.Tests
{
    /// <summary>
    /// Константы, использующиеся в тестах провайдера ресурсов
    /// </summary>
	internal static class Constants
    {
        /// <summary>
        /// Ключ первого значения из словаря
        /// </summary>
		public const string DictionaryKey1 = "FirstValue";

        /// <summary>
        /// Ключ второго значения из словаря
        /// </summary>
        public const string DictionaryKey2 = "SecondValue";

        /// <summary>
        /// Константы дефолтных тестовых словарей
        /// </summary>
        public static class Default
        {
            /// <summary>
            /// Путь ко второму словарю
            /// </summary>
            public const string Dictionary2Path = "pack://application:,,,/ResourceProvider.Tests;component/TestDictionaries/Dictionary2.xaml";

            /// <summary>
            /// Первое значение второго словаря
            /// </summary>
            public const string Dictionary2Value1 = "значение1";

            /// <summary>
            /// Второе значение второго словаря
            /// </summary>
            public const string Dictionary2Value2 = "значение2";
        }

        /// <summary>
        /// Константы тестовых словарей ru-RU
        /// </summary>
        public static class RuRu
        {
            /// <summary>
            /// Путь к первому словарю
            /// </summary>
            public const string Dictionary1Path = "pack://application:,,,/ResourceProvider.Tests;component/TestDictionaries/ru-RU/Dictionary1.xaml";

            /// <summary>
            /// Первое значение первого словаря
            /// </summary>
            public const string Dictionary1Value1 = "Первое значение";
        }

        /// <summary>
        /// Константы тестовых словарей en-US
        /// </summary>
        public static class EnUs
        {
            /// <summary>
            /// Путь к первому словарю
            /// </summary>
            public const string Dictionary1Path = "pack://application:,,,/ResourceProvider.Tests;component/TestDictionaries/en-US/Dictionary1.xaml";

            /// <summary>
            /// Первое значение первого словаря
            /// </summary>
            public const string Dictionary1Value1 = "First Value";
        }

        /// <summary>
        /// Первый словарь (с поддержкой локализации)
        /// </summary>
        public static readonly ResourceDictionaryInfo Dictionary1 = new ResourceDictionaryInfo("DICTIONARY1",
            CultureInfo.GetCultureInfo("ru-RU"),
            new Dictionary<CultureInfo, string>
            {
                {CultureInfo.GetCultureInfo("ru-RU"), RuRu.Dictionary1Path},
                {CultureInfo.GetCultureInfo("en-US"), EnUs.Dictionary1Path}
            });

        /// <summary>
        /// Второй словарь (без поддержки локализации)
        /// </summary>
        public static readonly ResourceDictionaryInfo Dictionary2 = new ResourceDictionaryInfo("DICTIONARY2", Default.Dictionary2Path);

        /// <summary>
        /// Имя третьего (несуществующего) словаря
        /// </summary>
        public const string Dictionary3Name = "DICTIONARY3";
    }
}
