using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Get(string name, string surname)
        {
            await distributedCache.SetStringAsync("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

            await distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            }); 
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await distributedCache.GetStringAsync("name");
            var surnameBinary = await distributedCache.GetAsync("surname");
            var surname= Encoding.UTF8.GetString(surnameBinary);
            return Ok(new { name, surname });
        }
    }
}
