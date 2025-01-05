using System;
using System.Collections.Generic;

public static class ListExtensions
{
    /// <summary>
    /// Returns a random element from the list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to select a random element from.</param>
    /// <returns>A random element from the list.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the list is empty.</exception>
    public static T Random<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("Cannot select a random element from an empty or null list.");
        }

        Random random = new Random();
        int index = random.Next(list.Count);
        return list[index];
    }
}
