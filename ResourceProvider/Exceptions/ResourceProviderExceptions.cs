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
using System.Runtime.Serialization;

namespace ResourceProvider.Exceptions
{
    /// <summary>
    /// Базовое исключение для исключений провайдера ресурсов
    /// </summary>
    [Serializable]
    public class ResourceProviderException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ResourceProviderException()
        {
        }

        public ResourceProviderException(string message)
            : base(message)
        {
        }

        public ResourceProviderException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ResourceProviderException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Словарь ресурсов не зарегистрирован
    /// </summary>
    [Serializable]
    public class DictionaryNotRegisteredException : ResourceProviderException
    {
        public DictionaryNotRegisteredException()
        {
        }

        public DictionaryNotRegisteredException(string message)
            : base(message)
        {
        }

        public DictionaryNotRegisteredException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DictionaryNotRegisteredException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Другой словарь уже зарегистрирован под таким именем
    /// </summary>
    [Serializable]
    public class OtherDictionaryAlreadyRegisteredWithSameNameException : ResourceProviderException
    {
        public OtherDictionaryAlreadyRegisteredWithSameNameException()
        {
        }

        public OtherDictionaryAlreadyRegisteredWithSameNameException(string message)
            : base(message)
        {
        }

        public OtherDictionaryAlreadyRegisteredWithSameNameException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OtherDictionaryAlreadyRegisteredWithSameNameException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Ресурс не найден в словаре
    /// </summary>
    [Serializable]
    public class ResourceNotFoundException : ResourceProviderException
    {
        public ResourceNotFoundException()
        {
        }

        public ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public ResourceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ResourceNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
