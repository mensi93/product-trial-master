using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Members
{
    public class MemberByUserNameSpecification : Specification<Member>
    {
        public MemberByUserNameSpecification(string userName)
        {
            Query.Where(x => x.UserName.Value == userName);
        }
    }
}
