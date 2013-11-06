using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib.Avito
{
    public class Avito
    {
        public string Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// Продавец Name+(компания, агенство и т.д)
        /// </summary>
        public string NameContact { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        /// <summary>
        /// категория
        /// </summary>
        public string Section { get; set; }
        public string Sort { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public string CountShow { get; set; }
        public string Url { get; set; }
    }
}
