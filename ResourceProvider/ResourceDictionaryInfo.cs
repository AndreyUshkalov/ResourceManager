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

namespace ResourceProvider
{
    /// <summary>
    /// Класс для хранения информации о словаре ресурсов
    /// </summary>
    public class ResourceDictionaryInfo
    {
        /// <summary>
        /// Инициализирует экземпляр ResourceDictionaryInfo
        /// </summary>
        /// <param name="path">Путь к словарю</param>
        /// <param name="name">Имя словаря</param>
        public ResourceDictionaryInfo(string path, string name)
        {
            _path = path;
            _name = name;
        }

        private readonly string _path;
        /// <summary>
        /// Путь к словарю ресурсов
        /// </summary>
        public string Path
        {
            get { return _path; }
        }

        private readonly string _name;
        /// <summary>
        /// Имя словаря ресурсов в провайдере ресурсов
        /// </summary>
        public string Name
        {
            get { return _name; }
        }
    }
}