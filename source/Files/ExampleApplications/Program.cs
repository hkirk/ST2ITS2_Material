// See https://aka.ms/new-console-template for more information

using System.IO.Compression;
using System.Text;
using FilesEx;

Console.WriteLine("Hello, World!");


Streams.WriteNumbersToStreams("test.txt");
Streams.WriteZippedToStreams("test.txt.gz");
Streams.WriteLinesToStream("test2.txt");

Streams.ReadLinesFromStream("test2.txt");

Streams.WriteBinaryToFile("test.bin");
Streams.ReadBinaryData("test.bin");

/* ------ Encoding ------ */

Console.WriteLine(Encoding.Default);

byte[] bytes = Encodings.ConvertToUtf8("Hello world!");
string unicode = Encodings.ConvertFromUnicode(bytes);

Console.WriteLine(unicode);

/* ------ Encoding ------ */

var patient = new Patient()
{
    FirstName = "Lars",
    LastName = "Larsen",
    Status = Status.Deceased,
    BirthDate = DateTime.Now
};

string json = Serializations.SerializeAsJSon(patient);
Console.WriteLine(json);


var patients = new List<Patient>()
{
    patient
};
string patientsJson = Serializations.SerializeAsJSon(patients);

Console.WriteLine(patientsJson);