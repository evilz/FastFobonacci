using System.Numerics;

namespace FastFibonacci;

public static class FibonacciCalculator
{
    public static BigInteger Fibonacci(BigInteger n)
    {
        ThrowIfNegative(n);
        return GetPairCore(n).Fib;
    }

    public static BigInteger Lucas(BigInteger n)
    {
        ThrowIfNegative(n);
        var (fib, nextFib) = GetPairCore(n);
        return (nextFib << 1) - fib;
    }

    internal static (BigInteger Fib, BigInteger NextFib) GetPair(BigInteger n)
    {
        ThrowIfNegative(n);
        return GetPairCore(n);
    }

    private static (BigInteger Fib, BigInteger NextFib) GetPairCore(BigInteger n)
    {
        if (n.IsZero)
        {
            return (BigInteger.Zero, BigInteger.One);
        }

        var (fib, nextFib) = GetPairCore(n >> 1);
        var doubledFib = fib * ((nextFib << 1) - fib);
        var doubledNextFib = (fib * fib) + (nextFib * nextFib);

        return n.IsEven
            ? (doubledFib, doubledNextFib)
            : (doubledNextFib, doubledFib + doubledNextFib);
    }

    private static void ThrowIfNegative(BigInteger n)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "n must be non-negative.");
        }
    }
}
