using System.Threading.Tasks;
using TreeApi.Models;
using System.Web.Http;
using TreeApi.Providers;

namespace TreeApi.Controllers
{
    public class TreeController : ApiController
    {
        private ITreeProvider _dataProvider;

        public TreeController(ITreeProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        // GET: Tree
        public async Task<World[]> Get()
        {
            return await _dataProvider.BuildTree();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}