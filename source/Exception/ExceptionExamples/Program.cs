// See https://aka.ms/new-console-template for more information

using ExceptionExamples;

Console.WriteLine("Hello, World!");

Faulty faulty = new Faulty();

faulty.ReadFileIntoString("NotExistingFile.txt");

// Fails with NullReferenceException - why?
string shortestString = faulty.GetShortestString();
Console.WriteLine(shortestString);

try
{
    faulty.ReadFileIntoString("NotExistingFile.txt");
} catch (FileNotFoundException e)
{
    Console.Error.WriteLine($"File '{e.FileName}' do not exists");
    Console.Error.WriteLine(e.Message);
}

try
{
    faulty.ReadFileIntoString("NotExistingFile.txt");
}
catch (FileNotFoundException e)
{
    Console.Error.WriteLine($"File '{e.FileName}' do not exists");
}
catch (ArgumentException)
{
    Console.Error.WriteLine("Filename supplied is empty, retry");
}

var filename = "NotExistingFile.txt";
try
{
    faulty.ReadFileIntoString(filename);
} catch (Exception e)
{
    Console.Error.WriteLine($"File '{filename}' do not exists");
    Console.Error.WriteLine(e.Message);
}


MemoryStream stream = null;
try
{
    stream = new MemoryStream();
    faulty.ReadFileIntoString("NotExistingFile.txt");
}
catch (FileNotFoundException e)
{
    Console.Error.WriteLine($"File '{e.FileName}' do not exists");
    Console.Error.WriteLine(e.Message);
}
finally
{
    if (stream != null)
        stream.Close();
}


try
{
    stream = new MemoryStream();
    faulty.ReadFileIntoString("NotExistingFile.txt");
}
finally
{
        stream.Dispose();
}

try
{
    TestEnoughCoffee();
} catch (NoCoffeeException){}

try
{
    TestEnoughCoffee();
}
catch (NoCoffeeException)
{
    Console.Error.WriteLine("Cant handle coffee problems here");
    throw;
}




void TestEnoughCoffee()
{
    throw new NoCoffeeException("No coffee at all", 100);
}
