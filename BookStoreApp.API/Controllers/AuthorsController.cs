using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using AutoMapper;
using BookStoreApp.API.Static;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BooKstoreDbContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BooKstoreDbContext context,IMapper mapper, ILogger<AuthorsController>logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
         
            try
            {
                var author = await _context.Authors.FirstOrDefaultAsync();
                if (_context.Authors == null)
                {
                    logger.LogWarning($"Record not Found: {nameof(GetAuthors)}");
                    return NotFound();
                }
               
                var AuthorsDtos = mapper.Map<IEnumerable<AuthorReadOnlyDto>>(author);
                return Ok(AuthorsDtos);
            }
            catch (Exception ex )
            {
                logger.LogError(ex, $"Eror Performing GET in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            throw new Exception("Test");
            try
            {
                if (_context.Authors == null)
                {
                    return NotFound();
                }


                var author = await _context.Authors.FindAsync(id);
                var authorDto = mapper.Map<AuthorReadOnlyDto>(author);

                if (author == null)
                {
                    logger.LogWarning($"Record Not Found : {nameof(GetAuthor)}   - ID {id}");
                    return NotFound();
                }

                return Ok(authorDto);
            }
            catch (Exception ex)
            {

                logger.LogError(ex,$"Error performing GET in {nameof(GetAuthor)}");
                return StatusCode(500,Messages.Error500Message);
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
         


            if (id != authorDto.Id)
            {
                logger.LogWarning($"Update ID invalid in {nameof(PutAuthor)}");
                return BadRequest();
            }
            var author =await _context.Authors.FindAsync(id);

            if (author == null)
            {
                logger.LogWarning($"{nameof(Author)}Update ID invalid in {nameof(PutAuthor)}");
                return NotFound();
            }
             mapper.Map(authorDto,author);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            
            try
            {
                var author = mapper.Map<Author>(authorDto);
                if (_context.Authors == null)
                {
                    return Problem("Entity set 'BooKstoreDbContext.Authors'  is null.");
                }


                _context.Authors.Add(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
            }
            catch (Exception ex)
            {

                logger.LogError(ex,$"Error Performing POST in {nameof(PostAuthor)}",authorDto);
                return StatusCode(500,Messages.Error500Message);
            }
           
            


         
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                if (_context.Authors == null)
                {
                    logger.LogWarning($"{nameof(Author)} record not found in {nameof(DeleteAuthor)}");
                    return NotFound();
                }
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                logger.LogError(ex,$"Error Peerforming DELETE  in {nameof(DeleteAuthor)}");
                return StatusCode(500,Messages.Error500Message);
            }
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
