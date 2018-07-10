using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VCSoftware.Dao
{
    public class EntityField
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool IsKey { get; set; }
    }
}
