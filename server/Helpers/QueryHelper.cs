namespace Bookify.Helpers
{
    public class QueryHelper
    {
        /// <summary>
        /// Parses a comma-separated string of include paths into a string array
        /// </summary>
        /// <param name="includes">A comma-separated string of entity properties to include</param>
        /// <returns>An array of include paths or an empty array if none provided</returns>
        public static string[] ParseIncludes(string includes)
        {
            return string.IsNullOrEmpty(includes)
                ? Array.Empty<string>()
                : includes.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
