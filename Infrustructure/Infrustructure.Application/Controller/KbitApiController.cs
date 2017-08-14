using System.Web.Http;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Controller
{
    public class KbitApiController: ApiController
    {
        public KbitApiController()
        {
            this.InitializeMongoDbDiInstaller();
        }

        private void InitializeMongoDbDiInstaller()
        {
            
        }
    }
}
