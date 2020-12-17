using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_With_Login.Models;

namespace API_With_Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : Controller
    {
        private readonly NumbersContext _context;

        public NumbersController(NumbersContext context)
        {
            _context = context;
        }

        // GET: api/Numbers?num1=***&num2=***
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Numbers>>> GetNumbers(int Num1, int Num2)
        {
            int Sum = Num1 + Num2;
            var nums = new Numbers() { Num1 = Num1, Num2 = Num2, Sum = Sum };
            _context.Add(nums);
            await _context.SaveChangesAsync();
            return await _context.Numbers.ToListAsync();
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Numbers>> GetNumbers(long id)
        {
            var numbers = await _context.Numbers.FindAsync(id);

            if (numbers == null)
            {
                return NotFound();
            }

            return numbers;
        }

        // PUT: api/Numbers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNumbers(long id, Numbers numbers)
        {
            if (id != numbers.Id)
            {
                return BadRequest();
            }

            _context.Entry(numbers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NumbersExists(id))
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

        // POST: api/Numbers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Numbers>> PostNumbers(Numbers numbers)
        {
            _context.Numbers.Add(numbers);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNumbers), new { id = numbers.Id }, numbers);
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Numbers>> DeleteNumbers(long id)
        {
            var numbers = await _context.Numbers.FindAsync(id);
            if (numbers == null)
            {
                return NotFound();
            }

            _context.Numbers.Remove(numbers);
            await _context.SaveChangesAsync();

            return numbers;
        }

        private bool NumbersExists(long id)
        {
            return _context.Numbers.Any(e => e.Id == id);
        }
    }
}
