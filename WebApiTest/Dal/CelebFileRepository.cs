using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WebApiTest.Models;

namespace WebApiTest.Dal
{
    public class CelebFileRepository
    {
        readonly string _dbPath = Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, @"DB/celebDb.json");
        private static int _id = 1;

        public bool Save(IEnumerable<Celeb> celeb)
        {
            IEnumerable<Celeb> existing = GetAllCelebs();
            var newList = existing.ToList();
            newList.AddRange(celeb);
            foreach (var item in newList)
            {
                item.Id = _id++;
            }
            InternalSave(newList);

            return true;
        }

        public Celeb GetById(int id)
        {
            return GetAllCelebs().First(o => o.Id == id);
        }

        public IEnumerable<Celeb> GetAllCelebs()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Celeb>>(File.ReadAllText(_dbPath));
        }

        public bool Remove(int id)
        {
            IEnumerable<Celeb> existing = GetAllCelebs();
            var newList = existing.ToList();
            InternalSave(newList.Where(o => o.Id != id));

            return true;
        }

        public bool Update(Celeb celeb)
        {
            IEnumerable<Celeb> existing = GetAllCelebs();
            var newList = existing.ToList();
            var list = newList.Where(o => o.Id != celeb.Id).ToList();
            list.Add(celeb);
            InternalSave(list);

            return true;
        }

        private void InternalSave(IEnumerable<Celeb> celeb)
        {
            using (StreamWriter file = File.CreateText(_dbPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, celeb);
            }
        }
    }
}