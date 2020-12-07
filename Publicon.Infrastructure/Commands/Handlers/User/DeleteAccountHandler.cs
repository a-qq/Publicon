using MediatR;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.User
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteAccountHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.User)));

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
