// using System;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;

// namespace MeetTheFamily.Core.Models
// {
//     public class FamilyTree
//     {
//         private Person Root { get; set; }

//         private FamilyTree(Person tree)
//         {
//             this.Root = tree;
//         }

//         public Person FindByName(string name)
//         {
//             Person person = FindByNameRecursively(this.Root, name);
//             if (person == null)
//                 throw new Exception("Person not found");
//             return person;
//         }

//         public ReadOnlyCollection<Person> FindPeoplesByRelationship(string name, string relationship)
//         {
//             Person person = FindByName(name);

//             switch (relationship)
//             {
//                 case "brothers":
//                     return person.Brothers;
//                 default:
//                 throw new Exception("No such relationship exist.");
//             }
//         }
       

//         private Person FindByNameRecursively(Person person, string name)
//         {
//             if (person.Name.ToLower() == name.ToLower())
//                 return person;
//             if (person.Spouse != null && person.Spouse.Name.ToLower() == name.ToLower())
//                 return person.Spouse;

//             Person foundPerson = null;
//             foreach (Person child in person.Childrens)
//             {
//                 foundPerson = FindByNameRecursively(child, name);
//                 if (foundPerson != null)
//                     break;
//             }
//             return foundPerson;
//         }
//         public static FamilyTree Create(Person tree)
//         {
//             return new FamilyTree(tree);
//         }
//     }
// }