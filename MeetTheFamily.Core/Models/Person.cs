using MeetTheFamily.Core.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace MeetTheFamily.Core.Models
{
    public class Person : IPerson
    {
        private List<IPerson> _childrens;
        private IPerson _spouse;

        private Person()
        {

        }
        private Person(string name, Gender gender, IPerson father, IPerson mother)
        {
            this.Name = name;
            this.Gender = gender;
            this.Father = father;
            this.Mother = mother;
            this._childrens = new List<IPerson>();
        }
        public string Name { get; }
        public Gender Gender { get; }
        public IPerson Father { get; }
        public IPerson Mother { get; }

        public IPerson Spouse
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
        public ReadOnlyCollection<IPerson> Childrens
        {
            get
            {
                return new ReadOnlyCollection<IPerson>(_childrens);
            }

        }
        public IPerson AddChild(string name, Gender gender)
        {
            if (this.Spouse == null)
                throw new Exception($"{this.Name} {ExceptionMessage.NO_CHILD_WITHOUT_SPOUSE}");
            IPerson father = ResolveFather();
            IPerson mother = ResolveMother();
            Person child = Person.Create(name, gender, father, mother);
            this.AddChild(child);
            return child;
        }

        public ReadOnlyCollection<IPerson> Sons
        {
            get
            {
                var sons = GetChildrenByGender(Gender.Male).ToList();
                return new ReadOnlyCollection<IPerson>(sons);
            }
        }

        public ReadOnlyCollection<IPerson> Daughters
        {
            get
            {
                var daughters = GetChildrenByGender(Gender.Female).ToList();
                return new ReadOnlyCollection<IPerson>(daughters);
            }
        }

        public ReadOnlyCollection<IPerson> Brothers
        {
            get
            {
                List<IPerson> brothers = GetBrothers().ToList();
                return new ReadOnlyCollection<IPerson>(brothers);
            }
        }

        private IEnumerable<IPerson> GetBrothers()
        {
            return GetSiblingsByGender(Gender.Male);
        }

        public ReadOnlyCollection<IPerson> Sisters
        {
            get
            {
                List<IPerson> sisters = GetSisters().ToList();
                if (sisters.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_SISTER}");
                return new ReadOnlyCollection<IPerson>(sisters);
            }
        }



        public ReadOnlyCollection<IPerson> GrandDaughters
        {
            get
            {
                var grandDaughters = GetDaughters().ToList();
                if (grandDaughters.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_GRANDDAUGHTER}");
                return new ReadOnlyCollection<IPerson>(grandDaughters);
            }
        }



        public ReadOnlyCollection<IPerson> Cousins
        {
            get
            {
                List<IPerson> cousins = GetCousins();
                if (cousins.Count == 0)
                    throw new Exception($"{this.Name} {ExceptionMessage.NO_COUSINS}");
                return new ReadOnlyCollection<IPerson>(cousins);
            }
        }



        public ReadOnlyCollection<IPerson> BrotherInLaw
        {
            get
            {
                List<IPerson> brotherInLaw = GetBrotherInLaw();
                return new ReadOnlyCollection<IPerson>(brotherInLaw);
            }
        }



        public ReadOnlyCollection<IPerson> SisterInLaw
        {
            get
            {
                List<IPerson> sisterInLaw = GetSisterInLaw();
                return new ReadOnlyCollection<IPerson>(sisterInLaw);
            }
        }



        public ReadOnlyCollection<IPerson> MaternalAunt
        {
            get
            {
                List<IPerson> maternalAunt = GetMaternalAunt().ToList();
                return new ReadOnlyCollection<IPerson>(maternalAunt);
            }
        }

        public ReadOnlyCollection<IPerson> PaternalAunt
        {
            get
            {
                var paternalAunt = GetPaternalAunt().ToList();
                return new ReadOnlyCollection<IPerson>(paternalAunt);
            }
        }

        public ReadOnlyCollection<IPerson> MaternalUncle
        {
            get
            {
                var maternalUncle = GetMaternalUncle().ToList();
                return new ReadOnlyCollection<IPerson>(maternalUncle);
            }
        }

        public ReadOnlyCollection<IPerson> PaternalUncle
        {
            get
            {
                var paternalAunt = GetPaternalUncle().ToList();
                return new ReadOnlyCollection<IPerson>(paternalAunt);
            }
        }

        public IEnumerable<IPerson> GetPaternalUncle()
        {
            var fatherBrothers = this.Father.Brothers;
            var fatherBrotherInLaw = this.Father.BrotherInLaw;

            var paternalUncle = fatherBrothers.Concat(fatherBrotherInLaw);
            return paternalUncle;
        }
        public IEnumerable<IPerson> GetMaternalUncle()
        {
            var motherBrothers = this.Mother.Brothers;
            var motherBrotherInLaw = this.Mother.BrotherInLaw;

            var maternalUncle = motherBrothers.Concat(motherBrotherInLaw);
            return maternalUncle;
        }
        private IEnumerable<IPerson> GetPaternalAunt()
        {
            var fatherSister = this.Father.Sisters;
            var fatherSisterInLaw = this.Father.SisterInLaw;
            var paternalAunt = fatherSister.Concat(fatherSisterInLaw);
            return paternalAunt;
        }
        private List<IPerson> GetBrotherInLaw()
        {
            var spouse = this.Spouse as Person;
            var spouseBrother = spouse.Brothers;
            var femaleSiblings = this.GetSiblingsByGender(Gender.Female);
            var husbandOfSiblings = femaleSiblings.Select(x => x.Spouse);
            var brotherInLaw = spouseBrother.Concat(husbandOfSiblings).ToList();
            return brotherInLaw;
        }
        private IEnumerable<IPerson> GetSisters()
        {
            return GetSiblingsByGender(Gender.Female);
        }
        private IEnumerable<IPerson> GetMaternalAunt()
        {
            var motherSisters = this.Mother.Sisters;
            var motherSisterInlaw = this.Mother.SisterInLaw;

            var maternalAunt = motherSisters.Concat(motherSisterInlaw);
            return maternalAunt;
        }

        private IEnumerable<IPerson> GetDaughters()
        {
            return this.Childrens.Where(x => x.Childrens.Count > 0).SelectMany(x => x.Daughters);
        }
        private IEnumerable<IPerson> GetChildrenByGender(Gender gender)
        {
            return this.Childrens.Where(x => x.Gender == gender);
        }
        private IEnumerable<IPerson> GetSiblingsByGender(Gender gender)
        {
            return this.GetSiblings().Where(x => x.Gender == gender);
        }

        private List<IPerson> GetSiblings()
        {
            var siblings = this.Mother.Childrens.ToList();
            siblings.Remove(this);
            return siblings;
        }
        private List<IPerson> GetSisterInLaw()
        {
            var spouse = this.Spouse as Person;
            var spouseSister = spouse.GetSiblingsByGender(Gender.Female);
            var maleSiblings = this.GetSiblingsByGender(Gender.Male);
            var wiviesOfSiblings = maleSiblings.Select(x => x.Spouse);
            var brotherInLaw = spouseSister.Concat(wiviesOfSiblings).ToList();
            return brotherInLaw;
        }

        private List<IPerson> GetCousins()
        {
            return (this.Father as Person).GetSiblings().SelectMany(x => x.Childrens).ToList();
        }

        private void AddChild(IPerson child)
        {
            var spouse = this.Spouse as Person;
            var current = this as Person;
            if (!current.IsChildExists(child))
                _childrens.Add(child);
            if (!spouse.IsChildExists(child))
                spouse.AddChild(child);
        }
        private IPerson ResolveMother()
        {
            return (this.Gender == Gender.Female) ? this : this.Spouse;
        }

        private IPerson ResolveFather()
        {
            return (this.Gender == Gender.Male) ? this : this.Spouse;
        }

        private bool IsChildExists(IPerson child)
        {
            return this.Childrens.Contains(child);
        }
        public static Person Create(string name, Gender gender, IPerson father, IPerson mother)
        {
            return new Person(name, gender, father, mother);
        }
        public static Person Create(string name, Gender gender)
        {
            return new Person(name, gender, null, null);
        }
    }
}