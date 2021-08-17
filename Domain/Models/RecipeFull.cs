﻿using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RecipeFull
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Difficulty Difficulty { get; set; }

        public TimeSpan TimeToComplete { get; set; }

        public DateTime DateCreated { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Difficulty} {Description} {TimeToComplete}";
        }
    }
}
