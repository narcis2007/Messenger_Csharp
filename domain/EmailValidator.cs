using email_client.data.domain;
using System;

namespace email_client.domain
{
    public class EmailValidator : Validator<Email>
    {
        public Errors validate(Email email)
        {
            Errors errors = new Errors();

            if (email.Username == null || email.Username == "")
                errors.add("title is null");

            if (email.Content == null || email.Content == "")
                errors.add("content is null");

            if (email.Username == null || email.Username == "")
                errors.add("an email must belong to someone");

            return errors;
        }

        /*internal bool isValid(Email email)
        {
            if (email != null && email.Title != null && email.Title != "" && email.Content != null && email.Content != "")//folosesc utils
                return true;
            return false;
        }*/
    }
}