using System;

namespace email_client.domain
{
    public class Email
    {
        private string content;
        private int id;
        private Status status;
        private string title;
        private string username;//adaug si receiver
        private string receiver;

        public Email(string title, string content)
        {
            this.title = title;
            this.content = content;
            this.Status = Status.UNSENT;
        }

        public string Content {
            get {return content; }
            set {content=value; }
        }

        public int Id { get {return id; } set {id=value; } }

        public Status Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public static Email copy(Email email)
        {
            Email emailCopy = new Email(email.title, email.Content);
            emailCopy.Status = email.Status;
            emailCopy.Id = email.Id;
            emailCopy.Username = email.Username;
            return email;
        }

        public string Username {
            get { return this.username;  }
            set { this.username = value; }
        }


        public string Receiver { get {return receiver; }  set {receiver=value; } }

        public string Title { get { return title; } internal set {this.title=value; } }
    }
}