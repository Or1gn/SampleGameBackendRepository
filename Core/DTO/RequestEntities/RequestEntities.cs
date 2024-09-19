namespace Core.DTO.RequestEntities
{
    public class RequestEntities
    {
        public record UserLoginAndRegisterRequest(string name, string password);
    }
}
