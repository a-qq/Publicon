using MediatR;
using Microsoft.AspNetCore.Http;
using Publicon.Infrastructure.DTOs;
using System;
using System.Collections.Generic;

namespace Publicon.Infrastructure.Commands.Models.Publication
{
    public class CreatePublicationCommand : Command, IRequest<PublicationDetailsDTO>
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationTime { get; set; }
        public IEnumerable<PublicationFieldToManipulateDTO> PublicationFields { get; set; }
        public IFormFile File { get; set; }

        //public CreatePublicationCommand(PublicationToAddDTO publicationData, IFormFile file)
        //{
        //    CategoryId = publicationData.CategoryId;
        //    Title = publicationData.Title;
        //    PublicationTime = publicationData.PublicationTime;
        //    PublicationFields = publicationData.PublicationFields;
        //    File = file;
        //}

    }
}
