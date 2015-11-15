using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using email_client.data.domain;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace email_client.data.repository
{
    abstract public class AbstractXMLRepository<E, ID> : AbstractCRUDRepository<E, ID>, IFileRepository<E, ID>
    {
        private string filename;

        protected AbstractXMLRepository(Validator<E> validator,string filename) : base(validator)
        {
            this.filename = filename;
            loadMemory();
        }

        public void loadMemory()
        {
            if (!File.Exists(filename))
            {
                XElement root = new XElement(getEntityName() + 's');
                root.Save(filename);
            }
            XElement allData = XElement.Load(filename);
            if (allData != null)
            {
                IEnumerable<XElement> xElements = allData.Descendants(getEntityName());
                foreach (XElement xElement in xElements)
                {
                    
                    E e=saveXElement(xElement);
                    base.save(e);
                }
                    
            }
        }

        internal abstract E saveXElement(XElement xElement);

        public void setFile(string filename)
        {
            this.filename = filename;
        }

        public void updateFile()
        {
            XElement root = new XElement(getEntityName()+'s');
            // Add child nodes
            foreach (E e in elements)
            {
                root.Add(child(e));
                
            }
            root.Save(filename);
        }

        internal abstract XElement child(E e);
        internal abstract string getEntityName();


        public override bool save(E e)
        {
            bool ok = base.save(e);
            if (ok)
                updateFile();
            return ok;
        }

        public override bool update(E e)
        {
            bool ok = base.update(e);
            if (ok)
                updateFile();
            return ok;
        }

        public override bool delete(ID id)
        {
            bool ok = base.delete(id);
            if (ok)
                updateFile();
            return ok;
        }
    }
}
