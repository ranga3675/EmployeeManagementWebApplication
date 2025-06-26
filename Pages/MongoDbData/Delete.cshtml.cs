using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDbWebApplication.Interfaces;
using MongoDbWebAppplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbWebApplication.Pages.MongoDbData
{
    public class DeleteModel : PageModel
    {
        private readonly IService _service;
        public DeleteModel(IService service)
        {
            _service = service;
        }

        [BindProperty]
        public Employee MongoDb { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mongodb = _service.GetByIdAsync<Employee>(id).Result;

            if (mongodb == null)
            {
                return NotFound();
            }
            else
            {
                MongoDb = mongodb;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                // Check if MongoDb object is null
                if (MongoDb == null)
                {
                    return NotFound("MongoDb object is null.");
                }
                // Delete the MongoDb item
                var mongodb = _service.GetByIdAsync<Employee>(id).Result;
                if (mongodb != null)
                {
                    MongoDb = mongodb;
                    await _service.DeleteAsync<Employee>(id);                    
                }
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while processing your request: {ex.Message}");
                return Page();
            }          
        }
    }
}
