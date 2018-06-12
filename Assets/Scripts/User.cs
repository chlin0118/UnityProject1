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

    public int correctBy1TimeInType1;
    public int correctBy2TimesInType1;
    public int correctBy3TimesInType1;
    public int correctBy4TimesInType1;
    public int totalProblemsInType1;
    public float correctRateInType1;

    public int correctBy1TimeInType2;
    public int correctBy2TimesInType2;
    public int totalProblemsInType2;
    public float correctRateInType2;

    public int correctBy1TimeInType3;
    public int correctBy2TimesInType3;
    public int correctBy3TimesInType3;
    public int correctBy4TimesInType3;
    public int totalProblemsInType3;
    public float correctRateInType3;

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

    public void setUserInType1(int correctBy1TimeInType1, int correctBy2TimesInType1, int correctBy3TimesInType1, int correctBy4TimesInType1,
        int totalProblemsInType1, float correctRateInType1)
    {
        this.correctBy1TimeInType1 = correctBy1TimeInType1;
        this.correctBy2TimesInType1 = correctBy2TimesInType1;
        this.correctBy3TimesInType1 = correctBy3TimesInType1;
        this.correctBy4TimesInType1 = correctBy4TimesInType1;
        this.totalProblemsInType1 = totalProblemsInType1;
        this.correctRateInType1 = correctRateInType1;
    }

    public void setUserInType2(int correctBy1TimeInType2, int correctBy2TimesInType2, int totalProblemsInType2, float correctRateInType2)
    {
        this.correctBy1TimeInType2 = correctBy1TimeInType2;
        this.correctBy2TimesInType2 = correctBy2TimesInType2;
        this.totalProblemsInType2 = totalProblemsInType2;
        this.correctRateInType2 = correctRateInType2;
    }

    public void setUserInType3(int correctBy1TimeInType3, int correctBy2TimesInType3, int correctBy3TimesInType3, int correctBy4TimesInType3,
        int totalProblemsInType3, float correctRateInType3)
    {
        this.correctBy1TimeInType3 = correctBy1TimeInType3;
        this.correctBy2TimesInType3 = correctBy2TimesInType3;
        this.correctBy3TimesInType3 = correctBy3TimesInType3;
        this.correctBy4TimesInType3 = correctBy4TimesInType3;
        this.totalProblemsInType3 = totalProblemsInType3;
        this.correctRateInType3 = correctRateInType3;
    }
}