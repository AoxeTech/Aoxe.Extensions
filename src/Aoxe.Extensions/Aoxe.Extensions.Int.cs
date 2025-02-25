namespace Aoxe.Extensions;

/// <summary>
/// Provides extension methods for integer-based range operations.
/// </summary>
public static partial class AoxeExtension
{
    /// <summary>
    /// Enables foreach iteration over an integer count, generating a sequence from 0 to count-1.
    /// </summary>
    /// <param name="count">The number of elements to generate</param>
    /// <returns>An enumerator for the generated sequence</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when count is negative
    /// </exception>
    /// <example>
    /// <code>
    /// foreach (int i in 5)
    /// {
    ///     Console.WriteLine(i); // Outputs 0,1,2,3,4
    /// }
    /// </code>
    /// </example>
    /// <remarks>
    /// This method enables syntactic sugar for quick range iterations:
    /// <list type="bullet">
    /// <item><description>Zero-based sequence</description></item>
    /// <item><description>Count must be non-negative</description></item>
    /// <item><description>Underlying implementation uses Enumerable.Range</description></item>
    /// </list>
    /// </remarks>
    public static IEnumerator<int> GetEnumerator(this int count) => count.Range().GetEnumerator();

    /// <summary>
    /// Generates a sequence of integers starting from a specified value.
    /// </summary>
    /// <param name="count">Number of integers to generate</param>
    /// <param name="start">Starting value (default: 0)</param>
    /// <returns>An IEnumerable&lt;int&gt; sequence</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">
    /// Thrown when count is negative
    /// </exception>
    /// <example>
    /// <code>
    /// var numbers = 5.Range(3); // Returns [3,4,5,6,7]
    /// </code>
    /// </example>
    public static IEnumerable<int> Range(this int count, int start = 0) =>
        Enumerable.Range(start, count);
}
