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
        [HttpPost]
        public async Task<IActionResult> StartAnalys(Guid fileId)
        {
            var file = await _db.UploadFiles.FindAsync(fileId);
            if (file == null)
            {
                TempData["Error"] = "فایل پیدا نشد";
                return RedirectToAction(nameof(ManageFiles));
            }
            if (file.OriginalFileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                await _fileAnalysService.AnalyseTextFileAsync(file.Id, CancellationToken.None);
            }
            else if (file.OriginalFileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) ||
                     file.OriginalFileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                await _fileAnalysService.AnalyseExelFileAsync(file.Id, CancellationToken.None);
            }
            else
            {
                TempData["Error"] = "فرمت فایل پشتیبانی نمی‌شود!";
                return RedirectToAction(nameof(ManageFiles));
            }

            TempData["Message"] = "تحلیل فایل شروع شد!";
            return RedirectToAction(nameof(ManageFiles));
        }
        


    }

}
