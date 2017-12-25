using System;
using StyleCop.CSharp;
using StyleCop;
using System.Web.Mvc;

namespace StyleCop.CustomRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public class ControllerNameAnalyzer : SourceAnalyzer
    {
        const string ruleName = "ControllerPostfixRule";

        public override void AnalyzeDocument(CodeDocument document)
        {
            CsDocument csDocument = (CsDocument)document;
            csDocument.WalkDocument(
                new CodeWalkerElementVisitor<object>(this.VisitElement));
        }

        private bool VisitElement(CsElement element, CsElement parentElement, object context)
        {
            var parentElementType = parentElement.GetType();
            bool isController = parentElementType.Equals(typeof(Controller));
            if (isController)
            {
                if (!element.Name.EndsWith("Controller", System.StringComparison.Ordinal))
                {
                    this.AddViolation(element, ruleName);
                }

                return false;
            }

            return true;
        }
    }
}
