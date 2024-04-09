using System;

namespace LegacyApp;

public class Verificator
{
    public bool NameVerification(string fname, string lName)
    {
        return string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lName);
    }
    public bool EmialVerification(string email)
    {
        return !email.Contains("@") && !email.Contains(",");
    }

    public bool AgeVerification(DateTime dob, int ageRequired)
    {
        var now = DateTime.Now;
        int age = now.Year - dob.Year;
        if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day)) age--;

        return age < ageRequired;
    }
}