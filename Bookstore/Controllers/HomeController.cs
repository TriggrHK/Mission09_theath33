using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository repository)
        {
            repo = repository;
        }

        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 10;
            var books = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    CurrentPage = pageNum,
                    ProjectsPerPage = pageSize,
                    TotalNumProjects = repo.Books.Count()
                }
            };



            return View(books);
        }
    }
}
