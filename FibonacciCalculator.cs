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
        return GetPairCore(n).Luc;
    }

    internal static (BigInteger Fib, BigInteger Luc) GetPair(BigInteger n)
    {
        ThrowIfNegative(n);
        return GetPairCore(n);
    }

    private static (BigInteger Fib, BigInteger Luc) GetPairCore(BigInteger n)
    {
        if (n.IsZero)
        {
            return (BigInteger.Zero, new BigInteger(2));
        }

        var half = n >> 1;
        var (fib, luc) = GetPairCore(half);
        var doubledFib = fib * luc;
        var doubledLuc = (luc * luc) - ((half.IsEven ? BigInteger.One : BigInteger.MinusOne) << 1);

        return n.IsEven
            ? (doubledFib, doubledLuc)
            : OddPairFromEven(doubledFib, doubledLuc);
    }

    private static (BigInteger Fib, BigInteger Luc) OddPairFromEven(BigInteger evenFib, BigInteger evenLuc)
    {
        var oddFib = (evenFib + evenLuc) >> 1;
        return (oddFib, oddFib + (evenFib << 1));
    }

    private static void ThrowIfNegative(BigInteger n)
    {
        if (n < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "n must be non-negative.");
        }
    }
}
