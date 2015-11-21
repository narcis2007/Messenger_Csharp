using email_client.data.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace email_client.data.repository
{
    public abstract class AbstractCRUDRepository<E, ID> : ICRUDRepository<E, ID>
    {
        protected List<E> elements = new List<E>();
        public Validator<E> validator;
        protected Object monitor = new Object();

        public Validator<E> Validator
        {
            get
            {
                return validator;
            }

            set
            {
                validator = value;
            }
        }

        protected AbstractCRUDRepository(Validator<E> validator)
        {
            this.Validator = validator;
        }
        protected AbstractCRUDRepository()
        {
        }

        public virtual int count()
        {
            return elements.Count;
        }

        public virtual bool delete(ID id)
        {
            /*
            foreach(E e in elements)
            {
                if (getEntityID(e).Equals(id))
                {
                    lock (monitor)
                    {
                        elements.Remove(e);
                    }
                    return true;
                }
            }*/
            int initialCount = elements.Count;
            lock (monitor)
            {
                
                var e = from u in elements
                        where !(getEntityID(u).Equals(id))
                        select u;
                elements = e.ToList();
            }
            return initialCount > elements.Count; 

            //return false;
        }

        public virtual E find(ID id)
        {
            /*
            foreach(E e in elements)
            {
                if (getEntityID(e).Equals(id))
                {
                    return e;
                }
            }
            */
            var e = from u in elements
                    where getEntityID(u).Equals(id)
                    select u;
            if (e.Count()==1)
                return e.First(); 

            return default(E);
        }

        public virtual IEnumerable<E> getAll()
        {
            return elements;
        }

        public virtual bool save(E e)
        {

            if (Validator != null)
            {
                Errors errors = Validator.validate(e);
                if (errors.count() > 0)
                    throw new ValidationException(errors);
            }
            setEntityID(e, elements.Count + 1);
            lock (monitor)
            {
                elements.Add(e);
            }
            return true;
        }

        public virtual bool update(E e)
        {

            if (Validator != null)
            {
                Errors errors = Validator.validate(e);
                if (errors.count() > 0)
                    throw new ValidationException(errors);
            }
            for (int i = 0; i < elements.Count; i++)
                {
                    if (getEntityID(e).Equals(getEntityID(elements[i])))
                    {
                        lock (monitor)
                        {
                            elements[i] = e;
                        }
                        return true;
                    }
                }
            return false;
        }
        public abstract void setEntityID(E e, int id);
        public abstract ID getEntityID(E e);
    }
}
