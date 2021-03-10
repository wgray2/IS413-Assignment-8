using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IS413_Assignment_5.Models;
using IS413_Assignment_5.Infrastructure;

namespace IS413_Assignment_5.Pages
{
    public class PurchaseModel : PageModel
    {
        private IStoreRepository repository;

        //Constructor
        public PurchaseModel (IStoreRepository repo, Cart cartService)
        {
            repository = repo;
            Cart = cartService;
        }

        //Properties
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        //Methods
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        
        public IActionResult OnPost(string ISBN, string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.ISBN == ISBN);

            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            
            Cart.AddItem(book, 1);
            
            //HttpContext.Session.SetJson("cart", Cart);

            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(string ISBN, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Book.ISBN == ISBN).Book);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
