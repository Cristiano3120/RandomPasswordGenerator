using System.Security.Cryptography;
using System.Windows;

namespace RandomPasswordGenerator
{
    public static class PasswordGenerator
    {
        private static readonly Dictionary<Chars, string> _charMap = new()
        {
            { Chars.LowerCase, "abcdefghijklmnopqrstuvwxyz" },
            { Chars.UpperCase, "ABCDEFGHIJKLMNOPQRSTUVWXYZ" },
            { Chars.Digits, "0123456789" },
            { Chars.Brackets, "()" },
            { Chars.CurlyBrackets, "{}" },
            { Chars.Dollar, "$" },
            { Chars.ExclamationMark, "!" },
            { Chars.QuestionMark, "?" },
            { Chars.AtSign, "@" },
            { Chars.Hashtag, "#" },
            { Chars.Percent, "%" },
            { Chars.Dot, "." },
            { Chars.And, "&" },
            { Chars.QuotationMark, "\"" },
            { Chars.Euro, "€" }
        };

        public static string GeneratePassword(Dictionary<Chars, bool> chars, int passwordLength)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                List<char> allowedChars = [];
                foreach (KeyValuePair<Chars, bool> kvp in chars)
                {
                    if (kvp.Value)
                    {
                        string charSet = _charMap[kvp.Key];
                        if (kvp.Key == Chars.LowerCase || kvp.Key == Chars.UpperCase)
                        {
                            allowedChars.AddRange(charSet);
                        }
                        else
                        {
                            int repeatCharCount = Random.Shared.Next(2, 4); ; 
                            for (int i = 0; i < repeatCharCount; i++)
                            {
                                allowedChars.AddRange(charSet);
                            }
                        }
                    }
                }

                if (allowedChars.Count == 0)
                {
                    MessageBox.Show("Please select atleast one type of chars.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return string.Empty;
                }

                char[] password = new char[passwordLength];
                byte[] randomBytes = new byte[passwordLength];

                for (int i = 0; i < passwordLength; i++)
                {
                    rng.GetBytes(randomBytes, 0, 1);
                    int index = randomBytes[0] % allowedChars.Count;
                    password[i] = allowedChars[index];
                }

                return new string(password);
            }
        }
    }
}
