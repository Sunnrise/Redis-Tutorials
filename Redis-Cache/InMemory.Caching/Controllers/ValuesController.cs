using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
      readonly IMemoryCache memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        //[HttpGet("set/{name}")]
        //public void SetName(string name)
        //{
        //    memoryCache.Set("name", name);
        //}


        //[HttpGet]
        //public string GetName()
        //{    
        //    if(memoryCache.TryGetValue<string>("name", out string name))
        //    {
        //        return name.Substring(3);
        //    }
        //    return "No value found";
        //}

        [HttpGet("setDate")]
        public void SetDate()
        {
            memoryCache.Set<DateTime>("date", DateTime.Now, options: new() 
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(10),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }
        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            if (memoryCache.TryGetValue<DateTime>("date", out DateTime date))
            {
                return date;
            }
            return DateTime.MinValue;
        }
    }
}
