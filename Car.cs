using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nphw3client
{
    public class Car
    {
        public string? Mark { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public override string ToString()
        {
            return $"{Mark} - {Model} ({Year})";
        }
    }
}
