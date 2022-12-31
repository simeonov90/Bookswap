namespace Bookswap.Application.Extensions.Methods
{
    public static class BookswapComparer
    {
        /// <summary>
        /// Compare two strings using string comparison, ordinal ignore case
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Compare(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
