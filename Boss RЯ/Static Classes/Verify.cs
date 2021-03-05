using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Boss_RЯ.Helper_Static_Classes;

namespace Boss_RЯ.Static_Classes
{

    public static class Verify
    {
        public static bool IsValidEmail(in string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                StreamFileWriteHelper.WriteLogErrorFile("Mail is not correct format");
                return false;
            }
        }
        public static bool IsDataContainsOnlyLetters(in string data)
        {
            return Regex.IsMatch(data, @"^[a-zA-Z ,-]+$") && (!string.IsNullOrWhiteSpace(data));
        }

        public static bool IsDataContainsOnlyIntegers(in string data)
        {
            return data.All(char.IsDigit) && (!string.IsNullOrWhiteSpace(data));
        }

        public static bool IsPhoneNumberCorrectFormat(in string data)
        {
            return (!string.IsNullOrWhiteSpace(data)) && Regex.IsMatch(data, "^[0-9 +()-]+$");
        }
        public static bool IsPersonAgeMoreThanSeventeen(in string age)
        {
            return IsDataContainsOnlyIntegers(age) && int.Parse(age) > 17;
        }

        public static bool IsDataCorrectFormat(in string userName)
        {
            return Regex.IsMatch(userName, @"^[a-zA-Z0-9 _+~!@$^&=#%*,.(){}:;?/-]+$") && (!string.IsNullOrWhiteSpace(userName));
        }

        public static bool IsPasswordStrong(in string password)
        {
            return password.Length > 7 && (!string.IsNullOrWhiteSpace(password));
        }

        public static bool IsNumberPositiveInteger(in string number)
        {
            return IsDataContainsOnlyIntegers(number) && int.Parse(number) >= 0;
        }

        public static bool IsEndTimeBiggerThanOrEqualFromStartTime(in DateTime start, in DateTime end)
        {
            return end >= start;
        }

        public static bool IsUserNameViewedAnnouncement(in List<string> alreadyViewedUserNames, in string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                if (alreadyViewedUserNames != null)
                {
                    foreach (var name in alreadyViewedUserNames)
                    {
                        if (name == userName)
                            return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public static bool IsChoiceCorrect(in string choice, in int maxChoice)
        {
            return (!string.IsNullOrWhiteSpace(choice)) && IsDataContainsOnlyIntegers(choice)
                && int.Parse(choice) <= maxChoice && int.Parse(choice) > 0;
        }
    }
}
