namespace EfCoreDemo.API.Vo;

public class UserVo
{
    public string Username { get; set; }
    public string Password { get; set; }
    public UserVo() { }

    public UserVo(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }
}
