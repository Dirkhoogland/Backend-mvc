﻿using System;
using System.Collections.Generic;

namespace Backend_mvc.Models.Database
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public int Lijst { get; set; }
        public string? Naam { get; set; }
        public string? Beschrijving { get; set; }
        public int? Duur { get; set; }
        public string? Status { get; set; }

        public virtual lijstentable LijstNavigation { get; set; } = null!;
    }
}
