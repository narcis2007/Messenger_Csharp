using email_client.data.repository;
using email_client.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using email_client.data.domain;
using System.Xml.Linq;

namespace email_client.repository
{
    class EmailRepository : AbstractPagingAndSortingRepository<Email, int>
    {
       

        public EmailRepository(Validator<Email> validator, string filename) : base(validator, filename)
        {
        }

        public override int getEntityID(Email e)
        {
            return e.Id;
        }

        public override void setEntityID(Email e, int id)
        {
            e.Id = id;
        }

        internal override XElement child(Email e)
        { 
            XAttribute id = new XAttribute("id", e.Id);
            XElement status = new XElement("status", e.Status);
            XElement title = new XElement("title", e.Title);
            XElement content = new XElement("content", e.Content);
            XElement username = new XElement("username", e.Username);
            XElement receiver = new XElement("receiver", e.Receiver);


            XElement xEmail = new XElement(getEntityName());
            xEmail.Add(id);
            xEmail.Add(status);
            xEmail.Add(title);
            xEmail.Add(content);
            xEmail.Add(username);
            xEmail.Add(receiver);
            return xEmail;
        }

        internal override string getEntityName()
        {
            return typeof(User).Name;
        }

        internal override Email saveXElement(XElement xElement)
        {
            int id = Int32.Parse(xElement.Attribute("id").Value);
            string username = xElement.Element("username").Value;
            string receiver = xElement.Element("receiver").Value;
            string title = xElement.Element("title").Value;
            string content = xElement.Element("content").Value;
            Status status = (Status)Enum.Parse(typeof(Status), xElement.Element("status").Value);


            Email email = new Email(title,content);
            email.Id = id;
            email.Status = status;
            email.Username = username;
            email.Receiver = receiver;
            return email;
        }

    }
    
}
