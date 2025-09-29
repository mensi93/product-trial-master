using Alten.ProductMaster.Application.Specifications.Members;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Application.Members.CreateMember
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result>
    {
        private readonly IRepository<Member> _memberRepository;
        public CreateMemberCommandHandler(IRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<Result> Handle(CreateMemberCommand command)
        {
            //todo : passwordhash
            var memberResult = Member.Create(command.username, command.firstName, command.email, command.password);

            if (memberResult.IsFailure)
            {
                return Result.Failure(memberResult.Error);
            }


            if (await _memberRepository.AnyAsync(new MemberByUserNameSpecification(command.username)))
            {
                return Result.Failure<Member>(DomainErrors.Member.UserNameAlreadyInUse);
            }

            if (await _memberRepository.AnyAsync(new MemberByEmailSpecification(command.email)))
            {
                return Result.Failure<Member>(DomainErrors.Member.EmailAlreadyInUse);
            }

            await _memberRepository.AddAsync(memberResult.Value);

            return Result.Success();
        }
    }
}
