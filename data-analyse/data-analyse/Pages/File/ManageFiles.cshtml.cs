using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using data_analyse.Data;
using data_analyse.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
namespace data_analyse.Pages.File
{
    public class ManageFilesModel : PageModel  // این نام باید مطابق با @model در فایل Razor باشد
    {
        private readonly AppDbContext _db;
        public ManageFilesModel(AppDbContext db)
        {
            _db = db;
        }

        public IList<UploadFile> Files { get; set; }  // لیست فایل‌های آپلود شده

        public void OnGet()
        {
            // اینجا فایل‌ها رو از دیتابیس می‌گیریم
            Files = _db.UploadFiles.ToList();
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
