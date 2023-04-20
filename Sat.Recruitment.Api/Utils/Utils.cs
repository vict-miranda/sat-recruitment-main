using System;

namespace Sat.Recruitment.Api.Utils
{
    public static class Utils
    {
        public static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        public static decimal SetMoneyByUserType(string userType, decimal newUserMoney)
        {
            decimal money = 0;
            switch (userType)
            {
                case "Normal":
                    if (newUserMoney > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        money = newUserMoney + (newUserMoney * percentage);
                    }
                    else if (newUserMoney < 100 && newUserMoney > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        money = newUserMoney + (newUserMoney * percentage);
                    }
                    break;
                case "SuperUser":
                    if (newUserMoney > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        money = newUserMoney + (newUserMoney * percentage);
                    }
                    break;
                case "Premium":
                    if (newUserMoney > 100)
                        money = newUserMoney + (newUserMoney * 2);
                    break;
                default:
                    break;
            }
            return money;
        }
    }
}
