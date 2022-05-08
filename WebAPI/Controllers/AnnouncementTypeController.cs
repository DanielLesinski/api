using Application.Dto.Announcements;
using Application.Interfaces.Announcements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AnnouncementTypeController : ControllerBase
    {
        IAnnouncementTypeService announcementTypeService;
        public AnnouncementTypeController(IAnnouncementTypeService announcementTypeService)
        {
            this.announcementTypeService = announcementTypeService;
        }

        [SwaggerOperation(Summary = "Get all announcement types (only admin)")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAnnouncementTypes()
        {
            var types = await announcementTypeService.GetAllAnnouncementTypes();
            if (types == null)
                return NotFound();
            return Ok(types);
        }

        [SwaggerOperation(Summary = "Get info about announcement type (only admin)")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAnnouncementType(int id)
        {
            var type = await announcementTypeService.GetAnnouncementTypeById(id);
            if (type == null)
                return NotFound();
            return Ok(type);
        }

        [SwaggerOperation(Summary = "Add a new announcement type (only admin)")]
        [HttpPost("add")]
        public async Task<IActionResult> AddAnnouncementType(AddAnnouncementTypeDto addType)
        {
            var newType = await announcementTypeService.AddNewAnnouncementType(addType);
            return Ok(newType);
        }


        [SwaggerOperation(Summary = "Update an announcement type (only admin)")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, UpdateAnnouncementTypeDto update)
        {
            await announcementTypeService.UpdateAnnouncementType(id, update);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an announcement type (only admin)")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAnnouncementType(int id)
        {
            await announcementTypeService.DeleteAnnouncementType(id);
            return NoContent();
        }
    }
}
