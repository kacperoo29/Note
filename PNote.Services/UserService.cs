using Microsoft.EntityFrameworkCore;
using PNote.Core;

namespace PNote.Services
{
    public class UserService : IUserService
    {
        private readonly PNoteDbContext _context;
        private DbSet<NoteUser> Users { get => _context.Set<NoteUser>(); }

        public NoteUser? CurrentUser { get; private set; }

        public UserService(PNoteDbContext context)
        {
            this._context = context;
        }

        public async Task<List<NoteUser>> GetUsers(CancellationToken cancellationToken = default)
        {
            return await Users.ToListAsync(cancellationToken);
        }

        public async Task<NoteUser> AddUser(NoteUser user, CancellationToken cancellationToken = default)
        {
            var entity = (await Users.AddAsync(user, cancellationToken)).Entity;
            Console.WriteLine(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public NoteUser SetCurrentUser(NoteUser user)
        {
            if (this.CurrentUser == null)
                this.CurrentUser = user;

            return this.CurrentUser;
        }
    }
}
