using MeetTheFamily.Core.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace MeetTheFamily.Core.Models
{
    public class Person
    {
        private List<Person> _childrens;
        private Person _spouse;

        private Person(string name, Gender gender, Person father, Person mother)
        {
            this.Name = name;
            this.Gender = gender;
            this.Father = father;
            this.Mother = mother;
            this._childrens = new List<Person>();
        }
        public string Name { get; }
        public Gender Gender { get; }
        public Person Father { get; }
        public Person Mother { get; }
        public Person Spouse
        {
            get
            {
                return _spouse;
            }
            set
            {
                if (this.Gender == value.Gender)
                    throw new Exception(ExceptionMessage.SAME_GENDER_FOR_SPOUSE);
                _spouse = value;
                if (value.Spouse == null)
                    value.Spouse = this;
            }
        }

        public Person SetSpouse(Person spouse)
        {
            Spouse = spouse;
            return this;
        }
        public ReadOnlyCollection<Person> Childrens
        {
            get
            {
                if (_childrens.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_CHILDREN}");
                return new ReadOnlyCollection<Person>(_childrens);
            }
        }
        public Person AddChild(string name, Gender gender)
        {
            if (this.Spouse == null)
                throw new Exception($"{this.Name} {ExceptionMessage.NO_CHILD_WITHOUT_SPOUSE}");
            Person father = ResolveFather();
            Person mother = ResolveMother();
            Person child = Person.Create(name, gender, father, mother);
            this.AddChildren(child);
            return child;
        }

        public ReadOnlyCollection<Person> Sons
        {
            get
            {
                var sons = GetChildrenByGender(Gender.Male);
                return new ReadOnlyCollection<Person>(sons);
            }
        }

        public ReadOnlyCollection<Person> Daughters
        {
            get
            {
                var daughters = GetChildrenByGender(Gender.Female);
                return new ReadOnlyCollection<Person>(daughters);
            }
        }

        public ReadOnlyCollection<Person> Brothers
        {
            get
            {
                List<Person> brothers = GetSiblingsByGender(Gender.Male);
                if (brothers.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_BROTHER}");
                return new ReadOnlyCollection<Person>(brothers);
            }
        }
        public ReadOnlyCollection<Person> Sisters
        {
            get
            {
                List<Person> sisters = GetSiblingsByGender(Gender.Female);
                if (sisters.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_SISTER}");
                return new ReadOnlyCollection<Person>(sisters);
            }
        }

        public ReadOnlyCollection<Person> GrandDaughters
        {
            get
            {
                var grandDaughters = this.Childrens.Where(x => x._childrens.Count > 0).SelectMany(x => x.Daughters).ToList();
                if (grandDaughters.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_GRANDDAUGHTER}");
                return new ReadOnlyCollection<Person>(grandDaughters);
            }
        }

        public ReadOnlyCollection<Person> Cousins
        {
            get
            {
                var cousins = this.Father.GetSiblings().SelectMany(x => x._childrens).ToList();
                if (cousins.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_COUSINS}");
                return new ReadOnlyCollection<Person>(cousins);
            }
        }
        private List<Person> GetChildrenByGender(Gender gender)
        {
            return this.Childrens.Where(x => x.Gender == gender).ToList();
        }
        private List<Person> GetSiblingsByGender(Gender gender)
        {
            List<Person> siblings = new List<Person>();
            siblings = this.GetSiblings().Where(x => x.Gender == gender).ToList();
            return siblings;
        }
        private List<Person> GetSiblings()
        {
            var siblings = this.Mother.Childrens.ToList();
            siblings.Remove(this);
            return siblings;
        }

        private void AddChildren(Person person)
        {
            if (!IsChildExists(person))
                _childrens.Add(person);
            if (!this.Spouse.IsChildExists(person))
                this.Spouse.AddChildren(person);
        }
        private Person ResolveMother()
        {
            return (this.Gender == Gender.Female) ? this : this.Spouse;
        }

        private Person ResolveFather()
        {
            return (this.Gender == Gender.Male) ? this : this.Spouse;
        }

        private bool IsChildExists(Person person)
        {
            return _childrens.Exists(x => x.Name == person.Name);
        }
        public static Person Create(string name, Gender gender, Person father, Person mother)
        {
            return new Person(name, gender, father, mother);
        }
        public static Person Create(string name, Gender gender)
        {
            return new Person(name, gender, null, null);
        }
    }
}