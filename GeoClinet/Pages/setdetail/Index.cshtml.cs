﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using BusinessObject.Entites;
using Microsoft.AspNetCore.Authorization;

namespace GeoClient.Pages.setdetail
{
    [Authorize(Policy = "Teacher")]
    public class IndexModel : PageModel
    {
        private readonly GeoTycoonDbcontext _context;

        public IndexModel(GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<SetQuestionDetail> SetQuestionDetails { get; set; } = new List<SetQuestionDetail>();
        public IList<SetQuestion> SetQuestions { get; set; } = new List<SetQuestion>();
        public IList<Question> Questions { get; set; } = new List<Question>();

        [BindProperty]
        public SetQuestionDetail SetQuestionDetail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchBySetName { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByTitle { get; set; }

        public async Task OnGetAsync()
        {
            // Default to SearchBySetName if no search type is selected
            if (!SearchBySetName && !SearchByTitle)
            {
                SearchBySetName = true;
            }

            var query = _context.SetQuestionDetails
                        .Include(d => d.SetQuestion)
                        .Include(d => d.Question)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchBySetName && SearchByTitle)
                {
                    query = query.Where(d => d.SetQuestion.SetName.Contains(SearchTerm) || d.Question.Title.Contains(SearchTerm));
                }
                else if (SearchBySetName)
                {
                    query = query.Where(d => d.SetQuestion.SetName.Contains(SearchTerm));
                }
                else if (SearchByTitle)
                {
                    query = query.Where(d => d.Question.Title.Contains(SearchTerm));
                }
            }

            SetQuestionDetails = await query.ToListAsync();
            SetQuestions = await _context.SetQuestions.ToListAsync();
            Questions = await _context.Questions.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddDetailAsync()
        {
            _context.SetQuestionDetails.Add(SetQuestionDetail);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditDetailAsync(string id)
        {
            _context.Attach(SetQuestionDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetQuestionDetailExists(SetQuestionDetail.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteDetailAsync(string id)
        {
            var detailToDelete = await _context.SetQuestionDetails.FindAsync(id);
            if (detailToDelete == null)
            {
                return NotFound();
            }

            _context.SetQuestionDetails.Remove(detailToDelete);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        private bool SetQuestionDetailExists(string id)
        {
            return _context.SetQuestionDetails.Any(e => e.Id == id);
        }
    }
}
