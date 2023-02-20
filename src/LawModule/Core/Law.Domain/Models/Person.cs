using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Law.Domain.Models
{
    public class Person : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool IsDelete { get; set; }



    }
}
