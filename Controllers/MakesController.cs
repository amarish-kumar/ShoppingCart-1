using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Core.Models;

namespace ShoppingCart.Controllers
{
    public class MakesController : Controller
    {
        private ShoppingCartDbContext _context;
        private IMapper _mapper;

        public MakesController(ShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            //Currently, EFCore only supports sync option of ToList
           // return await _context.Makes.Include(m => m.Models).ToListAsync();
           var makes = await _context.Makes.Include(m => m.Models).ToListAsync();

            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}
