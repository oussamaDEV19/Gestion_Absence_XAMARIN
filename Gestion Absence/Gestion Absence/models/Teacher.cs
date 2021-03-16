using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Absence.models
{
    class Teacher
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Num { get; set; }
        public string Password { get; set; }

    }
}
