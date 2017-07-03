using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Controllers.Resources;
using ShoppingCart.Models;
using ShoppingCart.Persistence;

namespace ShoppingCart.Controllers
{
    public class FeaturesController : Controller
    {
        private ShoppingCartDbContext _context;
        private IMapper _mapper;

        public FeaturesController(ShoppingCartDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await _context.Features.ToListAsync();

            return _mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }
    }
}
