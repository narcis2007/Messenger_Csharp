using System;
using email_client.repository;
using email_client.domain;
using System.Collections.Generic;
using email_client.data.repository;

namespace email_client.service
{
    internal class Service
    {
        private EmailRepository emailRepository;
        private UserRepository userRepository;
        private User user;

        public Service(EmailRepository emailRepository, UserRepository userRepository)//repository generic, sa utilizez un singur repository de baza
        {
            this.emailRepository = emailRepository;
            this.userRepository = userRepository;
        }

        public bool login(string username, string password)
        {
            User user = userRepository.findByUsername(username);
            this.user = user;
            return user != null && user.Password == password;
        }

        public bool add(string title, string content)
        {
            Email email = new Email(title, content);
            email.Username = user.Username;
            return emailRepository.save(email);
        }

        public IEnumerable<Email> getAll()
        {
            return emailRepository.getAll();
        }

        public IEnumerable<Email> searchByTitle(string title)
        {
            IEnumerable<Email> emails = emailRepository.getAll();
            List<Email> matchedEmails=new List<Email>();
            foreach(Email email in emails){
                if (email.Username.Contains(title))
                    matchedEmails.Add(email);
            }

            return matchedEmails;

        }

        public bool send(int id,string receiver)
        {
            Email email = emailRepository.find(id);
            if(email != null)
            {
                email.Status = Status.SENT;//adaug si la cine sa trimita
                email.Receiver = receiver;//verific daca exista user in repository!!
                emailRepository.update(email);
                return true;
            }
            return false;
        }

        internal Page<Email> getPage(int pageNr)
        {
            return emailRepository.getPage(pageNr);
        }

        public IEnumerable<Email> filterByStatus(string statusOption)
        {
            Status status;
            if (statusOption == "1")
            {
                status = Status.SENT;

            }
            else
            {
                status= Status.UNSENT;
            }

            IEnumerable<Email> emails = emailRepository.getAll();
            List<Email> matchedEmails = new List<Email>();
            foreach (Email email in emails)
            {
                if (email.Status==status)
                    matchedEmails.Add(email);
            }
            return matchedEmails;
        }

        public bool delete(int id)
        {
            return emailRepository.delete(id);
        }

        public bool edit(int id, string title, string content)
        {
            Email email = emailRepository.find(id);
            if (email != null&& email.Status==Status.UNSENT) { 
                email.Content = content;
                email.Username = title;
                return emailRepository.update(email);
            }
            return false;
        }
    }
}