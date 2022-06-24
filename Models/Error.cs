using System.Collections.Generic;

namespace IsIoTWeb.Models
{
    public class Error
    {
        public Error()
        {
            ErrorMessages = new List<string>();
        }
        public List<string> ErrorMessages { get; set; }
    }
}
