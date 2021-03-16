using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Absence.models
{
    class module
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
