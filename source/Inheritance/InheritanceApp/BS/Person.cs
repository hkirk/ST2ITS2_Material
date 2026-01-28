namespace InheritanceApp.BS;

public abstract class Person {
    protected string name;
    private string auid;
    protected Person(string auid) {
        this.auid = auid;
    }
    public abstract string GetName();
}