using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using email_client.repository;
using email_client.domain;
using email_client.service;
using email_client.ui;
namespace email_client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EmailValidator emailValidator = new EmailValidator();
                EmailRepository emailRepository = new EmailRepository(emailValidator, "emails.xml");

                UserValidator userValidator = new UserValidator();
                UserRepository userRepository = new UserRepository(userValidator,"users.xml");
                //userRepository.save(new User("admin","admin"));
                //userRepository.save(new User("narcis", "narcis"));
                Service service = new Service(emailRepository, userRepository);

                ConsoleUi console = new ConsoleUi(service);
                console.run();
            }
            catch (Exception e)
            {
                Console.WriteLine("something went wrong {0}",e.Message);
                Console.ReadKey();
            }

        }
    }
}
