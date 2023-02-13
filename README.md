# Intrinsics

.NET 7 had brought very significant performance improvements to several LINQ queries by vectorising the processing of various data structures. Whilst the improvements achieved, as denoted in this [article](https://devblogs.microsoft.com/dotnet/performance_improvements_in_net_7/#linq), are considerable, I wondered if it was possible to obtain further gains by resorting to [.NET Hardware Intrinsics](https://devblogs.microsoft.com/dotnet/hardware-intrinsics-in-net-core/), particularly for large datasets.

This project is intended to provide an example of how hardware intrinsics could be used for the Max, Min, Average and Sum methods. The benchmark tests were run on a machine that supports all versions of AVX and SSE, therefore no checks and fallback methods were required.

![Intrinsics Benchmark](https://user-images.githubusercontent.com/32436981/218456716-2c5109cd-68d2-4168-b412-3915ec5856d0.PNG)

Tests were performed on large arrays of 1 billion elements and the performance gains were very substantial. It is, therefore, clear that in business cases where the fast processing of large datasets is critical, hardware intrinsics could have an important role to play.
