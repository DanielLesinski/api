using Application.Dto.Announcements;
using Application.Interfaces.Announcements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }

        [SwaggerOperation(Summary = "Get all announcement")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAnnouncement()
        {
            var announcement = await announcementService.GetAllAnnouncement();
            if (announcement == null)
                return NotFound();
            return Ok(announcement);
        }

        [SwaggerOperation(Summary = "Get info about announcement")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAnnouncement(int id)
        {
            var announcement = await announcementService.GetAnnouncementById(id);
            if (announcement == null)
                return NotFound();
            return Ok(announcement);
        }

        [SwaggerOperation(Summary = "Search an announcement")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchAnnouncement(string info)
        {
            var announcement = await announcementService.SearchbyKeyword(info);
            if (announcement.Count == 0)
                return NotFound();
            return Ok(announcement);
        }

        [SwaggerOperation(Summary = "Get all user announcements")]
        [HttpGet("getUserAll/{id}")]
        public async Task<IActionResult> GetUserAllAnnouncements(int id)
        {
            var announcements = await announcementService.GetAllUserAnnouncement(id);
            if (announcements.Count == 0)
                return NotFound();
            return Ok(announcements);
        }

        [SwaggerOperation(Summary = "Add a new announcement")]
        [HttpPost("add")]
        public async Task<IActionResult> AddAnnouncement(AddAnnouncementDto addAnnouncement)
        {
            var newAnnouncement = await announcementService.AddNewAnnouncement(addAnnouncement);
            return Ok(newAnnouncement);
        }


        [SwaggerOperation(Summary = "Update an announcement (only owner or admin,moderator)")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, UpdateAnnouncementDto update)
        {
            await announcementService.UpdateAnnouncement(id, update);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete an announcement (only owner or admin,moderator)")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await announcementService.DeleteAnnouncement(id);
            return NoContent();
        }
    }
}
