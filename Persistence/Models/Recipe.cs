using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Difficulty Difficulty { get; set; }

        public TimeSpan TimeToComplete { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
