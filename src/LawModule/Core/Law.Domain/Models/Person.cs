using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Law.Domain.Models
{
    public class Person : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public override Guid Id { get; set; }
        [JsonIgnore]
        public override DateTime CreatedDate { get ; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public bool IsDelete { get; set; }



    }
}
