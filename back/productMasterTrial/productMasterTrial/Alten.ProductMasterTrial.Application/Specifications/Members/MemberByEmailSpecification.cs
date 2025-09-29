using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Members
{
    public class MemberByEmailSpecification : Specification<Member>
    {
        public MemberByEmailSpecification(string email)
        {
            Query.AsNoTracking().Where(u => u.Email.Value == email);
        }
    }
}
