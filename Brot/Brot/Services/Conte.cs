using System;
using System.Collections.Generic;
using System.Text;

namespace Brot.Services
{
    public class Conte
    {
        public Conte()
        {
            Name = "default";   //By default cannot be empty, must have at least 3 characters
        }
        public string Name { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Dictionary<string, string> CustomData { get; set; }
    }
}
