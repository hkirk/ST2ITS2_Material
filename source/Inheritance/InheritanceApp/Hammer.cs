namespace InheritanceApp;

public class Hammer {
    protected int force;
    public Hammer(int force) {
        this.force = force;
    }
    
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {force}");
    }
}