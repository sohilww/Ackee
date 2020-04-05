using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Ackee.AspNetCore
{
    public class VersionModelConvention : IControllerModelConvention
    {
        private readonly string _version;
        public VersionModelConvention(string version = "V1")
        {
            _version = version;
        }
        public void Apply(ControllerModel controller)
        {
            foreach (var selector in controller.Selectors)
            {
                if (selector.AttributeRouteModel is null)
                    CreateDefaultRoute(selector);
                if (DoesControllerHaveAttribute(selector))
                    selector.AttributeRouteModel.Template = _version + "/" + selector.AttributeRouteModel.Template;
            }

        }

        private static bool DoesControllerHaveAttribute(SelectorModel selector)
        {
            return !(string.IsNullOrWhiteSpace(selector.AttributeRouteModel.Template));
        }
        private static void CreateDefaultRoute(SelectorModel selector)
        {
            selector.AttributeRouteModel = new AttributeRouteModel() {Template = "api/[controller]"};
        }

        
    }
}
