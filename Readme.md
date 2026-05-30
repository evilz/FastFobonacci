Fast Fibonacci calculation
==========================

.NET 10 Native AOT console app for fast `BigInteger` Fibonacci and Lucas calculations.

The implementation uses a fast-doubling Fibonacci/Lucas recurrence. The release CLI keeps a simple `Stopwatch` timing output for quick manual checks, but reproducible performance comparisons should be done with the included BenchmarkDotNet project.

## Build

```bash
dotnet build -c Release
```

## Run

```bash
dotnet run --project FastFibonacci.csproj -c Release -- 10000
```

## Test

```bash
dotnet test -c Release
```

## Benchmark

```bash
dotnet run --project benchmarks/FastFibonacci.Benchmarks/FastFibonacci.Benchmarks.csproj -c Release -- --filter '*'
```

The benchmark compares:

- `OriginalImplementation`: the pre-change recursive Fibonacci/Lucas implementation
- `FastDoubling`: the current implementation, which avoids the odd `n - 1` recursive branch and replaces `BigInteger.Pow(l, 2)` with `l * l`

### Benchmark environment

- BenchmarkDotNet 0.15.8
- .NET SDK 10.0.300 / .NET runtime 10.0.8
- Linux Ubuntu 24.04.4 LTS
- AMD EPYC 7763 / x64 RyuJIT
- Job config: `IterationCount=5`, `LaunchCount=1`, `WarmupCount=3`

### Benchmark results

| Method                 | N     | Mean      | Ratio | Allocated |
|----------------------- |------ |----------:|------:|----------:|
| OriginalImplementation | 1000  |  1.905 us |  1.00 |   1.09 KB |
| FastDoubling           | 1000  |  1.380 us |  0.72 |   1.05 KB |
| OriginalImplementation | 10000 | 22.390 us |  1.00 |   6.11 KB |
| FastDoubling           | 10000 | 20.965 us |  0.94 |   6.03 KB |

On this machine, the optimized version was about 28% faster for `n=1000` and about 6% faster for `n=10000`, with slightly lower managed allocations in both cases.

## Native AOT publish

```bash
dotnet publish /tmp/workspace/evilz/FastFobonacci/FastFibonacci.csproj -c Release -r linux-x64
```

## Notes

- Representative correctness tests cover Fibonacci inputs `0`, `1`, `2`, `5`, `10`, and `100`.
- Invalid CLI input is rejected for missing, malformed, or negative `n`.