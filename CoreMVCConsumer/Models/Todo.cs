using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCConsumer.Models
{
    public class Todo
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
