using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FastFibonacci;

BenchmarkRunner.Run<FibonacciBenchmarks>();

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 5)]
public class FibonacciBenchmarks
{
    [Params(1_000, 10_000)]
    public int N { get; set; }

    [Benchmark(Baseline = true)]
    public BigInteger OriginalImplementation() => OriginalFibonacci(N).Fib;

    [Benchmark]
    public BigInteger FastDoubling() => FibonacciCalculator.Fibonacci(N);

    private static (BigInteger Fib, BigInteger Luc) OriginalFibonacci(BigInteger n)
    {
        if (n.IsZero)
        {
            return (BigInteger.Zero, new BigInteger(2));
        }

        if (!n.IsEven)
        {
            var (previousFib, previousLuc) = OriginalFibonacci(n - 1);
            return ((previousFib + previousLuc) / 2, ((5 * previousFib) + previousLuc) / 2);
        }

        var half = n >> 1;
        var sign = (half % 2) * 2 - 1;
        var (halfFib, halfLuc) = OriginalFibonacci(half);
        return (halfFib * halfLuc, BigInteger.Pow(halfLuc, 2) + (2 * sign));
    }
}
