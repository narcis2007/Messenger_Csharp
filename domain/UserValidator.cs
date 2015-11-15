using System;
using email_client.data.domain;

namespace email_client.domain
{
    public class UserValidator : Validator<User>
    {
        public Errors validate(User user)
        {
            Errors errors = new Errors();

            if (user == null )
                errors.add("email is null");

            if (user.Username == null|| user.Username == "")
                errors.add("username must not be empty");

            if (user.Password == null || user.Password == "")
                errors.add("password must not be empty");
            return errors;
        }
    }
}