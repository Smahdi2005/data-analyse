using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using data_analyse.Data;
using data_analyse.Services;

namespace data_analyse.Controllers
{
    public class FileController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IFileAnalysService _fileAnalysService;

        public FileController(AppDbContext db, IFileAnalysService fileAnalysService)
        {
            _db = db;
            _fileAnalysService = fileAnalysService;
        }
        public IActionResult ManageFiles()
        {
            var files = _db.UploadFiles.ToList();
            return View(files);



        }
        [HttpGet]
        public async Task<IActionResult> StartAnalys(Guid id)
        {
            var file = await _db.UploadFiles.FindAsync(id);
            if (file == null)
            {
                TempData["Error"] = "فایل پیدا نشد.";
                return RedirectToAction(nameof(ManageFiles));
            }
            // ابتدا بررسی می‌کنیم که نوع فایل چیه
            if (file.OriginalFileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                // اگر فایل متنی (.txt) باشه، تحلیل متن انجام میشه
                await _fileAnalysisService.AnalyzeTextFileAsync(file.Id, CancellationToken.None);
            }
            else if (file.OriginalFileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                     file.OriginalFileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                // اگر فایل اکسل (.xls یا .xlsx) باشه، تحلیل اکسل انجام میشه
                await _fileAnalysisService.AnalyzeExcelFileAsync(file.Id, CancellationToken.None);
            }
            else
            {
                // اگر نوع فایل چیز دیگه‌ای باشه، تحلیل عمومی انجام میشه
                await _fileAnalysisService.AnalyzeOtherFileAsync(file.Id, CancellationToken.None);
            }

            // پیام موفقیت برای شروع تحلیل
            TempData["Message"] = "تحلیل فایل شروع شد.";

            // بازگشت به صفحه مدیریت فایل‌ها
            return RedirectToAction(nameof(ManageFiles));


        }
    }
    
}
