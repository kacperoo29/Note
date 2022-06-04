using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PNote.Core
{
    public interface IUserService
    {
        Task<List<NoteUser>> GetUsers(CancellationToken cancellationToken = default);
        Task<NoteUser> AddUser(NoteUser user, CancellationToken cancellationToken = default);
    }
}
