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

    public int totalProblems;
    public float correctRate;

    public User()
    {
    }

    public User(string username, string email, int number)
    {
        this.username = username;
        this.email = email;
        this.number = number;
    }

    public User(string username, int number, int playerStage, int totalPlayedTime, int correctBy1Time, int totalProblems, float correctRate)
    {
        this.username = username;
        this.number = number;
        this.playerStage = playerStage;
        this.totalPlayedTime = totalPlayedTime;
        this.correctBy1Time = correctBy1Time;
        this.totalProblems = totalProblems;
        this.correctRate = correctRate;
    }
}