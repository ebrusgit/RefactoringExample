using System;

namespace LegacyApp
{
    public class UserService
    {
        private static readonly int _VerificationAge = 21;
        private IClientRepository _clientRepository;
        private Verificator _verificator;
        private CreditLimitChecker _creditLimitChecker;
        
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _verificator = new Verificator();
            _creditLimitChecker = new CreditLimitChecker();
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (_verificator.NameVerification(firstName, lastName) 
                || _verificator.EmialVerification(email) || _verificator.AgeVerification(dateOfBirth, _VerificationAge))
            {
                return false;
            } 
            
            var client = _clientRepository.GetById(clientId);

            var user = new User(client, dateOfBirth, email, firstName, lastName);
            
            UserType.UserLimitSetter(user);
            
            if (_creditLimitChecker.GotALimit(user) && _creditLimitChecker.GotLimitMoreThan500(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}