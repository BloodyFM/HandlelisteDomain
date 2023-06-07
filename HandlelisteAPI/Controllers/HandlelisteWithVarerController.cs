using HandlelisteAPI.Core.DataLogic;
using HandlelisteAPI.Core.DTO;
using HandlelisteDomain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HandlelisteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlelisteWithVarerController : ControllerBase
    {
        private readonly HandlelisteWithVarerLogic _hwvl;

        public HandlelisteWithVarerController(HandlelisteWithVarerLogic hwvl)
        {
            _hwvl = hwvl;
        }

        // GET: api/HandlelisteWithVarer/VareInstance/5
        [HttpGet("VareInstance/{handlelisteId}")]
        public async Task<ActionResult<VareInstanceDTO>> GetVareInstance(int handlelisteId, int vareId)
        {
            var vareInstanceDTO = await _hwvl.GetVareInstanceByIdDTO(handlelisteId, vareId);
            if (vareInstanceDTO == null)
            {
                return NotFound();
            }
            return vareInstanceDTO;
        }

        // POST: api/HandlelisteWithVarer/VareInstance/5
        [HttpPost("VareInstance/{handlelisteId}")]
        public async Task<ActionResult<VareInstance>> PostVareInstace(int handlelisteId, VareInstanceDTO vareInstanceDTO)
        {
            if (_hwvl.VareInstanceIsNull())
            {
                return Problem("Entity set 'PubContext.Vareinstance'  is null.");
            }
            var vareInstance = _hwvl.VareInstanceFromDTO(handlelisteId, vareInstanceDTO);
            if (_hwvl.VareInstanceExists(vareInstance.HandlelisteId, vareInstance.VareId))
            {
                return CreatedAtAction("GetVareInstance", new { handlelisteId = vareInstance.HandlelisteId, vareId = vareInstance.VareId }, _hwvl.VareInstanceToDTONoName(vareInstance));
            }
            if (!_hwvl.VareExists(vareInstance.VareId))
            {
                return BadRequest();
            }
            _hwvl.AddVareInstance(vareInstance);
            await _hwvl.SaveChangesAsync();

            return CreatedAtAction("GetVareInstance", new { handlelisteId = vareInstance.HandlelisteId, vareId = vareInstance.VareId }, _hwvl.VareInstanceToDTONoName(vareInstance));
        }

        // PUT: api/HandlelisteWithVarer/VareInstance/5
        [HttpPut("VareInstance/{handlelisteId}")]
        public async Task<IActionResult> PutVareInstance(int handlelisteId, VareInstanceDTO vareInstanceDTO)
        {
            var vareInstance = _hwvl.VareInstanceFromDTO(handlelisteId ,vareInstanceDTO);
            _hwvl.SetModifiedVareInstanceState(vareInstance);

            try
            {
                await _hwvl.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_hwvl.VareInstanceExists(handlelisteId, vareInstanceDTO.VareId))
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

        // DELETE: api/HandlelisteWithVarer/VareInstance/5
        [HttpDelete("VareInstance/{handlelisteId}")]
        public async Task<IActionResult> DeleteVareInstance(int handlelisteId, int vareId)
        {
            if (_hwvl.VareInstanceIsNull())
            {
                return Problem("Entity set 'PubContext.Vareinstance'  is null.");
            }
            var vareInstance = await _hwvl.GetVareInstanceByHandlelisteIdAndVareId(handlelisteId, vareId);
            if (vareInstance == null)
            {
                return NotFound();
            }

            _hwvl.RemoveVareInstance(vareInstance);
            await _hwvl.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/HandlelisteWithVarer/5
        [HttpGet("{handlelisteId}")]
        public async Task<ActionResult<HandlelisteWithVarerDTO>> GetHandleliste(int handlelisteId)
        {
            if (_hwvl.HandlelisterIsNull())
            {
                return Problem("Entity set 'HandlelisteContext.Handlelister'  is null.");
            }
            var handleliste = await _hwvl.GetHandlelisteIncludeVarer(handlelisteId);
            if (handleliste == null)
            {
                return NotFound();
            }
            return _hwvl.HandlelisteWithVarerToDTO(handleliste);
        }

        // PUT: api/HandlelisteWithVarer/IsCollected/5
        [HttpPut("IsCollected/{id}")]
        public async Task<IActionResult> PutIsCollected(int id, int vareId, bool isCollected)
        {
            var vareInstances = await _hwvl.FindVareInstancesByHandlelisteId(id);
            var vareInstance = new VareInstance();

            if (vareInstances.IsNullOrEmpty())
            {
                return BadRequest();
            }

            for (int i = 0; i < vareInstances.Count; i++)
            {
                var instance = vareInstances[i];
                if (instance.VareId == vareId)
                {
                    instance.IsCollected = isCollected;
                    vareInstance = instance;
                    _hwvl.SaveChanges();
                }
            }
            //var vareInstace = new VareInstance() { VareId = vareId, HandlelisteId = id, Mengde = 1, IsCollected = isCollected };
            _hwvl.SetModifiedVareInstanceState(vareInstance);

            try
            {
                await _hwvl.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_hwvl.HandlelisteExists(id))
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

        // POST: api/HandlelisteWithVarer
        [HttpPost]
        public async Task<ActionResult<Handleliste>> PostHandleliste(HandlelisteWithVarerDTO handleliste)
        {
            if (_hwvl.HandlelisterIsNull())
            {
                return Problem("Entity set 'HandlelisteContext.Handlelister'  is null.");
            }
            var newHandleliste = _hwvl.AddHandleliste(_hwvl.HandlelisteWithVarerFromDTO(handleliste));
            await _hwvl.SaveChangesAsync();
            var id = newHandleliste.Entity.HandlelisteId;
            handleliste.HandlelisteId = id;

            return CreatedAtAction("GetHandleliste", new { id }, handleliste);
        }

        // DELETE: api/HandlelisteWithVarer/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteHandleliste(int id)
        //{
        //    if (_hwvl.HandlelisterIsNull())
        //    {
        //        return Problem("Entity set 'HandlelisteContext.Handlelister'  is null.");
        //    }
        //    var handleliste = await _hwvl.FindHandlelisteById(id);
        //    if (handleliste == null)
        //    {
        //        return NotFound();
        //    }

        //    _hwvl.RemoveHandleliste(handleliste);
        //    await _hwvl.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
