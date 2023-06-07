using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HandlelisteDomain;
using HandlelisteAPI.DataLogic;

namespace HandlelisteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlelisteController : ControllerBase
    {
        private readonly HandlelisteLogic _hl;

        public HandlelisteController(HandlelisteLogic hl)
        {
            _hl = hl;
        }

        private static HandlelisteDTO HandlelisteToDTO(Handleliste handleliste)
        {
            return new HandlelisteDTO
            {
                HandlelisteId = handleliste.HandlelisteId,
                UserId = handleliste.UserId,
                HandlelisteName = handleliste.HandlelisteName,
            };
        }

        // GET: api/Handleliste/5
        [HttpGet("/{id}")]
        public async Task<ActionResult<HandlelisteDTO>> GetHandleliste(int id)
        {
            var handlelisteDTO = await _hl.GetHandlelisteByIdDTO(id);
            if (handlelisteDTO == null)
            {
                return NotFound();
            }
            return handlelisteDTO;
        }

        // GET: api/Handleliste/byUserId/
        [HttpGet("byUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<HandlelisteDTO>>> GetHandlelisterByUser(string userId)
        {
            if (_hl.HandlelisterIsNull())
            {
                return Problem("Entity set 'PubContext.Handlelister'  is null.");
            }

            if (HttpContext.Request.Method != "GET")
            {
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
            }

            return await _hl.GetHandlelisterByUserIdDTO(userId);
        }

        // PUT: api/Handleliste/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHandleliste(int id, HandlelisteDTO handlelisteDTO)
        {
            if (id != handlelisteDTO.HandlelisteId)
            {
                return BadRequest();
            }

            var handleliste = HandlelisteFromDTO(handlelisteDTO);
            _hl.SetModifiedHandlelisteState(handleliste);

            try
            {
                await _hl.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_hl.HandlelisteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/handleliste
        [HttpPost]
        public async Task<ActionResult<Handleliste>> PostHandleliste(HandlelisteDTO handlelisteDTO)
        {
            if (_hl.HandlelisterIsNull())
            {
                return Problem("Entity set 'PubContext.Handlelister'  is null.");
            }
            var handleliste = HandlelisteFromDTO(handlelisteDTO);

            _hl.AddHandleliste(handleliste);
            await _hl.SaveChangesAsync();

            return CreatedAtAction("GetHandleliste", new { id = handleliste.HandlelisteId}, HandlelisteToDTO(handleliste));
        }

        private static Handleliste HandlelisteFromDTO(HandlelisteDTO handlelisteDTO)
        {
            return new Handleliste()
            {
                HandlelisteId = handlelisteDTO.HandlelisteId,
                UserId = handlelisteDTO.UserId,
                HandlelisteName = handlelisteDTO.HandlelisteName,
            };
        }

        // DELETE: api/Handleliste/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHandleliste(int id)
        {
            if (_hl.HandlelisterIsNull())
            {
                return NotFound();
            }
            var handleliste = await _hl.GetHandlelisteById(id);
            if (handleliste == null)
            {
                return NotFound();
            }

            _hl.RemoveHandleliste(handleliste);
            await _hl.SaveChangesAsync();

            return NoContent();
        }
    }
}
