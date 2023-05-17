using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Data.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDBContext _appEFContext;
        public CategoriesController(ApplicationDBContext appEFContext)
        {
            _appEFContext = appEFContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> list()
        {
            var result = await _appEFContext.Categories.ToListAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateiewModel model)
        {
            CategoryEntity category = new CategoryEntity
            {
                DateCreated = DateTime.UtcNow,
                Name = model.Name,
                Description = model.Description,
                Image = model.Image,
                Priority = model.Priority
            };
            await _appEFContext.AddAsync(category);
            await _appEFContext.SaveChangesAsync();
            return Ok(model);
        }
    }
}
