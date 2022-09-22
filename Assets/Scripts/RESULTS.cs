using System;

[Serializable]
public class RESULTS
{
    public string timer;
    public int score;
    public int counter;

    // This is the Default Constructor
    public RESULTS()
    {

    }

    // This is my Constructor
    public RESULTS(string timer, int score, int counter)
    {
        this.timer = timer;
        this.score = score;
        this.counter = counter;
    }
}
