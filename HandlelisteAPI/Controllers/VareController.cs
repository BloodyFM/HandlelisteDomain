using HandlelisteAPI.Core.DataLogic;
using HandlelisteAPI.Core.DTO;
using HandlelisteData;
using HandlelisteDomain;
using Microsoft.AspNetCore.Mvc;

namespace HandlelisteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VareController : ControllerBase
    {
        private readonly VareLogic _vl;

        public VareController(VareLogic vl)
        {
            _vl = vl;
        }

        // GET: api/Varer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VareDTO>>> GetVarer()
        {
            var varerDTOList = await _vl.GetAllVarerDTO();
            return varerDTOList;
        }

        // GET: api/Vare/5
        [HttpGet("{vareId}")]
        public async Task<ActionResult<VareDTO>> GetVare(int vareId)
        {
            var vareDTO = await _vl.GetVareByIdDTO(vareId);
            if (vareDTO == null)
            {
                return NotFound();
            }
        
            return vareDTO;
        }

        public class VareScore
        {
            public int vareId;
            public int points;
            public VareScore(int vareId, int points)
            {
                this.vareId = vareId;
                this.points = points;
            }
        }

        // GET: api/Vare/Recommend/5
        [HttpGet("Recommend/{handlelisteId}")]
        public async Task<ActionResult<VareDTO?>?> GetVareReccomendationsFromSelection(int handlelisteId)
        {
            var currentHandleliste = await _vl.GetHandlelisteIncludingVarerById(handlelisteId);
            if (currentHandleliste == null)
            {
                return NotFound();
            }
            var userId = currentHandleliste.UserId;

            //var vareIds = new List<int>();
            //foreach (var vare in currentHandleliste.Varer) vareIds.Add(vare.VareId);

            var vareScores = new List<VareScore>();
            // count up the scores
            foreach (var vare in currentHandleliste.Varer)
            {
                var matchingHandlelister = await _vl.GetUsersHandlelisterThatContainsVareById(userId, vare.VareId);
                // Go through each handleliste
                foreach (var handleliste in matchingHandlelister)
                    // Check each Vare in the Handleliste
                    foreach (var matchingVare in handleliste.Varer)
                    {
                        if (currentHandleliste.Varer.Contains(matchingVare)) continue;
                        var i = vareScores.FindIndex(vs => vs.vareId == matchingVare.VareId);
                        if (i >= 0)
                            vareScores[i].points++;
                        else
                            vareScores.Add(new VareScore(matchingVare.VareId, 1));
                    }
            }

            // find the highest score
            VareScore bestVare;
            if (vareScores.Count > 0)
            {
                bestVare = vareScores[0];
            } else return null;
            for (var i = 1; i < vareScores.Count; i++)
            {
                if (vareScores[i].points > bestVare.points) bestVare = vareScores[i];
            }

            var returnVare = await _vl.GetVareByIdDTO(bestVare.vareId);
            return returnVare;
        }

        // POST: api/Vare
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vare>> PostVare(VareDTO vareDTO)
        {
            if (_vl.VarerIsNull())
            {
                return Problem("Entity set 'HandlelisteContext.Varer'  is null.");
            }
            var newVare = _vl.VareFromDTO(vareDTO);
            if (_vl.VareExists(newVare.VareId))
                return CreatedAtAction("GetVare", new { vareId = newVare.VareId }, _vl.VareToDTO(newVare));
            _vl.AddNewVare(newVare);
            await _vl.SaveChangesAsync();

            //Calls the action GetVare(int vareId) defined above
            return CreatedAtAction("GetVare", new {vareId = newVare.VareId}, _vl.VareToDTO(newVare));    
        }
    }
}
