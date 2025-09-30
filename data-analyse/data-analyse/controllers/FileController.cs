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
        public IActionResult StartAnalys(Guid id)
        {
            var file = _db.UploadFiles.Find(id);
            if (file == null)
            {
                TempData["Error"] = "فایل پیدا نشد";
                return RedirectToAction(nameof(ManageFiles));
            }
            _fileAnalysService.StartAnalyse(file.Id);
            TempData["Message"] = "تحلیل فایل شروغ شد";
            return RedirectToAction(nameof(ManageFiles));
        }
        


    }

}
