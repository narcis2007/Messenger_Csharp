using System;
using email_client.service;
using email_client.domain;
using System.Collections.Generic;
using email_client.data.repository;

namespace email_client.ui
{
    internal class ConsoleUi
    {
        private Service service;

        public ConsoleUi(Service service)
        {
            this.service = service;
        }

        public void run()
        {
            if (authenticate())
            {
                Console.WriteLine("successful authentification \n");
                while (true)
                {
                    showOptions();
                    string option = readOption();
                    switch (option)
                    {

                        case "1":
                             add();
                            break;
                        case "2":
                            show_all();
                            break;
                        case "3":
                            searchByTitle();
                            break;
                        case "4":
                            edit();
                            break;
                        case "5":
                            delete();
                            break;
                        case "6":
                            send();
                            break;
                        case "7":
                            filterByStatus();
                            break;
                        case "8":
                            showPage();
                            break;

                    }
                }
            }
            else
            {
                Console.WriteLine("wrong user");
                Console.ReadKey();
            }

        }

        private void showPage()
        {
            Console.WriteLine("page number:");
            int pageNr= Convert.ToInt32(Console.ReadLine());
            Page<Email> page = service.getPage(pageNr);
            if (page != null)
                printEmails(page.getElements());
            else
                Console.Out.WriteLine("invalid page");
        }

        private void filterByStatus()
        {
            Console.WriteLine("1-sent");
            Console.WriteLine("2-unsend");
            string status = Console.ReadLine();
            IEnumerable<Email> emails = service.filterByStatus(status);
            printEmails(emails);
        }

        private void send()
        {
            Console.WriteLine("id email:");
            int id = Convert.ToInt32(Console.ReadLine());//adaug si la cine sa trimita
            Console.WriteLine("send to:");
            string receiver = Console.ReadLine();

            if (service.send(id,receiver) == true)
            {
                Console.WriteLine("sent");
            }
            else
            {
                Console.WriteLine("could not send the email");
            }
        }

        private void delete()
        {
            Console.WriteLine("id email:");
            int id = Convert.ToInt32(Console.ReadLine());
            if (service.delete(id) == true)
            {
                Console.WriteLine("deleted");
            }
            else
            {
                Console.WriteLine("could not delete the email");
            }
        }

        private void edit()
        {
            Console.WriteLine("id email:");
            int id = Convert.ToInt32( Console.ReadLine());
            Console.WriteLine("title:");
            string title = Console.ReadLine();
            Console.WriteLine("content:");
            string content = Console.ReadLine();
            try
            {
                if (service.edit(id, title, content) == true)
                {
                    Console.WriteLine("edited");
                }
                else
                {
                    Console.WriteLine("the email could not be edited");
                }
            }
            catch (ValidationException e)
            {
                printErrors(e);
            }
        }

        private void searchByTitle()
        {
            Console.WriteLine("title:");
            string title = Console.ReadLine();
            IEnumerable<Email> emails = service.searchByTitle(title);
            printEmails(emails);
        }

        private void show_all()
        {
            IEnumerable<Email> emails = service.getAll();
            printEmails(emails);
            
        }

        private void printEmails(IEnumerable<Email> emails)
        {   foreach (Email email in emails)
            {
                Console.WriteLine("id:{0}", email.Id);
                Console.WriteLine("title:{0}", email.Title);
                Console.WriteLine("content:{0}", email.Content);
                Console.WriteLine("status:{0}", email.Status);
                Console.WriteLine("user:{0}", email.Username);
                Console.WriteLine("receiver:{0}", email.Receiver);
                Console.WriteLine("-------------------");
            }
        }

        private void add()
        {
            Console.WriteLine("title:");
            string title = Console.ReadLine();
            Console.WriteLine("content:");
            string content = Console.ReadLine();
            try
            {
                if (service.add(title, content) == true)
                    Console.WriteLine("added");
                else
                    Console.WriteLine("the email could not be added");
            }
            catch (ValidationException e)
            {
                printErrors(e);
            }
        }

        private static void printErrors(ValidationException e)
        {
            foreach (string error in e.getErrors().getAll())
            {
                Console.Out.WriteLine(error);
            }
        }

        private static string readOption()
        {
            return Console.ReadLine();
        }

        private void showOptions()
        {
            Console.WriteLine("1-add email");
            Console.WriteLine("2-show all emails");
            Console.WriteLine("3-filter by title");
            Console.WriteLine("4-edit unsent email");
            Console.WriteLine("5-delete email");
            Console.WriteLine("6-send email");
            Console.WriteLine("7-filter by status");
            Console.WriteLine("8-show page");
        }

        private bool authenticate()
        {
            string username, password;
            Console.WriteLine("username:");
            username = Console.ReadLine();
            Console.WriteLine("password:");
            password = Console.ReadLine();
            return service.login(username, password);
        }
    }
}