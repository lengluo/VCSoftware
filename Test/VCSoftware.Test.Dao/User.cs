using System.ComponentModel.DataAnnotations.Schema;
using VCSoftware.Dao;
using VCSoftware.Dao.Repository;
using Xunit;

namespace VCSoftware.Test.Dao
{
    [Table("Sys_User")]
    public class User : BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }

        [NotMapped]
        public string Nothing { get; set; }
    }
}
