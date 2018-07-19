using System.Collections.Generic;
using System.Collections.ObjectModel;
using MeetTheFamily.Core.Constants;

namespace MeetTheFamily.Core.Models
{
    public class NullablePerson : IPerson
    {
        ReadOnlyCollection<IPerson> empty;
        NullablePerson()
        {
            empty = new ReadOnlyCollection<IPerson>(new List<IPerson>());
        }

        public string Name => "Null";

        public Gender Gender => Gender.Null;

        public IPerson Father => this;

        public IPerson Mother => this;

        public IPerson Spouse { get ; set; }

        public ReadOnlyCollection<IPerson> Childrens => empty;

        public ReadOnlyCollection<IPerson> Sons => empty;

        public ReadOnlyCollection<IPerson> Daughters => empty;

        public ReadOnlyCollection<IPerson> Brothers => throw new System.NotImplementedException();

        public ReadOnlyCollection<IPerson> Sisters => throw new System.NotImplementedException();

        public ReadOnlyCollection<IPerson> GrandDaughters => throw new System.NotImplementedException();

        public ReadOnlyCollection<IPerson> Cousins => throw new System.NotImplementedException();

        public ReadOnlyCollection<IPerson> BrotherInLaw => throw new System.NotImplementedException();

        public ReadOnlyCollection<IPerson> SisterInLaw => throw new System.NotImplementedException();

        public IPerson AddChild(string name, Gender gender)
        {
            throw new System.NotImplementedException();
        }
    }
}