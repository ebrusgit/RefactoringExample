using System;
using System.Collections.Generic;

namespace LegacyApp;

public class UserType
{
    public static Dictionary<string, ICreditLimitSetter> SetterClasses = new Dictionary<string, ICreditLimitSetter>
    {
        { "NormalClient", new NormalClient() },
        { "ImportantClient", new ImportantClient() },
        { "VeryImportantClient", new VeryImportantClient() }
    };

    private static IUserCreditService _userCreditService;

    static UserType()
    {
        _userCreditService = new UserCreditService();
    }
    public static void UserLimitSetter(User user)
    {
        if (SetterClasses.TryGetValue(user.Client.Type, out ICreditLimitSetter value))
        {
            value.SetLimit(user, _userCreditService);
        }
        else
        {
            new NormalClient().SetLimit(user, _userCreditService);
        }
    }
}

class NormalClient : ICreditLimitSetter
{
    public void SetLimit(User user, IUserCreditService _userCreditService)
    {
        user.HasCreditLimit = true;
        user.CreditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
    }
}
class ImportantClient : ICreditLimitSetter
{
    public void SetLimit(User user, IUserCreditService _userCreditService)
    {
        user.CreditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth) * 2;
    }
}
class VeryImportantClient : ICreditLimitSetter
{
    public void SetLimit(User user, IUserCreditService _userCreditService)
    {
        user.HasCreditLimit = false;
    }
}