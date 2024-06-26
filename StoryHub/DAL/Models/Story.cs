﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public string? ImageName { get; set; }

        public StoryTeller? Teller { get; set; }

        public int? TellerId { get; set; }
    }
}
