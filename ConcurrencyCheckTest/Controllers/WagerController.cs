using ConcurrencyCheckTest.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ConcurrencyCheckTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WagerController : ControllerBase
    {
        private readonly MyDbContext _context;
        public WagerController(MyDbContext cache)
        {
            _context = cache;
        }

        // generate a new wager
        [HttpPost("createWager")]
        public async Task<IActionResult> CreateWager(int Id)
        {
            // 在實際應用中，應該從資料庫中取得 Wager 實例
            // 此處僅作為示例，返回一個新的 Wager 實例
            Wager wager = new Wager { Id = Id, Status = 0, UpdateDate = DateTimeOffset.Now };

            _context.Wagers.Add(wager);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetWager), new { id = wager.Id }, wager);

        }

        // GET: api/Wagers
        [HttpGet]
        public ActionResult<IEnumerable<Wager>> GetWagers()
        {
            return _context.Wagers.ToList();
        }

        // GET: api/Wagers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWager(int Id)
        {
            // 從 MemoryCache 中獲取 Wager
            var wager = _context.Wagers.Find(Id);

            if (wager == null)
            {
                return NotFound();
            }

            return Ok(wager);
        }

        // PUT: api/Wagers/5
        [HttpPut("{id}")]
        public IActionResult PutWager(int id, int status,bool idDelay)
        {
            var wager = _context.Wagers.Find(id);

            if (wager == default)
            {
                return BadRequest();
            }
            wager.Status = status;
            wager.UpdateDate = DateTime.UtcNow;

            _context.Entry(wager).State = EntityState.Modified;

            if (idDelay)
            {
                Task.Delay(5000).Wait();
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WagerExists(id))
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

        // DELETE: api/Wagers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWager(int id)
        {
            var wager = _context.Wagers.Find(id);
            if (wager == null)
            {
                return NotFound();
            }

            _context.Wagers.Remove(wager);
            _context.SaveChanges();

            return NoContent();
        }

        private Task<Wager> GetWagerFromDatabase(int wagerId)
        {
            // 在實際應用中，應該從資料庫中取得 Wager 實例
            // 此處僅作為示例，返回一個新的 Wager 實例
            return Task.FromResult(new Wager { Id = wagerId });
        }

        private bool WagerExists(int id)
        {
            return _context.Wagers.Any(e => e.Id == id);
        }
    }
}
