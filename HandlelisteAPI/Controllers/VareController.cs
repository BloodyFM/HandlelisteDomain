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
                foreach (var handleliste in matchingHandlelister)
                    foreach (var matchingVare in handleliste.Varer)
                    {
                        //if (vare.VareId == matchingVare.VareId) continue; //redundant, next line would cover this to
                        bool skip = false;
                        for (int i = 0; i < currentHandleliste.Varer.Count; i++)
                        {
                            if (matchingVare.VareId == currentHandleliste.Varer[i].VareId) skip = true;
                        }
                        if (skip) continue;
                        bool exists = false;
                        for (var i = 0; i < vareScores.Count; i++)
                        {
                            if (vareScores[i].vareId == matchingVare.VareId)
                            {
                                vareScores[i].points++;
                                exists = true;
                            }
                        }
                        if (!exists)
                            vareScores.Add(new VareScore(matchingVare.VareId, 1));
                    }
            }

            // find the highest score
            var bestVare = new VareScore(0, 0);
            if (vareScores.Count > 0)
            {
                bestVare = vareScores[0];
            }
            for (var i = 1; i < vareScores.Count; i++)
            {
                if (vareScores[i].points > bestVare.points) bestVare = vareScores[i];
            }

            if (vareScores.Count == 0)
            {
                return null;
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
