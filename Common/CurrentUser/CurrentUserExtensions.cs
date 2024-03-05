using System.Globalization;
namespace Common.CurrentUser
{
    public static  class CurrentUserExtensions
    {
        public static string FindClaimValue(this ICurrentUser currentUser, string claimType)
        {

            return currentUser.FindClaim(claimType)?.Value;
        }

        public static T FindClaimValue<T>(this ICurrentUser currentUser, string claimType)
            where T : struct
        {
            var value = currentUser.FindClaimValue(claimType);
            if (value == null)
            {
                return default;
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
