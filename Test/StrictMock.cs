﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiYemek.Tests
{
    /// <summary>
    /// MockBehavior.Strict olarak çalışan Mock alt sınıfıdır.
    /// </summary>
    /// <typeparam name="T">Mocklanacak sınıfın tip bilgisi.</typeparam>
    public sealed class StrictMock<T> : Mock<T> where T : class
    {
        /// <summary>
        /// MockBehavior.Strict olarak yeni Mock sınıfı örneği oluşturan yapıcı metot.
        /// </summary>
        public StrictMock() : base(MockBehavior.Strict)
        {
        }
    }
}