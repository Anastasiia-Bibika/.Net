using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laboratory2.Models;
using Laboratory2.Data;

namespace Laboratory2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountContext _context;

        public AccountController(AccountContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return Ok(await _context.Account.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return BadRequest(404);
            }
            else
            {
                return Ok(account);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> Add(Account account)
        {
            _context.Add(account);
            await _context.SaveChangesAsync();
            return Ok(await _context.Account.ToListAsync());
        }
        // POST: AccountController/Create
        [HttpPut]
        public async Task<ActionResult<List<Account>>> Update(Account request)
        {
            var account = await _context.Account.FindAsync(request.Id);
            if (account == null)
            {
                return BadRequest(404);
            }
            else
            {
                account.Pib = request.Pib;
                account.Salary = request.Salary;
                account.Childrens = request.Childrens;
                account.Experience = request.Experience;

                await _context.SaveChangesAsync();

                return Ok(account);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<List<Account>>> Delete(int id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return BadRequest(404);
            }
            else
            {
                _context.Account.Remove(account);

                await _context.SaveChangesAsync();

                return Ok(account);
            }
        }

        [HttpGet("orderBy")]
        public async Task<ActionResult<List<Account>>> Get(string order, string column)
        {
            if (order == "asc")
            {

                var accounts = await _context.Account.OrderBy(
                    account => account.GetType().GetProperty(column).GetValue(account)
                ).ToListAsync();
                return Ok(accounts);
            }
            else
            {
                var accounts = await _context.Account.OrderByDescending(
                    account => account.Pib
                ).ToListAsync();
                return Ok(accounts);
            }
        }

        [HttpGet("filterBy")]
        public async Task<ActionResult<List<Account>>> Get(string childrens)
        {
            var accounts = await _context.Account.Where(
                account => account.Childrens == childrens
            ).ToListAsync();
            return Ok(accounts);
        }

        [HttpGet("pages")]
        public async Task<ActionResult<List<Account>>> Get(float pageItems, int page)
        {
            var pageCount = Math.Ceiling(_context.Account.Count() / pageItems);

            var accounts = await _context.Account
                .Skip((page - 1) * (int)pageItems)
                .Take((int)pageItems)
                .ToListAsync();

            return Ok(accounts);
        }
    }
}
