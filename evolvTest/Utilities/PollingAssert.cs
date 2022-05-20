using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace evolvAutoFramework.Utilities
{
    public abstract class PollingAssert
    {
        public static void EqualTo<T>(Func<T> predicate, T expectedResult, int timeoutms = 0) =>
            Assert.That(PollingUtility.PollingWait(predicate, expectedResult, timeoutms), Is.EqualTo(expectedResult));
    }

    public static void EqualTo<T>(Func<T> predicate, T expectedResult, int timeoutms = 0) =>
                Assume.That(PollingUtility.PollingWait(predicate, expectedResult, timeoutms), Is.EqualTo(expectedResult));
}


