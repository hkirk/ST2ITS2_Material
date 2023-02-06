using System.IO.Compression;

namespace FilesEx;

public class Streams
{
    public static void WriteNumbersToStreams(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(fs);

        for (int i = 0; i < 100; i++)
        {
            streamWriter.Write(i);
        }
        
        streamWriter.Close();
    }
    
    public static void WriteZippedToStreams(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Create);
        var gZipStream = new GZipStream(fs, CompressionLevel.Fastest);
        StreamWriter streamWriter = new StreamWriter(gZipStream);

        for (int i = 0; i < 100; i++)
        {
            streamWriter.Write(i);
        }
        
        streamWriter.Close();
    }
    
    public static void WriteLinesToStream(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(fs);

        for (int i = 0; i < 100; i++)
        {
            streamWriter.WriteLine($"Number is {i}");
        }
        
        streamWriter.Close();
    }

    public static void WriteBinaryToFile(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Create);
        var binaryWriter = new BinaryWriter(fs);

        binaryWriter.Write("Hello World!");
        binaryWriter.Write(42);
        binaryWriter.Write('H');
        
        binaryWriter.Close();
    }

    public static void ReadBinaryData(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Open);
        BinaryReader binaryReader = new BinaryReader(fs);

        string readString = binaryReader.ReadString();
        int readInt32 = binaryReader.ReadInt32();
        var readChar = binaryReader.ReadChar();
        
        Console.WriteLine("'{0}', '{1}', '{2}'", readString, readInt32, readChar);
        
        binaryReader.Close();
    }

    public static void ReadLinesFromStream(string filename)
    {
        FileStream fs = new FileStream(filename, FileMode.Open);
        StreamReader streamReader = new StreamReader(fs);

        while (!streamReader.EndOfStream)
        {
            string? line = streamReader.ReadLine();
            Console.WriteLine(line);
        }
        streamReader.Close();
    }
}