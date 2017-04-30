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
using System.Linq;
using System.Windows;
using ResourceProvider.Exceptions;
using ResourceProvider.Interfaces;

namespace ResourceProvider
{
    /// <summary>
    /// Провайдер ресурсов
    /// </summary>
    public class ResourceProvider : IResourceProvider
    {
        /// <summary>
        /// Зарегистрированные словари
        /// </summary>
        private readonly Dictionary<string, ResourceDictionary> _registeredDictionaries = new Dictionary<string, ResourceDictionary>();

        /// <summary>
        /// Регистрация словаря ресурсов
        /// </summary>
        /// <param name="dictionaryPath">Путь к словарю ресурсов</param>
        /// <param name="dictionaryName">Имя словаря ресурсов</param>
        /// <exception cref="DictionaryAlreadyRegisteredWithOtherNameException">Словарь ресурсов зарегистрирован под другим именем.</exception>
        public void RegisterDictionary(string dictionaryPath, string dictionaryName)
        {
            var source = new Uri(dictionaryPath);

            ResourceDictionary value;
            if (_registeredDictionaries.TryGetValue(dictionaryName, out value) && value.Source == source)
                return;

            if (_registeredDictionaries.Values.Any(d => d.Source == source))
                throw new DictionaryAlreadyRegisteredWithOtherNameException();

            var dictionary = new ResourceDictionary { Source = source };
            _registeredDictionaries.Add(dictionaryName, dictionary);
        }

        /// <summary>
        /// Получить ресурс из словаря
        /// </summary>
        /// <typeparam name="T">Тип ресурса</typeparam>
        /// <param name="resourceName">Имя ресурса</param>
        /// <param name="dictionaryName">Имя словаря ресурсов</param>
        /// <returns>Найденный ресурс</returns>
        /// <exception cref="DictionaryNotRegisteredException">Словарь не зарегистрирован.</exception>
        /// <exception cref="ResourceNotFoundException">Ресурс не найден в словаре.</exception>
        public T GetResource<T>(string resourceName, string dictionaryName)
        {
            ResourceDictionary dictionary;
            if (!_registeredDictionaries.TryGetValue(dictionaryName, out dictionary))
                throw new DictionaryNotRegisteredException();

            if (dictionary.Keys.OfType<string>().All(key => key != resourceName))
                throw new ResourceNotFoundException();

            return (T)dictionary[resourceName];
        }

        /// <summary>
        /// Получить список зарегистрированных словарей ресурсов
        /// </summary>
        public List<string> GetDictionaryNames()
        {
            return _registeredDictionaries.Keys.ToList();
        }

        /// <summary>
        /// Получить словарь по имени
        /// </summary>
        /// <param name="dictionaryName">Имя словаря</param>
        /// <exception cref="DictionaryNotRegisteredException">Словарь не зарегистрирован.</exception>
        /// <returns>Зарегистрированный словарь</returns>
        public ResourceDictionary GetDictionary(string dictionaryName)
        {
            ResourceDictionary dictionary;
            if (!_registeredDictionaries.TryGetValue(dictionaryName, out dictionary))
                throw new DictionaryNotRegisteredException();

            return dictionary;
        }
    }
}