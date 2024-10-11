using IronPdf.Razor.Pages;
using IronPdf.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageSample.Models;

namespace RazorPageSample.Pages
{
    public class PersonsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<Person> persons { get; set; }

        public void OnGet()
        {
            persons = new List<Person>
            {
            new Person { Name = "Alice", Title = "Mrs.", Description = "Software Engineer" },
            new Person { Name = "Bob", Title = "Mr.", Description = "Software Engineer" },
            new Person { Name = "Charlie", Title = "Mr.", Description = "Software Engineer" }
            };

            ViewData["personList"] = persons;
        }
        public IActionResult OnPostAsync()
        {
            persons = new List<Person>
            {
            new Person { Name = "Alice", Title = "Mrs.", Description = "Software Engineer" },
            new Person { Name = "Bob", Title = "Mr.", Description = "Software Engineer" },
            new Person { Name = "Charlie", Title = "Mr.", Description = "Software Engineer" }
            };

            ViewData["personList"] = persons;

            ChromePdfRenderer _pdfRenderer = new ChromePdfRenderer
            {
                RenderingOptions =
                    {
                        MarginLeft = 10,
                        MarginRight = 10,
                        MarginBottom = 15,
                        MarginTop = 15,
                        PaperOrientation = PdfPaperOrientation.Landscape,
                        Timeout = 90
                    }
            };
            _pdfRenderer.RenderingOptions.WaitFor.AllFontsLoaded(2000);

            // Render Razor Page to PDF document
            PdfDocument pdf = _pdfRenderer.RenderRazorToPdf(this);
            
            Response.Headers.Add("Content-Disposition", "inline");

            return File(pdf.BinaryData, "application/pdf", "razorPageToPdf.pdf");
        }
    }
}
