using Alten.ProductMaster.SharedKirnel;
using Alten.ProductMasterTrial.Domain.Errors;
using System.Text.RegularExpressions;

namespace Alten.ProductMaster.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public const int maxLength = 255;
        public string Value { get; private set; }

        private Email()
        {

        }

        private Email(string email)
        {
            Value = email;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result.Failure<Email>(DomainErrors.EmailErrors.EmptyEmail);
            }

            if (email.Length > maxLength)
            {
                return Result.Failure<Email>(DomainErrors.EmailErrors.EmailTooLong);
            }

            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailRegex))
            {
                return Result.Failure<Email>(DomainErrors.EmailErrors.EmailInvalidFormat);
            }


            return new Email(email);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
