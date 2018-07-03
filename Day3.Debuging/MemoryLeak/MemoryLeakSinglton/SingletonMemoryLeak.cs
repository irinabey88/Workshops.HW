using System;
using System.Collections.Generic;
using System.Numerics;
using MemoryLeakSinglton.Properties;

namespace MemoryLeakSinglton
{
    /// <summary>
    /// Represents an object SingleMemoryLeak.
    /// </summary>
    public class SingletonMemoryLeak
    {
        private static SingletonMemoryLeak _instance;
        private static List<BigInteger[]> _listOfBigObjects = new List<BigInteger[]>();

        /// <summary>
        /// Initializes a new _instance of the <see cref="SingletonMemoryLeak"/>
        /// </summary>
        private SingletonMemoryLeak() { }

        /// <summary>
        /// Gets Instance of the <see cref="SingletonMemoryLeak"/>
        /// </summary>
        public static SingletonMemoryLeak Instance => _instance ?? (_instance = new SingletonMemoryLeak());

        /// <summary>
        /// Create new array of the <see cref="BigInteger"/> and adds it to the list in objetc <see cref="SingletonMemoryLeak"/>
        /// </summary>
        /// <returns>Created array of the <see cref="BigInteger"/></returns>
        /// <exception cref="InvalidCastException">Invalid cast exception array size</exception>
        public static BigInteger[] CreateBigObject()
        {
            int arraySize;
            var isParsed =int.TryParse(Resources.ARRAY_SIZE, out arraySize);
            if (!isParsed)
            {
                throw new InvalidCastException(nameof(Resources.ARRAY_SIZE));
            }

            var bigObject = new BigInteger[arraySize];
            _listOfBigObjects.Add(bigObject);
            return bigObject;
        }
    }
}
