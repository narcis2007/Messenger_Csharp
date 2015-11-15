using System;
using System.Collections.Generic;

namespace email_client.data.repository
{
    public class Page<E> : Pageable<E>
    {
        private List<E> elements;
        private static int maxPageSize = 5;
        public Page()
        {
            elements = new List<E>();
        }

        public static int MaxPageSize { get {return maxPageSize; } internal set {maxPageSize=value; } }

        public bool addElement(E e)
        {
            if (elements.Count < maxPageSize)
            {
                elements.Add(e);
                return true;
            }
            return false;
                
        }

        public IEnumerable<E> getElements()
        {
            return elements;
        }

        public int getPageSize()
        {
            return elements.Count;
        }
    }
}