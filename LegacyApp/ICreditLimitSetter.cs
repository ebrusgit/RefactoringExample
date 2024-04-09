using System;

namespace LegacyApp;

public interface ICreditLimitSetter
{
    void SetLimit(User user, IUserCreditService _userCreditService);
}