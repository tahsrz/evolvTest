using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace evolvAutoFramework.Utilities
{
    public static class AutoRetry
    {
        public void AutoRetry(Action action)
        {
            var tries = 3;
            while (true)
            {
                try
                {
                    action();
                    break; // success!
                }
                catch
                {
                    if (--tries == 0)
                        throw;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
