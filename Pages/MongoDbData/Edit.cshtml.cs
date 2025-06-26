using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDbWebApplication.Interfaces;
using MongoDbWebApplication.Services;
using MongoDbWebAppplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbWebApplication.Pages.MongoDbData
{
    public class EditModel : PageModel
    {      
        private readonly IService _service;

        public EditModel(IService service)
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
            MongoDb = mongodb;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(MongoDb).State = EntityState.Modified;

            try
            {
                await _service.UpdateAsync<Employee>(MongoDb.Id, MongoDb);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while processing your request: {ex.Message}");
                return Page();
                //if (!MongoDbExists(MongoDb.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return RedirectToPage("/Index");
        }

        //private bool MongoDbExists(string id)
        //{
        //    return _context.MongoDb.Any(e => e.Id == id);
        //}
    }
}
