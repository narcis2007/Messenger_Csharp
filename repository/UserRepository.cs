using System;
using email_client.domain;
using email_client.data.repository;
using System.Xml.Linq;
using email_client.data.domain;

namespace email_client.repository
{
    public class UserRepository : AbstractXMLRepository<User, int>
    {

        public UserRepository(Validator<User> validator, string filename) : base(validator, filename)
        {
        }

        public User findByUsername(string username)
        {
            
            for(int i=0;i< elements.Count; i++)
            {
                if(elements[i].Username==username)
                    return User.copy(elements[i]);
            }
            return null;
        }
        public override int getEntityID(User user)
        {
            return user.Id;
        }

        public override void setEntityID(User user, int id)
        {
            user.Id = id;
        }

        internal override XElement child(User u)
        {
            XAttribute id = new XAttribute("id", u.Id);
            XElement username = new XElement("username", u.Username);
            XElement password = new XElement("password", u.Password);
            XElement xuser = new XElement(getEntityName());
            xuser.Add(id);
            xuser.Add(username);
            xuser.Add(password);
            return xuser;
        }

        internal override string getEntityName()
        {
            return typeof(User).Name;
        }

        internal override User saveXElement(XElement xElement)
        {
            int id= Int32.Parse(xElement.Attribute("id").Value);
            string username = xElement.Element("username").Value;
            string password = xElement.Element("password").Value;
            User user = new User(username,password);
            user.Id = id;
            return user;
           
        }
    }
}