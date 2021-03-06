﻿using System;

namespace WebApiRest.NetCore.Domain.Models
{
    public class AuthorizationModel
    {
        public int IdRole { get; set; }
        public int IdMenu { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}