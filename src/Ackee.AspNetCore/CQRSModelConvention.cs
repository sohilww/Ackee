using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;

namespace Ackee.AspNetCore
{
    public class CQRSModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerName = controller.ControllerName.Replace("Query", "", StringComparison.OrdinalIgnoreCase);
            foreach (var selector in controller.Selectors)
            {
                if (!(selector.AttributeRouteModel is null))
                    selector.AttributeRouteModel.Template = 
                        selector.AttributeRouteModel.Template.Replace("[controller]",controllerName, StringComparison.OrdinalIgnoreCase);
            }

        }
    }
}
