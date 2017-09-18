using System.Web.Http;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.Mvc.Api.Controllers
{
    /// <summary>
    /// Base interface for all <see cref="ApiController"/>'s implemented in Kbit.
    /// All <see cref="ApiController"/>'s should implement this interface.
    /// </summary>
    /// <typeparam name="TApplicationModelType"></typeparam>
    public interface IKbitApiController<in TApplicationModelType> where TApplicationModelType : ApplicationModelBase
    {
        /// <summary>
        /// Retrieves a resource by <see cref="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IHttpActionResult GetById(string id);
        IHttpActionResult GetAll();
        IHttpActionResult Add(TApplicationModelType applicationModel);
        IHttpActionResult Update(TApplicationModelType applicationModel);
        IHttpActionResult Delete(TApplicationModelType applicationModel);
    }
}
