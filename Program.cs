using System.Diagnostics;
using System.Numerics;
using FastFibonacci;

#if DEBUG
var n = new BigInteger(5);
#else
if (!TryGetInput(args, out var n, out var errorMessage))
{
    Console.Error.WriteLine(errorMessage);
    return 1;
}
#endif

var stopwatch = Stopwatch.StartNew();
var fibonacci = FibonacciCalculator.Fibonacci(n);
stopwatch.Stop();

#if DEBUG
Console.WriteLine($"F({n}) = {fibonacci} in {stopwatch.Elapsed.TotalMilliseconds} ms");
#else
GC.KeepAlive(fibonacci);
Console.WriteLine(FormatTimeSpanWithNanoseconds(stopwatch.Elapsed));
#endif

return 0;

static bool TryGetInput(string[] args, out BigInteger n, out string errorMessage)
{
    if (args.Length != 1)
    {
        n = default;
        errorMessage = "Usage: FastFibonacci <non-negative n>";
        return false;
    }

    if (!BigInteger.TryParse(args[0].Trim(), out n))
    {
        errorMessage = $"Invalid integer input: '{args[0]}'.";
        return false;
    }

    if (n < 0)
    {
        errorMessage = "n must be non-negative.";
        return false;
    }

    errorMessage = string.Empty;
    return true;
}

static string FormatTimeSpanWithNanoseconds(TimeSpan timeSpan)
{
    long nanoseconds = timeSpan.Ticks % TimeSpan.TicksPerMillisecond * 100;

    if (timeSpan.TotalSeconds < 1)
        return $"{timeSpan.Milliseconds} ms {nanoseconds} ns";

    if (timeSpan.TotalMinutes < 1)
        return $"{timeSpan.Seconds}s {timeSpan.Milliseconds} ms {nanoseconds} ns";

    if (timeSpan.TotalHours < 1)
        return $"{timeSpan.Minutes}m {timeSpan.Seconds}s {timeSpan.Milliseconds} ms";

    if (timeSpan.TotalDays < 1)
        return $"{timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";

    return $"{(int)timeSpan.TotalDays}d {timeSpan.Hours}h {timeSpan.Minutes}m";
}