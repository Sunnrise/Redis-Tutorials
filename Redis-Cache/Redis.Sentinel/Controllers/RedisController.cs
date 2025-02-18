﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.Sentinel.Services;

namespace Redis.Sentinel.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        [HttpGet("{key}/{value}")]
        public async Task<IActionResult> SetValue(string key, string value)
        {
            var redis= await RedisService.RedisMasterDatabase();
            await redis.StringSetAsync(key, value);
            return Ok();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetValue(string key)
        {
            var redis = await RedisService.RedisMasterDatabase();
            var value = await redis.StringGetAsync(key);
            return Ok(value.ToString());
        }
    }
}
