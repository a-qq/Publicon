using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Publicon.API.Binders.BodyandRoute;
using Publicon.Infrastructure.Commands.Models.Category;
using Publicon.Infrastructure.DTOs;
using Publicon.Infrastructure.Queries.Models.Category;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publicon.Api.Controllers
{
    [Route("api/categories/")]
    [ApiController]
    public class CategoriesController : AbstractController
    {
        public CategoriesController(IMediator mediator) : base(mediator) { }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryCommand command)
        {
            var category = await Handle(command);
            return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, category);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories([FromQuery] GetCategoriesQuery query)
        {
            var categories = await Handle(query);
            return Ok(categories);
        }

        [Authorize]
        [HttpGet("{categoryId}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(Guid categoryId)
        {
            var category = await Handle(new GetCategoryQuery(categoryId));
            return Ok(category);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{categoryId}")]
        public async Task<ActionResult> EditCategory(Guid categoryId, [FromBody] EditCategoryCommand command)
        {
            await Handle(command.SetCategoryId(categoryId));
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{categoryId}/fields/{fieldId}")]
        public async Task<ActionResult> EditField(Guid categoryId, Guid fieldId, [FromBody] EditFieldCommand command)
        {
            await Handle(command.SetIds(categoryId, fieldId));
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("{categoryId}/fields")]
        public async Task<ActionResult<FieldDTO>> CreateField(Guid categoryId, CreateFieldCommand command)
        {

           var field = await Handle(command.SetCategoryId(categoryId));
           return Ok(field);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{categoryId}/fields/{fieldId}")]
        public async Task<ActionResult> DeleteField(Guid categoryId, Guid fieldId)
        {
            await Handle(new DeleteFieldCommand(categoryId, fieldId));
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{categoryId}/soft-delete")]
        public async Task<ActionResult> SoftDeleteCategory(Guid categoryId)
        {
            await Handle(new SoftDeleteCategoryCommand(categoryId));
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryId)
        {
            await Handle(new DeleteCategoryCommand(categoryId));
            return NoContent();
        }
        //+add field -> add is required and default value field
        //+safe-delete field -> if no publications have values of it or if it's not required
        //+safe-delete category -> -||-
        //+delete-category and all publications
        //+add publication
        //+get publications (filter, sort and search)
        //download result of it in csv
        //+get single publication
        //+download publication
        //edit publication
        //edit publication field
        //remove publication field if's not required
        //delete publication


    }
}
