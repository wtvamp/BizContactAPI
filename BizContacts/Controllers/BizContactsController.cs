using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BizContacts.DAL;
using Microsoft.AspNetCore.Authorization;

namespace BizContacts.API.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class BizContactsController : ControllerBase
    {
        private readonly BizContactContext _context;

        public BizContactsController(BizContactContext context)
        {
            _context = context;
        }

        // GET: api/BizContacts
        [HttpGet]
        public IEnumerable<BizContact> GetBizContacts()
        {
            return _context.BizContacts;
        }

        // GET: api/BizContacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBizContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bizContact = await _context.BizContacts.FindAsync(id);

            if (bizContact == null)
            {
                return NotFound();
            }

            return Ok(bizContact);
        }

        // PUT: api/BizContacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBizContact([FromRoute] int id, [FromBody] BizContact bizContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bizContact.BizContactId)
            {
                return BadRequest();
            }

            _context.Entry(bizContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BizContactExists(id))
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

        // POST: api/BizContacts
        [HttpPost]
        public async Task<IActionResult> PostBizContact([FromBody] BizContact bizContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BizContacts.Add(bizContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBizContact", new { id = bizContact.BizContactId }, bizContact);
        }

        // DELETE: api/BizContacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBizContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bizContact = await _context.BizContacts.FindAsync(id);
            if (bizContact == null)
            {
                return NotFound();
            }

            _context.BizContacts.Remove(bizContact);
            await _context.SaveChangesAsync();

            return Ok(bizContact);
        }

        private bool BizContactExists(int id)
        {
            return _context.BizContacts.Any(e => e.BizContactId == id);
        }
    }
}