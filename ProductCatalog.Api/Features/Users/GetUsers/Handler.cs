using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Api.Contracts;
using ProductCatalog.Api.Database;

namespace ProductCatalog.Api.Features.Users.GetUsers
{
    internal sealed class Handler : IRequestHandler<Query, IEnumerable<UserResponse>>
    {
        private readonly AppDbContext _context;

        public Handler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Users.AsNoTracking()
                                       .Select(user => new UserResponse
                                       {
                                           Id = user.Id,
                                           UserName = user.UserName!,
                                           Email = user.Email!,
                                           LockoutEnabled = user.LockoutEnabled
                                       })
                                       .ToListAsync(cancellationToken);
        }
    }
}
