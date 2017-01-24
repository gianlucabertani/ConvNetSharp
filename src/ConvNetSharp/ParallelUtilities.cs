using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvNetSharp
{
    public class ParallelUtilities
    {
        public static void For(int fromInclusive, int toExclusive, Action<int> body)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.For(fromInclusive, toExclusive, options, body);
        }

        public static void For(long fromInclusive, long toExclusive, Action<long> body)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.For(fromInclusive, toExclusive, options, body);
        }

        public static void For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.For(fromInclusive, toExclusive, options, localInit, body, localFinally);
        }

        public static void For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
        {
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;

            Parallel.For(fromInclusive, toExclusive, options, localInit, body, localFinally);
        }
    }
}
