using CustomIoCContainer.Attributes;

namespace TypesLibrary
{
    public class ImportClass
    {
        [Import]
        public SuperClass SuperObject { get; set; }
    }
}
