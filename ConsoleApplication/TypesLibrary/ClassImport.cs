using CustomIoCContainer.Attributes;

namespace TypesLibrary
{
    public class ClassImport
    {
        [Import]
        public ClassExport ExportClass{ get; set; }
    }
}
