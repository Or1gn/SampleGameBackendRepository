using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Interfaces
{
    [DataContract, Table("user")]
    public record User([property: Key, DataMember, Column("id")] Guid id,
                       [property: DataMember, Column("name")] string name,
                       [property: DataMember, Column("password")] string password)
    {
        public ICollection<PlayerInfo> Players { get; init; }       
    }
    public interface IAuthenticationUserService   
    {
        User RegisterUser(string name, string password);
        User? LoginUser(string name, string password);
    }


}
