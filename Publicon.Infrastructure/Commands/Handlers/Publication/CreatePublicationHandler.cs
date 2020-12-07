using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Publicon.Core.Entities.Concrete;
using Publicon.Core.Exceptions;
using Publicon.Core.Repositories.Abstract;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Managers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Publicon.Infrastructure.Commands.Handlers.Publication
{
    public class CreatePublicationHandler : IRequestHandler<CreatePublicationCommand, PublicationDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly IBlobManager _blobManager;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreatePublicationHandler(
            IMapper mapper,
            IBlobManager blobManager,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _blobManager = blobManager;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<PublicationDetailsDTO> Handle(CreatePublicationCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.Category)));

            if (category.IsArchived)
                throw new PubliconException(ErrorCode.CategoryArchived);

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if(user == null)
                throw new PubliconException(ErrorCode.EntityNotFound(typeof(Core.Entities.Concrete.User)));

            var fields = category.Fields;
            if (request.PublicationFields != null && request.PublicationFields.Any())
            {
                foreach (var field in request.PublicationFields)
                {
                    if (!fields.Any(f => f.Id == field.FieldId))
                        throw new PubliconException(ErrorCode.EntityNotFound(typeof(Field)));

                }
            }

            var requiredFields = fields.Where(f => f.IsRequired);
            if (requiredFields.Any() && (request.PublicationFields == null || !requiredFields.All(f => request.PublicationFields.Any(pf => pf.FieldId == f.Id))))
                throw new PubliconException(ErrorCode.NotAllRequiredFieldsPresent, "Fill all the required fields!");


            var publication = _mapper.Map<Core.Entities.Concrete.Publication>(request);
            var publicationFields = _mapper.Map<IEnumerable<PublicationField>>(request.PublicationFields);

            publication.AddPublicationFields(publicationFields);
            publication.SetUser(user);

            category.AddPublication(publication);
            await _categoryRepository.SaveChangesAsync();

            if (!string.IsNullOrWhiteSpace(publication.FileName))
                await _blobManager.UploadFileBlobAsync(request.File, publication.FileName);

            
            var publicationDTO = _mapper.Map<PublicationDetailsDTO>(publication);
            return publicationDTO;
        }
    }
}
