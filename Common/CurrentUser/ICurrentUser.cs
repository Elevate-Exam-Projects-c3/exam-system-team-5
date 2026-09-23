namespace exam_system.Common.CurrentUser
{
    public interface ICurrentUser 
    {
        Guid UserId { get; }
        //Guid? StudentId { get; }
        string? Email { get; }
        string? Role { get; }
        bool IsAuthenticated { get; }
    }
}
