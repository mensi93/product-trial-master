using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Members
{
    public class MemberByEmailAndPassword : Specification<Member>
    {
        public MemberByEmailAndPassword(string email,string password)
        {
            Query.AsNoTracking().Where(u => u.Email.Value == email && u.Password == password);
        }
    }
}
