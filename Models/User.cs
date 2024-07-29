namespace ExcelCrudMVC.Models{
    public class User
{
    public int UserID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

}