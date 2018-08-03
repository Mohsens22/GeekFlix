using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace GeekFlixServer.Controllers
{
    [Route("api/[controller]")]
    public class VideosController : Controller
    {
        public VideosController()
        {
            DBContext.InitializeLocal();
        }
        // GET api/videos
        [HttpGet]
        public IEnumerable<OutputItem> Get(int limit = 30, int offset = 0)
        {
            return DBContext.Instance.All<OutputItem>().AsEnumerable().Skip(offset).Take(limit);
        }
        [HttpGet("{*id}")]
        public IActionResult GetByLocation(string id)
        {
            try
            {
                Stream stream = new FileStream(id, FileMode.Open, FileAccess.Read, FileShare.Read);
                return File(stream, "application/octet-stream");
            }
            catch
            {
                return NotFound();
            }

        }
        // DELETE api/videos/somepath
        [HttpDelete("{*path}")]
        public void Delete(string path)
        {
            path = path.Replace("/", @"\");
            var item = DBContext.Instance.All<OutputItem>().Where(x => x.OutputPath == path).FirstOrDefault();

            DBContext.LocalInstance.Write(() =>
            {
                var ot = new OutputItem()
                {
                    AfterPause = item.AfterPause,
                    BeforePause = item.BeforePause,
                    EndTime = item.EndTime,
                    Lines = item.Lines,
                    Name = item.Name,
                    OutputPath = item.OutputPath,
                    SpeakingTime = item.SpeakingTime,
                    StartTime = item.StartTime,
                    SynthRate = item.SynthRate
                };
                DBContext.LocalInstance.Add(ot);
            });
            // Log the shit item
            DBContext.Instance.Write(() =>
            {
                DBContext.Instance.Remove(item);
            });

        }
    }
}
