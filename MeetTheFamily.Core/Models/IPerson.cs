using System.Collections.ObjectModel;
using MeetTheFamily.Core.Constants;

namespace MeetTheFamily.Core.Models
{
    public interface IPerson
    {
        string Name { get; }
        Gender Gender { get; }
        IPerson Father { get; }
        IPerson Mother { get; }
        IPerson Spouse { get; set; }
        ReadOnlyCollection<IPerson> Childrens { get; }
        IPerson AddChild(string name, Gender gender);
        ReadOnlyCollection<IPerson> Sons { get; }
        ReadOnlyCollection<IPerson> Daughters { get; }
        ReadOnlyCollection<IPerson> Brothers { get; }
        ReadOnlyCollection<IPerson> Sisters{get;}

        ReadOnlyCollection<IPerson> GrandDaughters{get;}

        ReadOnlyCollection<IPerson> Cousins{get;}
        ReadOnlyCollection<IPerson> BrotherInLaw{get;}
        ReadOnlyCollection<IPerson> SisterInLaw{get;}
    }
}