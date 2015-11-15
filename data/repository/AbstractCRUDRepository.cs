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
        protected Validator<E> validator;

        protected AbstractCRUDRepository(Validator<E> validator)
        {
            this.validator = validator;
        }

        public virtual int count()
        {
            return elements.Count;
        }

        public virtual bool delete(ID id)
        {
            foreach(E e in elements)
            {
                if (getEntityID(e).Equals(id))
                {
                    elements.Remove(e);
                    return true;
                }
            }
            return false;
        }

        public virtual E find(ID id)
        {
            foreach(E e in elements)
            {
                if (getEntityID(e).Equals(id))
                {
                    return e;
                }
            }
            return default(E);
        }

        public virtual IEnumerable<E> getAll()
        {
            return elements;
        }

        public virtual bool save(E e)
        {
            Errors errors =validator.validate(e);
            if (errors.count() > 0)
                throw new ValidationException(errors);
             setEntityID(e, elements.Count + 1);
             elements.Add(e);
            return true;
        }

        public virtual bool update(E e)
        {
            Errors errors = validator.validate(e);
            if (errors.count() > 0)
                throw new ValidationException(errors);
            for (int i=0;i< elements.Count;i++)
            {
                if (getEntityID(e).Equals(getEntityID(elements[i])))
                {
                    elements[i]=e;
                    return true;
                }
            }
            return false;
        }
        public abstract void setEntityID(E e, int id);
        public abstract ID getEntityID(E e);
    }
}
