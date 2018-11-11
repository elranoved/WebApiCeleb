using System.Collections.Generic;
using System.Web.Http;
using WebApiTest.Dal;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    public class CelebController : ApiController
    {
        private readonly CelebFileRepository _celebFileRepository;

        public CelebController()
        {
         _celebFileRepository = new CelebFileRepository();            
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("api/Celeb/GetAll")]
        public IEnumerable<Celeb> GetAll()
        {
            return _celebFileRepository.GetAllCelebs();
        }

        // GET api/values/5
        public Celeb Get(int id)
        {
            return _celebFileRepository.GetById(id);
        }

        // POST api/values
        public void Post([FromBody]IEnumerable<Celeb> value)
        {
            _celebFileRepository.Save(value);
        }

        // PUT api/values/5
        public void Put([FromBody]Celeb value)
        {
            _celebFileRepository.Update(value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
           _celebFileRepository.Remove(id);
        }
    }
}
