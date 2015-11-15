using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using email_client.data.domain;

namespace email_client.data.repository
{
    public abstract class AbstractPagingAndSortingRepository<E, ID> : AbstractXMLRepository<E,ID>, IPagingAndSortingRepository<E, ID>
    {
        private IComparable<E> comparator;
        private List<Page<E>> pages;

        protected AbstractPagingAndSortingRepository(Validator<E> validator,string filename) : base(validator,filename)
        {
            pages=generatePages().ToList();
        }

        public ICollection<Page<E>> generatePages()
        {

            int maxSize = Page<E>.MaxPageSize, count = 0;
            List<Page<E>> pages = new List<Page<E>>();
            foreach (E e in elements)
            {
                if (count % maxSize == 0)
                {
                    Page<E> page = new Page<E>();
                    pages.Add(page);
                }
                pages[count / maxSize].addElement(e);
                count++;


                
            }
            return pages;
        }

        public Page<E> getPage(int nr)
        {
            if (nr < 0 || nr >= getPageCount())
                return null;
            return pages[nr];
        }

        public int getPageCount()
        {
            return pages.Count();
        }

        public void setComarator(IComparable<E> comparator)
        {
            this.comparator = comparator;
        }

        public override bool save(E e)
        {
            var ok = base.save(e);
            if (ok)
            {
                pages=generatePages().ToList();
            }
            return ok;
        }

        public override bool delete(ID id)
        {
            var ok= base.delete(id);
            if (ok)
                pages = generatePages().ToList();

            return ok;
        }
    }
}
