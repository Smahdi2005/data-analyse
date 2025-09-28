using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace data_analyse.Pages.Uploads
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IFormFile? File { get; set; }
        public string? Message { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (File == null || File.Length == 0)
            {
                Message = "هیچ فایلی انتخاب نشده یا فایل خالی است.";
                return Page();
            }
            Message = $"فایل دریافت شد: {File.FileName} ({File.Length} bytes)";
            return Page();
        }
    }
}
