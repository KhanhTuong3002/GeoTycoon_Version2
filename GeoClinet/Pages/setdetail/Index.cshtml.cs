using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.setdetail
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<SetQuestionDetail> SetQuestionDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SetQuestionDetail = await _context.SetQuestionDetails
                .Include(s => s.Question)
                .Include(s => s.SetQuestion).ToListAsync();
        }
    }
}
