namespace InheritanceApp;

public class BrickLayer : Hammer {
    public BrickLayer() : base(10)
    { }
    
    public void BreakBrick(Brick brick) {
        HitWithForce(brick);
    }
    
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {force} with "
                          + "a brick hammer");
    }
    
    private Hammer _hammer;
}