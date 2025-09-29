using Alten.ProductMaster.SharedKirnel;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Domain.ValueObjects
{
    public sealed class UserName : ValueObject
    {

        public const int MaxLength = 50;
        public string Value { get; private set; }

        private UserName()
        {
            
        }

        private UserName(string value)
        {
            Value = value;
        }

        public static Result<UserName> Create(string username)
        {
            if (string.IsNullOrEmpty(username)) 
            {
                return Result.Failure<UserName>(DomainErrors.UserNameErrors.EmptyUserName);
            }

            if (username.Length > MaxLength) 
            {
                return Result.Failure<UserName>(DomainErrors.UserNameErrors.UserNameTooLong);
            }

            return new UserName(username);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
