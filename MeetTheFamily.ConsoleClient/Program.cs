using MeetTheFamily.Core.Models;
using MeetTheFamily.Core.Constants;
using System;
using System.Collections.ObjectModel;

namespace MeetTheFaimily.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            // var king = Person.Create("King Shan", Gender.Male);
            // var queen = Person.Create("Queen Anga", Gender.Female);
            // king.Spouse = queen;
            // var ish = king.AddChild("Ish", Gender.Male);
            // var chit = king.AddChild("Chit", Gender.Male);
            // var vich = king.AddChild("Vich", Gender.Male);
            // var satya = king.AddChild("Satya", Gender.Female);
            // ChitTree(chit);
            // VichTree(vich);
            // SatyaTree(satya);
            // FamilyTree familyTree = FamilyTree.Create(king);

            // Console.WriteLine("Hello World!");
            // //Console.WriteLine(familyTree.FindByName("misa").Name);
            // var result = familyTree.FindPeoplesByRelationship("driya","brothers");
            // PrintName(result);
        }

        // private static void PrintName(ReadOnlyCollection<Person> peoples)
        // {
        //     foreach(var person in peoples)
        //     {
        //         Console.WriteLine(person.Name);
        //     }
        // }
        // private static void SatyaTree(Person satya)
        // {
        //     satya.SetSpouse(Person.Create("Vyan", Gender.Male));

        //     satya.AddChild("Satvy", Gender.Female)
        //     .SetSpouse(Person.Create("Asva", Gender.Male));

        //     satya.AddChild("Savya", Gender.Male)
        //     .SetSpouse(Person.Create("Krpi", Gender.Female))
        //     .AddChild("Kriya", Gender.Male);

        //     satya.AddChild("Saayan", Gender.Male)
        //     .SetSpouse(Person.Create("Mina", Gender.Female))
        //     .AddChild("Misa", Gender.Male);
        // }

        // private static void VichTree(Person vich)
        // {
        //     var lika = Person.Create("Lika", Gender.Female);
        //     vich.Spouse = lika;

        //     var vila = vich.AddChild("Vila", Gender.Male);
        //     var chika = vich.AddChild("Chika", Gender.Female);

        //     var jnki = Person.Create("Jnki", Gender.Female);
        //     vila.Spouse = jnki;

        //     var lavnya = vila.AddChild("Lavnya", Gender.Female);
        //     lavnya.Spouse = Person.Create("Gru", Gender.Male);

        //     chika.Spouse = Person.Create("Kpila", Gender.Male);

        // }
        // private static void ChitTree(Person chit)
        // {
        //     var ambi = Person.Create("Ambi", Gender.Female);
        //     chit.Spouse = ambi;
        //     var drita = chit.AddChild("Drita", Gender.Male);
        //     var vrita = chit.AddChild("Vrita", Gender.Male);

        //     var jaya = Person.Create("Jaya", Gender.Female);
        //     drita.Spouse = jaya;
        //     var jata = drita.AddChild("Jata", Gender.Male);
        //     var driya = drita.AddChild("Driya", Gender.Female);

        //     var mnu = Person.Create("Mnu", Gender.Male);
        //     driya.Spouse = mnu;
        // }
    }
}
