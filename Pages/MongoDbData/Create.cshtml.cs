using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using MongoDbWebApplication.Interfaces;
using MongoDbWebAppplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbWebApplication.Pages.MongoDbData
{
    public class CreateModel : PageModel
    {
        private readonly IService _service;

        public CreateModel(IService service)
        {
           _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Employee MongoDb { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (MongoDb == null)
                {
                    return NotFound("MongoDb object is null.");
                }

                await _service.CreateAsync<Employee>(MongoDb);

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
