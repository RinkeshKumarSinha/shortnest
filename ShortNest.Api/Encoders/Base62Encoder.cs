using System.Text;

namespace ShortNest.Api.Encoders
{
    public static class Base62Encoder
    {
        private const string Base62Chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Encode(int value)
        {
            var result = new StringBuilder();
            while (value > 0)
            {
                result.Insert(0, Base62Chars[value % 62]);
                value /= 62;
            }
            return result.ToString();
        }
    }

}
