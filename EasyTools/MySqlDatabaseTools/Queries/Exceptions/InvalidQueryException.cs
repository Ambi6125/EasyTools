using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    public class InvalidQueryException : Exception
    {
        private string message;

        public override string Message => message;

        public InvalidQueryException(string message)
        {
            this.message = message;
        }
    }
}
