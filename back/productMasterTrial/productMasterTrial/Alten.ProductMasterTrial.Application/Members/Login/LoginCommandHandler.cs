using Alten.ProductMaster.Application.Common.Authentication;
using Alten.ProductMaster.Application.Specifications.Members;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Application.Members.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IReadRepository<Member> _memberRepository;
        public LoginCommandHandler(
            IReadRepository<Member> memberRepository,
            IJwtProvider jwtProvider)
        {
            _memberRepository = memberRepository;
            _jwtProvider = jwtProvider; 
        }
        public async Task<Result<string>> Handle(LoginCommand request)
        {

            var memberByEmailAndPassword = new MemberByEmailAndPassword(request.email, request.password);

            var member = await _memberRepository.FirstOrDefaultAsync(memberByEmailAndPassword);

            if (member == null) 
            {
                return Result.Failure<string>(DomainErrors.Member.InvalidCredentials);
            }

            var token = _jwtProvider.GenerateToken(member);

            return token;
        }
    }
}
