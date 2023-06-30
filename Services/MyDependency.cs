namespace CustomerDemo.Services;

public class MyDependency : IMyDependency
{
    public int GetNumber()
    {
        return 23;
    }

    public string GetText()
    {
        return "Hello from MyDependency!";
    }
}