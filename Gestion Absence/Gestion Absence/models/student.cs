using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Absence.models
{
    class student
    {

        [PrimaryKey]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Num { get; set; }

        public int Nb_Absent { get; set; }

        public int Nb_Present { get; set; }
        public string module { get; set; }
        public override String ToString()
        {
            return FirstName + " " + LastName;
        }

    }
}
