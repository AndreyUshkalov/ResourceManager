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
using System.Windows;
using ResourceProvider.Events;
using ResourceProvider.Exceptions;

namespace ResourceProvider.Interfaces
{
    /// <summary>
    /// Интерфейс провайдера ресурсов
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Текущая культура
        /// </summary>
        /// <remarks>
        /// По-умолчанию культура равна null
        /// </remarks>
        CultureInfo CultureInfo { get; set; }

        /// <summary>
        /// Событие смены текущей культуры
        /// </summary>
        event EventHandler<CultureInfoChangedEventArgs> CultureInfoChanged;

        /// <summary>
        /// Регистрация словаря ресурсов
        /// </summary>
        /// <param name="dictionaryInfo">Информация о словаре ресурсов</param>
        /// <exception cref="OtherDictionaryAlreadyRegisteredWithSameNameException">Другой словарь уже зарегистрирован под таким именем.</exception>
        void RegisterDictionary(ResourceDictionaryInfo dictionaryInfo);

        /// <summary>
        /// Получить ресурс из словаря
        /// </summary>
        /// <typeparam name="T">Тип ресурса</typeparam>
        /// <param name="resourceName">Имя ресурса</param>
        /// <param name="dictionaryName">Имя словаря ресурсов</param>
        /// <returns>Найденный ресурс</returns>
        /// <exception cref="DictionaryNotRegisteredException">Словарь не зарегистрирован.</exception>
        /// <exception cref="ResourceNotFoundException">Ресурс не найден в словаре.</exception>
        T GetResource<T>(string resourceName, string dictionaryName);

        /// <summary>
        /// Получить список зарегистрированных словарей ресурсов
        /// </summary>
        List<string> GetDictionaryNames();

        /// <summary>
        /// Получить словарь по имени
        /// </summary>
        /// <param name="dictionaryName">Имя словаря</param>
        /// <exception cref="DictionaryNotRegisteredException">Словарь не зарегистрирован.</exception>
        /// <returns>Зарегистрированный словарь</returns>
        ResourceDictionary GetDictionary(string dictionaryName);
    }
}
