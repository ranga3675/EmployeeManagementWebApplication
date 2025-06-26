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
    public class DetailsModel : PageModel
    {
        private readonly IService _service;
        public DetailsModel(IService service)
        {
          _service = service;
        }

        public Employee MongoDb { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while processing your request: {ex.Message}");
                return Page();
            }

        }
    }
}
