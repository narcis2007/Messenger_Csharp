using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace email_client.data.repository
{
    public interface ICRUDRepository<E,ID> : IRepository<E,ID>
    {   //I/O in fisiere XML
        int count();
        E find(ID id);
        IEnumerable<E> getAll();
        bool save(E e);
        bool update(E e);
        bool delete(ID id);
    }

}
