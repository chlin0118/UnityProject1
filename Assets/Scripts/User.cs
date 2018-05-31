public class User
{
    public string username;
    public string email;
    public int number;

    public int playerStage;
    public int totalPlayedTime;
    public int correctBy1Time;
    public int correctBy2Times;
    public int correctBy3Times;
    public int correctBy4Times;

    public User()
    {
    }

    public User(string username, string email, int number)
    {
        this.username = username;
        this.email = email;
        this.number = number;
    }
}