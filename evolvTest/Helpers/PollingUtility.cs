using System;
using System.Diagnostics;
using System.Threading;

namespace evolvAutoFramework.Helpers
{
    public static class PollingUtility
    {
        private static readonly int POLLING_INTERVAL = ConfigurationHelpers.PollingInterval;
        private static readonly int MAX_WAIT_TIME = ConfigurationHelpers.MaxWaitTime;

        public static bool Polling(Func<bool> predicate, int timeoutms = 0)
        {
            timeoutms = timeoutms > 0 ? timeoutms : MAX_WAIT_TIME;
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutms)
            {
                if (predicate())
                {
                    return true;
                }
                Thread.Sleep(POLLING_INTERVAL);
            }
            return false;
        }

        public static T PollingWait<T>(Func<T> predicate, T expectedResult, int timeoutms = 0)
        {
            timeoutms = timeoutms > 0 ? timeoutms : MAX_WAIT_TIME;
            Stopwatch sw = Stopwatch.StartNew();
            T result;
            do
            {
                try
                {
                    result = predicate();
                    if (result.Equals(expectedResult))
                    {
                        return result;
                    }
                }
                catch { }
                Thread.Sleep(POLLING_INTERVAL);
            } while (sw.ElapsedMilliseconds < timeoutms);
            return predicate();
        }

        public static string PollingWaitNotNullOrEmpty<T>(Func<T> predicate, int timeoutms = 0)
        {
            timeoutms = timeoutms > 0 ? timeoutms : MAX_WAIT_TIME;
            Stopwatch sw = Stopwatch.StartNew();
            string result;
            do
            {
                result = predicate().ToString();
                if (!string.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
                Thread.Sleep(POLLING_INTERVAL);
            } while (sw.ElapsedMilliseconds < timeoutms);
            return result;
        }

        public static void PollingRowCount(Func<int> predicate, int expectedResult, int timeoutms = 0)
        {
            timeoutms = timeoutms > 0 ? timeoutms : MAX_WAIT_TIME;
            Stopwatch sw = Stopwatch.StartNew();
            int result;
            do
            {
                try
                {
                    result = predicate();
                    Console.WriteLine("Result= " + result);
                    Console.WriteLine("expectedResult= " + expectedResult);
                    if (result >= expectedResult) ;
                }
                catch { }
                Thread.Sleep(POLLING_INTERVAL);
            } while (sw.ElapsedMilliseconds < timeoutms);

        }
    }
}
