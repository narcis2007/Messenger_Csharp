using System.Collections.Generic;

namespace email_client.data.repository
{
    public interface Pageable<E>
    {
        int getPageSize();
        IEnumerable<E> getElements();
        bool addElement(E e);
    }
}