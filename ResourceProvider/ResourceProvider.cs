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
using System.Linq;
using System.Windows;
using ResourceProvider.Events;
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
        /// Зарегистрированные словари
        /// </summary>
        private readonly Dictionary<string, ResourceDictionaryInfo> _resourceDictionaryInfos = new Dictionary<string, ResourceDictionaryInfo>();

        private CultureInfo _cultureInfo;
        /// <summary>
        /// Текущая культура
        /// </summary>
        /// <remarks>
        /// По-умолчанию культура равна null
        /// </remarks>
        public CultureInfo CultureInfo
        {
            get { return _cultureInfo; }
            set
            {
                if (Equals(_cultureInfo, value))
                    return;

                var oldValue = _cultureInfo;
                _cultureInfo = value;

                OnCultureInfoChanged(oldValue, _cultureInfo);
            }
        }

        /// <summary>
        /// Обработчик изменения текущей культуры
        /// </summary>
        /// <param name="oldValue">Предыдущее значение</param>
        /// <param name="newValue">Новое значение</param>
        private void OnCultureInfoChanged(CultureInfo oldValue, CultureInfo newValue)
        {
            foreach (var dictionaryInfo in _resourceDictionaryInfos.Values)
            {
                UpdateRegisteredDictionary(dictionaryInfo, newValue);
            }
            OnCultureInfoChanged(new CultureInfoChangedEventArgs(oldValue, newValue));
        }

        /// <summary>
        /// Событие смены текущей культуры
        /// </summary>
        public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChanged = delegate { };

        /// <summary>
        /// Инвокатор события смены текщей культуры
        /// </summary>
        /// <param name="e">Аргументы события</param>
        protected virtual void OnCultureInfoChanged(CultureInfoChangedEventArgs e)
        {
            CultureInfoChanged(this, e);
        }

        /// <summary>
        /// Регистрация словаря ресурсов
        /// </summary>
        /// <param name="dictionaryInfo">Информация о словаре ресурсов</param>
        /// <exception cref="OtherDictionaryAlreadyRegisteredWithSameNameException">Другой словарь уже зарегистрирован под таким именем.</exception>
        public void RegisterDictionary(ResourceDictionaryInfo dictionaryInfo)
        {
            if (_resourceDictionaryInfos.TryGetValue(dictionaryInfo.Name, out ResourceDictionaryInfo di))
            {
                if (di == dictionaryInfo)
                    return;

                throw new OtherDictionaryAlreadyRegisteredWithSameNameException(dictionaryInfo.Name);
            }

            _resourceDictionaryInfos.Add(dictionaryInfo.Name, dictionaryInfo);
            UpdateRegisteredDictionary(dictionaryInfo, CultureInfo);
        }

        /// <summary>
        /// Обновить словарь зарегистрированных словарей
        /// </summary>
        /// <param name="dictionaryInfo">Информация о словаре</param>
        /// <param name="cultureInfo">Культура</param>
        private void UpdateRegisteredDictionary(ResourceDictionaryInfo dictionaryInfo, CultureInfo cultureInfo)
        {
            var source = new Uri(dictionaryInfo.GetPath(cultureInfo));

            if (_registeredDictionaries.TryGetValue(dictionaryInfo.Name, out ResourceDictionary d))
            {
                if (d.Source == source)
                    return;

                _registeredDictionaries.Remove(dictionaryInfo.Name);
            }
            var dictionary = new ResourceDictionary { Source = source };
            _registeredDictionaries.Add(dictionaryInfo.Name, dictionary);
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
