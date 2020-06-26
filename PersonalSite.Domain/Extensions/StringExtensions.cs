namespace PersonalSite.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string @this)
        {

            return string.IsNullOrEmpty(@this);
        }
    }
}