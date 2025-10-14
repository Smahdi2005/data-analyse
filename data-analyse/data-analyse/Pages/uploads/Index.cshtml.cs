using System;
using System.IO;
using System.Threading.Tasks;
using data_analyse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace data_analyse.Pages.Uploads
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
        public string? Message { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (File == null || File.Length == 0)
            {
                TempData["Error"] = "هیچ فایلی انتخاب نشده ";
                return Page();
            }
            var allowedExtentions = new[] { ".txt, .xls, .xlsx" };
            var extention = Path.GetExtension(File.FileName).ToLower();

            var allowedExtensions = new[] { ".txt", ".xls", ".xlsx" };
            var extension = Path.GetExtension(File.FileName);

            if (string.IsNullOrWhiteSpace(extension) ||
                !allowedExtensions.Any(e => e.Equals(extension, StringComparison.OrdinalIgnoreCase)))
            {
                TempData["Error"] = "فرمت فایل مجاز نیست!";
                return Page();
            }

            if (File.Length > 50 * 1024 * 1024)
            {
                TempData["Error"] = "حجم فایل بیش از حد مجاز است";
                return Page();
            }
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await File.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }
            var uploadFile = new UploadFile
            {
                Id = Guid.NewGuid(),
                OriginalFileName = File.FileName,
                Data = fileBytes,
                CreatedAtUtc = DateTime.UtcNow,
            };
            _db.UploadFiles.Add(uploadFile);
            await _db.SaveChangesAsync();

            TempData["Message"] = $"فایل {File.FileName} با موفقیت اپلود و ذخیره شد";

            return RedirectToPage();

        }
    }
}

