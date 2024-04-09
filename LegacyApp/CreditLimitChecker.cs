namespace LegacyApp;

public class CreditLimitChecker
{
    public bool GotALimit(User user)
    {
        return user.HasCreditLimit;
    }

    public bool GotLimitMoreThan500(User user)
    {
        return user.CreditLimit < 500;
    }
}