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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using ResourceProvider.Interfaces;
using SampleApplication.Infrastructure;
using SampleApplication.Resources;
namespace SampleApplication.ViewModels
{
    /// <summary>
    /// Вью-модель для главного окна
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Провайдер ресурсов
        /// </summary>
        private readonly IResourceProvider _resourceProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="resourceProvider">Провайдер ресурсов</param>
        public MainViewModel(IResourceProvider resourceProvider)
        {
            _resourceProvider = resourceProvider ?? throw new ArgumentNullException(nameof(resourceProvider));
            ShowResourceCommand = new SimpleCommand(OnShowResourceCommandExecute);
        }

        /// <summary>
        /// Команда "Показать ресурс из словаря"
        /// </summary>
        public ICommand ShowResourceCommand { get; }

        /// <summary>
        /// Обработчик команды "Показать ресурс из словаря"
        /// </summary>
        private void OnShowResourceCommandExecute()
        {
            MessageBox.Show(_resourceProvider.GetResource<string>(ResourceKeys.SomeValueKey, Constants.StringDictionary.Name));
        }

        #region INotifyPropertyChaned

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChaned
    }
}