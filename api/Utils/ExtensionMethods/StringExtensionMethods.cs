using System.Text;

namespace api.Utils.ExtensionMethods;
public static class StringExtensionMethods
{
    public static string RemoveAcentuation(this string text) =>
        System.Web.HttpUtility.UrlDecode(
            System.Web.HttpUtility.UrlEncode(
                text, Encoding.GetEncoding("iso-8859-7")));
}

