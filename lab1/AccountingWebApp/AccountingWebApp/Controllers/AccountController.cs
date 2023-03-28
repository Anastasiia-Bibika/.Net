using AccountingWebApp.Models;
using AccountingWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AplicationDBContext _context;
        public AccountController(AplicationDBContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return Ok(await this._context.Accounting.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddNewCar(Account account)
        {
            this._context.Accounting.Add(account);
            await this._context.SaveChangesAsync();

            return Ok(await this._context.Accounting.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Account>>> UpdateCar(Account account)
        {
            this._context.Accounting.Update(account);
            await this._context.SaveChangesAsync();

            return Ok(await this._context.Accounting.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Account>>> DeleteCar(int id)
        {
            var DBaccount = await this._context.Accounting.FindAsync(id);
            if (DBaccount == null)
            {
                return BadRequest("Value is null");
            }
            this._context.Accounting.Remove(DBaccount);
            await this._context.SaveChangesAsync();
            return Ok(await this._context.Accounting.ToListAsync());
        }
    }
}
