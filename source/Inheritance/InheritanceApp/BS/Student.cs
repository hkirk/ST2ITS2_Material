namespace InheritanceApp.BS;

public class Student : Person {
    public Student(string auid, string name): base(auid) {
        base.name = name;
    }
    public override string GetName() {
        return base.name;
    }
}