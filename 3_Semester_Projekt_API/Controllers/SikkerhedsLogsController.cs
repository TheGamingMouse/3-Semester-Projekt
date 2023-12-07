using Microsoft.AspNetCore.Mvc;
using SikkerhedsLogRepositoryLib;

namespace _3_Semester_Projekt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SikkerhedsLogsController : Controller
    {
        private SikkerhedsLogRepositoryDB _data;
        public SikkerhedsLogsController(SikkerhedsLogRepositoryDB data)
        {
            _data = data;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get()
        {
            IEnumerable<SikkerhedsLog> sl = _data.Get();

            if (sl.ToList().Count == 0)
            {
                return NoContent();
            }
            return Ok(sl);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] SikkerhedsLog value)
        {
            SikkerhedsLog sl = _data.Add(value);
            return Created($"api/sikkerhedsLogs/{sl.Id}", sl);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete(int id)
        {
            try
            {
                SikkerhedsLog sl = _data.Delete(id);
                return Ok(sl);
            }
            catch(KeyNotFoundException knfe)
            {
                return NotFound(knfe);
            }
            {

            }
        }
    }
}