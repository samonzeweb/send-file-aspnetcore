using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SendFile.Controllers
{
    [Route("/api/file")]
    public class SendFileController : Controller
    {
        [HttpGet("raw")]
        public async Task GetRaw()
        {
            this.Response.StatusCode = StatusCodes.Status200OK;
            this.Response.ContentType = "text/plain ;charset=utf-8";
            // If you need ETag
            // this.Response.Headers.Append("ETag", "\"ABCDEF\"");
            
            // You have the total control, content could be
            // generated 'on the fly' if needed, but all headears
            // have to be set by you.
            var content = await System.IO.File.ReadAllBytesAsync("data.txt");
            await this.Response.Body.WriteAsync(content);

            // Or ...
            // using(var stream = System.IO.File.Open("data.txt", FileMode.Open, FileAccess.Read))
            // {
            //     await stream.CopyToAsync(this.Response.Body);
            // }            
        }

        [HttpGet("withsendfile")]
        public async Task GetWithSendFile()
        {
            this.Response.StatusCode = StatusCodes.Status200OK;
            this.Response.ContentType = "text/plain ;charset=utf-8";
            // If you need ETag
            // this.Response.Headers.Append("ETag", "\"ABCDEF\"");

            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.sendfileresponseextensions?view=aspnetcore-2.0
            // SendFileAsync does not add headers, or do anything unwanted.
            // -> do it yourself but you've the control.
            await this.Response.SendFileAsync("data.txt");
        }

        [HttpGet("withphysicalfile")]
        public IActionResult GetWithPhysicalFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt");
            
            // Simple version
            //return PhysicalFile(filePath, "text/plain ;charset=utf-8");

            // More complex version
            return new PhysicalFileResult(filePath, "text/plain ;charset=utf-8")
            {
                // If you need ETag
                // EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"ABCDEF\""),
            };            
        }

        [HttpGet("withvirtualfile")]
        public IActionResult GetWithVirtualFile()
        {
            // returns a VirtualFileResult, restricted to wwwroot
            
            // Simple version
            //return File("data-from-wwwroot.txt", "text/plain ;charset=utf-8");
            
            // More complex version
            return new VirtualFileResult("data-from-wwwroot.txt", "text/plain ;charset=utf-8")
            {
                // If you need ETag
                // EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"ABCDEF\""),
            };
        }

        [HttpGet("withfilestream")]
        public IActionResult GetWithFileStream()
        {
            // returns a FileStreamRestult
            // The given stream will be disposed by the framework
            var stream = System.IO.File.Open("data.txt", FileMode.Open, FileAccess.Read);

            return new FileStreamResult(stream, "text/plain ;charset=utf-8")
            {
                // If you need ETag
                // EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"ABCDEF\""),
            };
        }

        [HttpGet("withfilecontent")]
        public async Task<IActionResult> GetWithFileContent()
        {
            // returns a FileContentRestult
            var content = await System.IO.File.ReadAllBytesAsync("data.txt");
            return new FileContentResult(content, "text/plain ;charset=utf-8")
            {
                // If you need ETag
                // EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"ABCDEF\""),
            };
        }
    }
}