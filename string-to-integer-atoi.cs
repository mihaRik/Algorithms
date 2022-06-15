using System;

namespace Algorithms
{
    public class string_to_integer_atoi
    {
        public static void RunTestCases()
        {
            var s = "   -42";

            Console.WriteLine(MyAtoi(s).ToString());
        }

        static int MyAtoi(string s)
        {
            ValidateString(s);

            var (stringNumber, isPositive) = FindOnlyNumberAndSignFromString(s);

            return ParseInt(stringNumber, isPositive);
        }

        static int ParseInt(string stringNumber, bool isPositive)
        {
            long multiplier = 0;
            long result = 0;

            for (var i = stringNumber.Length - 1; i >= 0; i--)
            {
                var c = stringNumber[i];
                var number = GetIntFromChar(c);

                if (!isPositive)
                {
                    number = -number;
                }

                if (multiplier == 0)
                {
                    result += number;
                    multiplier = 10;
                }
                else
                {
                    result += multiplier * number;
                    multiplier *= 10;
                }

                if (result > int.MaxValue)
                {
                    return int.MaxValue;
                }

                if (result < int.MinValue)
                {
                    return int.MinValue;
                }
            }

            return (int)result;
        }

        static (string, bool) FindOnlyNumberAndSignFromString(string s)
        {
            var firstNumber = FindFirstNumber(s);
            var firstSign = FindFirstSign(s);
            var firstChar = FindFirstChar(s);
            var number = "";
            var isPositive = true;

            if (firstChar != -1 && (IsItLetter(s[firstChar]) || IsItDot(s[firstChar])))
            {
                return (number, isPositive);
            }

            if (firstNumber == firstSign && firstSign == -1)
            {
                return (number, isPositive);
            }


            if ((firstNumber != -1 && firstSign != -1 && firstNumber < firstSign) ||
                firstSign == -1 && firstNumber != -1)
            {
                for (var i = firstNumber; i < s.Length; i++)
                {
                    var c = s[i];
                    if (IsItNumber(c))
                    {
                        number += c;
                    }
                    else
                    {
                        break;
                    }
                }

                return (number, isPositive);
            }

            if (firstSign != -1 && IsItMinusSign(s[firstSign]))
            {
                isPositive = false;
            }

            for (var i = firstSign + 1; i < s.Length; i++)
            {
                var c = s[i];
                if (IsItNumber(c))
                {
                    number += c;
                }
                else
                {
                    break;
                }
            }

            return (number, isPositive);
        }

        static int FindFirstChar(string s)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (!IsItSpace(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        static int FindFirstNumber(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (IsItNumber(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        static int FindFirstSign(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (IsItPlusSign(s[i]) || IsItMinusSign(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        static void ValidateString(string s)
        {
            if (s.Length > 200)
            {
                throw new ArgumentOutOfRangeException(
                    $"Length should be more than or equal 0 and less than or equal 200");
            }

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (!IsItAllowedCharacter(c))
                {
                    throw new ArgumentOutOfRangeException($"String should contain only allowed characters. Value: {c}");
                }
            }
        }

        static bool IsItAllowedCharacter(char c)
        {
            return IsItAlphaNumeric(c) || IsItSpace(c) || IsItPlusSign(c) || IsItMinusSign(c) || IsItDot(c);
        }

        static bool IsItNumber(char c)
        {
            return c >= 48 && c <= 57;
        }

        static bool IsItUpperLetter(char c)
        {
            return c >= 65 && c <= 90;
        }

        static bool IsItLowerLetter(char c)
        {
            return c >= 97 && c <= 122;
        }

        static bool IsItLetter(char c)
        {
            return IsItUpperLetter(c) || IsItLowerLetter(c);
        }

        static bool IsItAlphaNumeric(char c)
        {
            return IsItLetter(c) || IsItNumber(c);
        }

        static bool IsItSpace(char c)
        {
            return c == 32;
        }

        static bool IsItPlusSign(char c)
        {
            return c == 43;
        }

        static bool IsItMinusSign(char c)
        {
            return c == 45;
        }

        static bool IsItDot(char c)
        {
            return c == 46;
        }

        static int GetIntFromChar(int c)
        {
            return c switch
            {
                48 => 0,
                49 => 1,
                50 => 2,
                51 => 3,
                52 => 4,
                53 => 5,
                54 => 6,
                55 => 7,
                56 => 8,
                57 => 9,
                _ => throw new ArgumentOutOfRangeException($"This is not a number. Value: {c}")
            };
        }
    }
}