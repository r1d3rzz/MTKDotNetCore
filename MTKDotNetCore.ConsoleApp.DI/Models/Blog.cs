﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp.DI.Models
{
    [Table("Blog")]
    internal class Blog
    {
        [Key]
        public int BlogId {get; set;}

        public string BlogTitle { get; set; }

        public string BlogAuthor { get; set; }

        public string BlogContent { get; set; }

        public bool DeleteFlag { get; set; }
    }
}
