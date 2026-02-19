using System.Runtime;

class Question
{
    private string AskQuestion;
    private string CorrectAnswer;

    public Question(string text, string answer)
    {
        AskQuestion = text;
        CorrectAnswer = answer;
    }
    public bool askquestion()
    {
        Console.WriteLine(AskQuestion);
        Console.Write("Your answer: ");
        string inputanswer = Console.ReadLine();
        if (inputanswer != null && inputanswer.ToLower() == CorrectAnswer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}