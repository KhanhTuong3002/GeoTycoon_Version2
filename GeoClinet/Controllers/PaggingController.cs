using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace GeoClinet.Controllers
{
    public class PaggingController : Controller
    {
        private readonly GeoTycoonDbcontext _context;
        public PaggingController(GeoTycoonDbcontext context)
        {
            _context = context;
        }
    }
}
