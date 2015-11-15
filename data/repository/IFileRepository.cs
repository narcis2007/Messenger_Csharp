using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace email_client.data.repository
{
    interface IFileRepository<E,ID> :ICRUDRepository<E,ID>
    {
        void updateFile();
        void loadMemory();
        void setFile(string filename);
    }
}
