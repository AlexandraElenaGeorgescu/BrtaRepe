using Microsoft.AspNetCore.Mvc;
using NewsAPI.Models;
using NewsAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace NewsAPI.Controllers
{
    /// <summary>
    /// Summary comment for Announcements Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        IAnnouncementCollectionService _announcementCollectionService;

        public AnnouncementsController(IAnnouncementCollectionService announcementCollectionService)
        {
            _announcementCollectionService = announcementCollectionService ?? throw new ArgumentNullException(nameof(AnnouncementsCollectionService));
        }

        /// <summary>
        /// Get all announcements
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAnnouncements()
        {
            List<Announcement> announcements = await _announcementCollectionService.GetAll();
            return Ok(announcements);
        }


        ///<summary>
        ///Get an announcement by id
        ///</summary>
        ///<param name="id">Introduce the ID</param>
        ///<returns></returns>
        [HttpGet("{id}")]
        public async Task< IActionResult> GetAnnouncementById(Guid id)
        {
            var announcement =await _announcementCollectionService.Get(id);

            if(announcement == null)
            {
                return NotFound();
            }

            return Ok(announcement);
        }


        /// <summary>
        /// Create a new announcement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] Announcement announcement)
        {
            if (announcement == null)
            {
                return BadRequest("Announcement can't be null");
            }
            var isCreated = await _announcementCollectionService.Create(announcement);

            if (!(isCreated))
            {
                return BadRequest();
            }
            
            return CreatedAtAction(nameof(GetAnnouncementById), new {id = announcement.Id}, announcement);
        }

        /// <summary>
        /// Update an announcement
        /// </summary>
        /// <param name="id"></param>
        /// <param name="announcement"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] Announcement announcement, [Required] Guid id)
        {
            var isUpdated = await _announcementCollectionService.Update(id, announcement);
            if (!isUpdated)
            {
                return NotFound("The announcement was not found!");
            }

            return Ok(announcement);
        }

        /// <summary>
        /// Delete an announcement by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            var isDeleted = await _announcementCollectionService.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Announcement was not found");
            }
            return Ok(isDeleted);
        }

        /// <summary>
        /// Get announcements by category Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("getByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetAnnouncementsByCategoryId(string categoryId)
        {
            var announcements = await  _announcementCollectionService.GetAnnouncementsByCategoryId(categoryId);

            return Ok(announcements);
        }
    }
}
