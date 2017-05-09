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

using System.Windows;
using ResourceProvider.Events;
using ResourceProvider.Interfaces;
using SampleApplication.Infrastructure;
using SampleApplication.ViewModels;

namespace SampleApplication.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Локальный словарь ресурсов, предназначенный для хранения словарей из провайдера ресурсов
        /// </summary>
        private readonly ResourceDictionary _resourceDictionary = new ResourceDictionary();

        /// <summary>
        /// Инициализирует экземпляр основного окна приложения
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            var resourceProvider = App.GetResourceProvider();
            resourceProvider.CultureInfoChanged += OnResourceProviderCultureInfoChanged;
            UpdateResourceDictionaries(resourceProvider);
            Resources.MergedDictionaries.Add(resourceProvider.GetDictionary(Constants.CultureInfoNamesDictionary.Name));
            Resources.MergedDictionaries.Add(_resourceDictionary);
            DataContext = new MainViewModel(resourceProvider);
        }

        /// <summary>
        /// Обработчик изменения культуры провайдера ресурсов
        /// </summary>
        private void OnResourceProviderCultureInfoChanged(object sender, CultureInfoChangedEventArgs e)
        {
            var resourceProvider = sender as IResourceProvider;
            UpdateResourceDictionaries(resourceProvider);
        }

        /// <summary>
        /// Обновить локальный словарь ресурсов
        /// </summary>
        /// <param name="resourceProvider">Провайдер ресурсов</param>
        private void UpdateResourceDictionaries(IResourceProvider resourceProvider)
        {
            _resourceDictionary.MergedDictionaries.Clear();
            _resourceDictionary.MergedDictionaries.Add(resourceProvider.GetDictionary(Constants.StringDictionary.Name));
        }
    }
}
