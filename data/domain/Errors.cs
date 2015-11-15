using System;
using System.Collections.Generic;

namespace email_client.data.domain
{
    public class Errors
    {
        List<string> errors;

        public Errors()
        {
            errors = new List<string>();
        }

        public int count()
        {
            return errors.Count;
        }

        public void add(string v)
        {
            errors.Add(v);
        }

        public IEnumerable<string> getAll()
        {
            return errors;
        }
    }
}