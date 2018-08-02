using System;
using System.Collections.Generic;
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

        // DELETE api/videos/somepath
        [HttpDelete("{path}")]
        public void Delete(string path)
        {
            var item = DBContext.Instance.All<OutputItem>().Where(x => x.OutputPath == path).FirstOrDefault();
            DBContext.Instance.Remove(item);
        }
    }
}
