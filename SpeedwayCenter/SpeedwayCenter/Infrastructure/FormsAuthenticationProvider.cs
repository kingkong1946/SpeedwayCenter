using System.Web.Security;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;

namespace SpeedwayCenter.Infrastructure
{
    public class FormsAuthenticationProvider : IAuthenticationProvider
    {
        private readonly MembershipProvider _membershipProvider;

        public FormsAuthenticationProvider(MembershipProvider membershipProvider)
        {
            _membershipProvider = membershipProvider;
        }

        public bool Authenticate(string userName, string password)
        {

            bool result = _membershipProvider.ValidateUser(userName, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }
            return result;
        }
    }
}