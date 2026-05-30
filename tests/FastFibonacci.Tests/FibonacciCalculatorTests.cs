using System.Globalization;
using System.Numerics;
using FastFibonacci;

namespace FastFibonacci.Tests;

public class FibonacciCalculatorTests
{
    [Theory]
    [InlineData("0", "0")]
    [InlineData("1", "1")]
    [InlineData("2", "1")]
    [InlineData("5", "5")]
    [InlineData("10", "55")]
    [InlineData("100", "354224848179261915075")]
    public void Fibonacci_returns_expected_values(string input, string expected)
    {
        var result = FibonacciCalculator.Fibonacci(Parse(input));

        Assert.Equal(Parse(expected), result);
    }

    [Theory]
    [InlineData("0", "2")]
    [InlineData("1", "1")]
    [InlineData("2", "3")]
    [InlineData("5", "11")]
    [InlineData("10", "123")]
    [InlineData("20", "15127")]
    public void Lucas_returns_expected_values(string input, string expected)
    {
        var result = FibonacciCalculator.Lucas(Parse(input));

        Assert.Equal(Parse(expected), result);
    }

    [Fact]
    public void Negative_inputs_throw()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciCalculator.Fibonacci(BigInteger.MinusOne));
        Assert.Throws<ArgumentOutOfRangeException>(() => FibonacciCalculator.Lucas(BigInteger.MinusOne));
    }

    private static BigInteger Parse(string value) => BigInteger.Parse(value, CultureInfo.InvariantCulture);
}
