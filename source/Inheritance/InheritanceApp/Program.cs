namespace InheritanceApp;

using InheritanceApp.Bricks;

public class Program
{
    public static void Main(string[] args)
    {
        Hammer hammer = new BrickLayer();
        hammer.HitWithForce(new Brick());
        
        BrickLayer brickLayer = new BrickLayer();
        brickLayer.HitWithForce(new Brick());
    }
}