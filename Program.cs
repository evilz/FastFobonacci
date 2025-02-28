using System.Diagnostics;
using System.Numerics;



Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

#if DEBUG
var n = 5;
var (result,_) = Fibonacci(n);
stopwatch.Stop();
Console.WriteLine($"Result : {result} in {stopwatch.Elapsed.TotalMilliseconds} ms");

#else
var n = int.Parse(args[0].Trim());
Fibonacci(n);
stopwatch.Stop();
TimeSpan timeSpan = stopwatch.Elapsed;
Console.WriteLine(FormatTimeSpanWithNanoseconds(stopwatch.Elapsed));
#endif

return;

static (BigInteger Fib, BigInteger Luc) Fibonacci(BigInteger  n)
{
    // Base case
    if (n == 0) return (0, 2);
    
    // Handle negative indices
    if (n < 0)
    {
        n *= -1;
        var (fib, luc) = Fibonacci(n);
        var k = n % 2 * 2 - 1;
        return (fib * k, luc * k);
    }
    
    // Odd case
    if (n % 2 == 1)
    {
        var (fib, luc) = Fibonacci(n - 1);
        return ((fib + luc) / 2, (5 * fib + luc) / 2);
    }
    
    // Even case
    n >>= 1;
    var k2 = n % 2 * 2 - 1;
    var (f, l) = Fibonacci(n);
    return (f * l, l * l + 2 * k2);
    
    // if (n == 0) return (0, 2);
    //
    // if (n % 2 == 1)
    // {
    //     var (fib, luc) = Fibonacci(n - 1);
    //     return ((fib + luc) /2 ,(5 * fib +luc) /2) ;
    // }
    //
    // n >>= 1;
    // var k = n % 2 * 2 - 1;
    // var (f, l) = Fibonacci(n );
    // return (f * l, l * l + 2 * k);
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