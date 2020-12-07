using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Publicon.Infrastructure.Commands.Models.Publication;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Queries.Models.Publication;
using System;
using System.Threading.Tasks;

namespace Publicon.Api.Controllers
{

    [Route("api/publications/")]
    [ApiController]
    public class PublicationsController : AbstractController
    {
        public PublicationsController(IMediator mediator) : base(mediator) { }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PublicationDetailsDTO>> CreatePublication([FromForm] CreatePublicationCommand command)
        {
            var publication = await Handle(command);
            return CreatedAtRoute("GetPublication",
                new { publicationId = publication.Id }, publication);
        }

        //check if publicationfields are mapped!!
        //set in validator max 50 MB
        [Authorize]
        [HttpGet("{publicationId}", Name = "GetPublication")]
        public async Task<ActionResult<PublicationDetailsDTO>> GetPublication(Guid publicationId)
        {
            var publication = await Handle(new GetPublicationQuery(publicationId));
            return Ok(publication);
        }

        [Authorize]
        [HttpGet("{publicationId}/download")]
        public async Task<ActionResult> DownloadPublication(Guid publicationId)
        {
            var blob = await Handle(new DownloadPublicationQuery(publicationId));
            return File(blob.Content, blob.ContentType, blob.FileName);
        }
        
        //search by title, filter by categories, return paginated list
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PublicationPaginatedListDTO>> GetPublications([FromQuery] GetPublicationsQuery query)
        {
            var publications = await Handle(query);
            return Ok(publications);
        }

        [Authorize]
        [HttpGet("excel-simple")]
        public async Task<ActionResult> GetPublicationsRaport([FromQuery] GetExcelSimpleRaportQuery query)
        {
            var stream = await Handle(query);
            return File(stream, "application/csv", "report-" + DateTime.UtcNow.ToBinary().ToString()+".csv");
        }

        [Authorize]
        [HttpGet("excel-detailed")]
        public async Task<ActionResult> GetPublicationsDetaildRaport([FromQuery] GetExcelDetailedRaportQuery query)
        {
            var stream = await Handle(query);
            return File(stream, "application/csv", "report-" + DateTime.UtcNow.ToBinary().ToString() + ".csv");
        }

        [Authorize("MustBeOwnerOrAdmin")]
        [HttpPut("{publicationId}")]
        public async Task<ActionResult> EditPublication(Guid publicationId, EditPublicationCommand command)
        {
            await Handle(command.SetPublicationId(publicationId));
            return NoContent();
        }

        [Authorize("MustBeOwnerOrAdmin")]
        [HttpPut("{publicationId}/upload")]
        public async Task<ActionResult> UpsertPublicationFile(Guid publicationId, [FromForm] UpsertPublicationFileCommand command)
        {
            await Handle(command.SetPublicationId(publicationId));
            return NoContent();
        }

        [Authorize("MustBeOwnerOrAdmin")]
        [HttpDelete("{publicationId}/fields/{fieldId}")]
        public async Task<ActionResult> DeletePublicationField(Guid publicationId, Guid fieldId)
        {
            await Handle(new DeletePublicationFieldCommand(publicationId, fieldId));
            return NoContent();
        }

        [Authorize("MustBeOwnerOrAdmin")]
        [HttpDelete("{publicationId}")]
        public async Task<ActionResult> DeletePublication(Guid publicationId)
        {
            await Handle(new DeletePublicationCommand(publicationId));
            return NoContent();
        }
        
        [Authorize("MustBeOwnerOrAdmin")]
        [HttpDelete("{publicationId}/delete-file")]
        public async Task<ActionResult> DeletePublicationFile(Guid publicationId)
        {
            await Handle(new DeletePublicationFileCommand(publicationId));
            return NoContent();
        }

        [Authorize]
        [HttpGet("pdf-data")]
        public async Task<ActionResult> GetDetailedPublications([FromQuery] GetDetailedPublicationsQuery query)
        {
            var publications = await Handle(query);
            return Ok(publications);
        }
    }
}