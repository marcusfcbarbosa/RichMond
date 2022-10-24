using Richmond.Domain.Data;
using Richmond.Domain.Entities;

namespace Richmond.Domain.Repositories
{
    public interface IAccountRepository
    {
    }
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly RichMondContext _context;
        public AccountRepository(RichMondContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
