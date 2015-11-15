using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace email_client.data.repository
{
    public interface IPagingAndSortingRepository<E,ID> :ICRUDRepository<E,ID>
    {
        Page<E> getPage(int nr);
        int getPageCount();
        void setComarator(IComparable<E> comparator);
        ICollection<Page<E>> generatePages();
    }
}
