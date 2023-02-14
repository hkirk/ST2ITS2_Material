namespace ExceptionExamples;

public class Faulty
{
    public List<string> strings { get; set; }

    public string GetShortestString()
    {
        string shortest = null;
        while (!IsLastString())
        {
            string next = GetNextString();
            if (shortest == null || shortest.Length > next.Length)
            {
                shortest = next;
            }
        }

        return shortest;
    }

    private int counter = 0;
    private bool IsLastString()
    {
        if (counter < strings.Count)
        {
            return false;
        }

        return true;
    }

    private string GetNextString()
    {
        string next = strings[counter];
        counter++;
        return next;
    }

    private void Reset()
    {
        counter = 0;
    }


    public void ReadFileIntoString(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Filnenmame is empty");
        }
        string[] readAllLines = File.ReadAllLines(name);
        strings = readAllLines.ToList();
    }
}