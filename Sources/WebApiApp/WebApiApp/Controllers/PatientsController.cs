using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiApp.Data;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientDbContext _context;

        public PatientsController(PatientDbContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            if (_context.Patient == null)
            {
                return NotFound();
            }
            return await _context.Patient.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(Guid id)
        {
            if (_context.Patient == null)
            {
                return NotFound();
            }
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // GET: Patients/Search
        [HttpGet]
        [Route("Search")]
        //public async Task<ActionResult<Patient>> GetFilteredPatient([FromBody] FilterRequest date)
        public async Task<ActionResult<Patient>> GetFilteredPatient(bool isBefore, DateTime? date, bool isLater)
        {
            if (_context.Patient == null)
            {
                return NotFound();
            }

            var query = _context.Patient.AsQueryable();


            if (isBefore == isLater)
            {
                if (date.HasValue)
                {
                    query = query.Where(d => d.BirthDate.Date == date.Value.Date);
                }
            }
            else
            {
                if (isBefore && date.HasValue)
                {
                    query = query.Where(d => d.BirthDate.Date < date.Value.Date);
                }
                else if (isLater && date.HasValue)
                {
                    query = query.Where(d => d.BirthDate.Date > date.Value.Date);
                }
            }

            return Ok(await query.ToListAsync());
        }


        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(Guid id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (_context.Patient == null)
            {
                return Problem("Entity set 'PatientDbContext.Patient'  is null.");
            }
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            if (_context.Patient == null)
            {
                return NotFound();
            }
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(Guid id)
        {
            return (_context.Patient?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
