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
using System.Collections.Generic;
using System.Globalization;

namespace ResourceProvider
{
    /// <summary>
    /// Класс для хранения информации о словаре ресурсов
    /// </summary>
    public class ResourceDictionaryInfo
    {
        /// <summary>
        /// Дефолтный путь к словарю
        /// </summary>
        private readonly string _defaultPath;

        /// <summary>
        /// Пути к словарям ресурсов, ассоциированные с культурами
        /// </summary>
        private readonly Dictionary<CultureInfo, string> _paths;

        /// <summary>
        /// Инициализирует экземпляр ResourceDictionaryInfo
        /// </summary>
        /// <param name="name">Имя словаря</param>
        /// <param name="defaultPath">Дефолтный путь к словарю</param>
        public ResourceDictionaryInfo(string name, string defaultPath)
        {
            _defaultPath = defaultPath;
            _name = name;
        }

        /// <summary>
        /// Инициализирует экземпляр ResourceDictionaryInfo
        /// </summary>
        /// <param name="name">Имя словаря</param>
        /// <param name="defaultCultureInfo">Культура по-умолчанию</param>
        /// <param name="paths">Пути к словарям ресурсов, ассоциированные с культурами</param>
        public ResourceDictionaryInfo(string name, CultureInfo defaultCultureInfo, Dictionary<CultureInfo, string> paths)
        {
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));

            if (defaultCultureInfo == null)
                throw new ArgumentNullException(nameof(defaultCultureInfo));

            if (!paths.ContainsKey(defaultCultureInfo))
                throw new ArgumentException("Один из путей к словарям должен быть ассоциирован с культурой по-умолчанию");

            _name = name;
            _defaultPath = paths[defaultCultureInfo];
            _paths = paths;
        }

        private readonly string _name;
        /// <summary>
        /// Имя словаря ресурсов в провайдере ресурсов
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Получить путь к словарю
        /// </summary>
        /// <param name="cultureInfo">Ассоциированная со словарем культура</param>
        /// <returns>
        /// Возвращает путь к словарю, который ассоциирован с переданной культурой.
        /// В случае отсутствия такого словаря возвращает дефолтный путь.
        /// </returns>
        public string GetPath(CultureInfo cultureInfo)
        {
            if (cultureInfo != null && _paths != null && _paths.TryGetValue(cultureInfo, out string path))
            {
                return path;
            }

            return _defaultPath;
        }
    }
}