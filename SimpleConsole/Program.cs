// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

Console.WriteLine("Hello, World!");

var summary = BenchmarkRunner.Run<ExtensionTests>();

[MemoryDiagnoser]
public class ExtensionTests
{
    private static readonly List<string> ListOfStrings =
        Enumerable.Range(0, 100000).Select(x => Guid.NewGuid().ToString()).ToList();

    [Benchmark]
    public void GroupBy() => ListOfStrings.ThrowIfDuplicate_GroupBy(x => x);

    [Benchmark]
    public void Distinct() => ListOfStrings.ThrowIfDuplicate_Distinct(x => x);
}

public static class Extensions
{
    public static IEnumerable<TSource> ThrowIfDuplicate_GroupBy<TSource, TKey>(
        this IEnumerable<TSource> parameter,
        Func<TSource, TKey> keySelector,
        [CallerArgumentExpression(nameof(parameter))]
        string? parameterName = null,
        string? message = null
    )
    {
        var list = parameter.ToList();
        var hasDuplicates = list.GroupBy(keySelector).Any(g => g.Count() > 1);
        return hasDuplicates
            ? throw new ArgumentException(message ?? $"List '{list}' contains duplicates!", parameterName)
            : list;
    }


    public static IReadOnlyList<TSource> ThrowIfDuplicate_Distinct<TSource, TKey>(
        this IReadOnlyList<TSource> parameter,
        Func<TSource, TKey> keySelector,
        [CallerArgumentExpression(nameof(parameter))]
        string? parameterName = null,
        string? message = null
    )
    {
        var list = parameter.Select(keySelector).ToList();
        return list.Distinct().Count() != list.Count
            ? throw new ArgumentException(message ?? $"List '{list}' contains duplicates!", parameterName)
            : parameter;
    }
}