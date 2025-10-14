using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_analyse.Data;
using data_analyse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace data_analyse.Pages.File
{
    public class ManageFilesModel : PageModel  // این نام باید مطابق با @model در فایل Razor باشد
    {
        private readonly AppDbContext _db;
        public ManageFilesModel(AppDbContext db)
        {
            _db = db;
        }
        public List<UploadFile> Files { get; set; } = new List<UploadFile>();  // لیست فایل‌های آپلود شده

        public async Task OnGetAsync()
        {
            Files = await _db.UploadFiles
                             .OrderByDescending(f => f.CreatedAtUtc)
                             .ToListAsync();
        }

        [BindProperty]
        public Guid FileId { get; set; }

        public async Task<IActionResult> OnPostStartAnalysisAsync(Guid id)
        {
            var file = await _db.UploadFiles.FindAsync(id);
            if (file == null)
            {
                TempData["Error"] = "فایل پیدا نشد.";
                return RedirectToPage();
            }

            // اینجا می‌توانید متد تحلیل فایل رو صدا بزنید
            TempData["Message"] = "تحلیل فایل شروع شد.";
            return RedirectToPage();
        }
    }
}
