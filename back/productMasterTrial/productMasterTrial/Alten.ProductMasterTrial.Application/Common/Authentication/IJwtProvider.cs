using Alten.ProductMasterTrial.Domain.Entities;

namespace Alten.ProductMaster.Application.Common.Authentication
{
    public interface IJwtProvider
    {
        string GenerateToken(Member member);
    }
}
