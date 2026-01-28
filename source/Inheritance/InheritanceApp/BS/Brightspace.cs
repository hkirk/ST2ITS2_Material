namespace InheritanceApp.BS;

public class Brightspace {
    private List<Person> _persons = new List<Person>();
    
    public void addPerson(Person person) {
        _persons.Add(person);
    }
    
    public void PrintPersons() {
        foreach (Person person in _persons) {
            Console.WriteLine(person.GetName());
        }
    }
}