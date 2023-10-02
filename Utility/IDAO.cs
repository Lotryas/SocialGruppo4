using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public interface IDAO
    {
        public List<Entity> Read();
        public bool Delete(int id);
        public bool Insert(Entity e);
        public bool Update(Entity e);
        public Entity Find(int id);
    }
}
