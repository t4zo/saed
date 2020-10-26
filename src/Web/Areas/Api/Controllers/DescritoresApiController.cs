﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAED.Infrastructure.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SAED.Web.Areas.Api.Controllers
{
    public class DescritoresApiController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public DescritoresApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            var descritores = await _context.Descritores.AsNoTracking().Where(x => x.TemaId == id).ToListAsync();

            if (descritores is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(descritores));
        }
    }
}