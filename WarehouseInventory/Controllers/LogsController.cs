using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseInventory.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class LogsController : ControllerBase
{
    private readonly string _logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetLogFiles()
    {
        if (!Directory.Exists(_logDirectory))
            return NotFound("Logs directory not found.");

        var files = Directory.GetFiles(_logDirectory, "log-*.txt")
                             .Select(Path.GetFileName)
                             .ToList();

        return Ok(files);
    }

    [HttpGet("{fileName}")]
    public IActionResult GetLogFileContent(string fileName)
    {
        var filePath = Path.Combine(_logDirectory, fileName);
        if (!System.IO.File.Exists(filePath))
            return NotFound("Log file not found.");

        var content = System.IO.File.ReadAllText(filePath);
        return Ok(content);
    }
}