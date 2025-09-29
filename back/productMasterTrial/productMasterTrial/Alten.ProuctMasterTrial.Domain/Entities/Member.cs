using Alten.ProductMaster.Domain.ValueObjects;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Domain.Entities
{
    public sealed class Member
    {
        public Guid Id { get; private set; }
        public UserName UserName { get; private set; }

        public string FirstName { get; private set; }

        public Email Email { get; private set; }

        public string Password { get; private set; }

        private Member()
        {

        }

        private Member(UserName userName, string firstName, Email email, string password)
        {
            UserName = userName;
            FirstName = firstName;
            Email = email;
            Password = password;
        }

        public static Result<Member> Create(string userName,string firstName,string email,string password)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return Result.Failure<Member>(DomainErrors.Member.FirstNameEmpty);
            }

            var userNameResult = UserName.Create(userName);
            if (userNameResult.IsFailure)
                return Result.Failure<Member>(userNameResult.Error);

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return Result.Failure<Member>(emailResult.Error);


            if (string.IsNullOrEmpty(password))
            {
                return Result.Failure<Member>(DomainErrors.Member.PasswordEmpty);
            }

            return new Member(userNameResult.Value, firstName, emailResult.Value, password);
        }
    }
}
