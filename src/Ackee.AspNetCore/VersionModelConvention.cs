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
            var controllerName = controller.ControllerName;
            foreach (var selector in controller.Selectors)
            {
                if (!(selector.AttributeRouteModel is null))
                    selector.AttributeRouteModel.Template = $"{_version}/api/{controllerName}";
            }

        }
    }
}
