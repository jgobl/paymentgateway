namespace PaymentGateway.Api.Extensions
{
    public static class StringExtensions
    {
        public static string Mask(this string value)
        {
            return value.Substring(value.Length - 4).PadLeft(value.Length - 4, '*');            
        }
    }
}
