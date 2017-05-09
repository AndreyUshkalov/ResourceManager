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
using System.Windows.Input;

namespace SampleApplication.Infrastructure
{
    /// <summary>
    /// Простейшая реализация команды
    /// </summary>
    public class SimpleCommand : ICommand
    {
        /// <summary>
        /// Метод, который должна выполнить команда
        /// </summary>
        private readonly Action _execute;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="execute">Метод, который должна выполнить команда</param>
        public SimpleCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Может ли выполниться команда
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary>
    /// Простейшая реализация команды
    /// </summary>
    public class SimpleCommand<T> : ICommand
    {
        /// <summary>
        /// Метод, который должна выполнить команда
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="execute">Метод, который должна выполнить команда</param>
        public SimpleCommand(Action<T> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Может ли выполниться команда
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        public void Execute(object parameter)
        {
            _execute(parameter is T ? (T)parameter : default(T));
        }

        public event EventHandler CanExecuteChanged;
    }
}
