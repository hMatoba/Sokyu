using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Sokyu.Models;
using System.Threading.Tasks;

namespace Sokyu.Controllers
{
    [Route("devstoreaccount1")]
    public class HomeController : Controller
    {
        [Route("{*filename}")]
        [HttpPut]
        public async Task<IActionResult> PutAsync(string filename)
        {
            await Blob.PutAsync(filename, Request.Body);
            Response.StatusCode = 201;
            return Content("0");
        }

        [Route("{*filename}")]
        [HttpGet]
        public async Task<IActionResult> GetAsync(string filename)
        {
            var stream = await Blob.GetAsync(filename);
            if (stream == null)
            {
                Response.StatusCode = 404;
                return Content("404 Not Found");
            }

            string _contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out _contentType);
            var contentType = _contentType ?? "application/octet-stream";
            var streamResult = new FileStreamResult(stream, contentType);
            return streamResult;
        }

    }
}
