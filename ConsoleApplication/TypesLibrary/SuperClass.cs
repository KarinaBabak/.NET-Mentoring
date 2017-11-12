using CustomIoCContainer.Attributes;

namespace TypesLibrary
{
    [Export(typeof(ISuperInterface))]
    public class SuperClass : ISuperInterface
    {
    }
}
