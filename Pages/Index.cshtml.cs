using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDbWebApplication.Interfaces;
using MongoDbWebApplication.Services;
using MongoDbWebAppplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbWebApplication.Pages
{
    public class IndexModel : PageModel
    {   
        private readonly IService _service;

        public IndexModel(IService service)
        {           
            _service = service;
        }

        public IList<Employee> MongoDb { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {       

                // Fetching data using the service layer instead of direct collection access
                MongoDb = (IList<Employee>)await _service.GetAllAsync<Employee>();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error or show a user-friendly message
                ModelState.AddModelError(string.Empty, "An error occurred while loading data: " + ex.Message);
                //MongoDb = await _collection.Find(_ => true).ToListAsync();
            }
                
        }
    }
}
