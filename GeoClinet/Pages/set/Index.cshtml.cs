using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GeoClinet.Pages.set
{
    [Authorize(Policy = "Teacher")]
    public class IndexModel : PageModel
    {
        private readonly GeoTycoonDbcontext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(GeoTycoonDbcontext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;

            
        }
        public string UserId { get; set; }
        public IList<SetQuestion> SetQuestions { get; set; } = new List<SetQuestion>();

        [BindProperty]
        public SetQuestion SetQuestion { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchBySetName { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByQuestionNumber { get; set; }

        public async Task OnGetAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                UserId = userIdClaim.Value;
                ViewData["CurrentUserId"] = UserId;
            }
            // Default to SearchBySetName if no search type is selected
            if (!SearchBySetName && !SearchByQuestionNumber)
            {
                SearchBySetName = true;
            }

            var query = from s in _context.SetQuestions
                        select s;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchBySetName && SearchByQuestionNumber)
                {
                    if (int.TryParse(SearchTerm, out int number))
                    {
                        query = query.Where(s => s.SetName.Contains(SearchTerm) || s.QuestionNumber == number);
                    }
                    else
                    {
                        query = query.Where(s => s.SetName.Contains(SearchTerm));
                    }
                }
                else if (SearchBySetName)
                {
                    query = query.Where(s => s.SetName.Contains(SearchTerm));
                }
                else if (SearchByQuestionNumber)
                {
                    if (int.TryParse(SearchTerm, out int number))
                    {
                        query = query.Where(s => s.QuestionNumber == number);
                    }
                }
            }

            SetQuestions = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var errors = new List<string>();

            var existingSetQuestion = await _context.SetQuestions
    .FirstOrDefaultAsync(sq => sq.SetName == SetQuestion.SetName);


            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                UserId = userIdClaim.Value;
                ViewData["CurrentUserId"] = UserId;
            }
            if (string.IsNullOrEmpty(UserId))
            {
                ModelState.AddModelError("", "ID not found.");
                return Page();
            }


            if (existingSetQuestion != null)
            {
                errors.Add("SetName already exists.");
            }

            if (SetQuestion.QuestionNumber < 30)
            {
                errors.Add("QuestionNumber must not be less than 30.");
            }

            if (errors.Any())
            {
                return BadRequest(errors);
            }

            _context.SetQuestions.Add(SetQuestion);
            await _context.SaveChangesAsync();

            return new PageResult();
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            var errors = new List<string>();
            var existingSetQuestion = _context.SetQuestions
                .FirstOrDefault(sq => sq.SetName == SetQuestion.SetName && sq.Id != SetQuestion.Id);

            if (existingSetQuestion != null)
            {
                errors.Add("SetName already exists.");
            }

            if (SetQuestion.QuestionNumber < 30)
            {

                errors.Add("QuestionNumber must not be less than 30.");

            }

            if (errors.Any())
            {
                return BadRequest(errors);
            }

            _context.Attach(SetQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetQuestionExists(SetQuestion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new PageResult();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var questionToDelete = await _context.SetQuestions.FindAsync(id);

            if (questionToDelete == null)
            {
                return NotFound();
            }

            _context.SetQuestions.Remove(questionToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        private bool SetQuestionExists(string id)
        {
            return _context.SetQuestions.Any(e => e.Id == id);
        }
    }
}
