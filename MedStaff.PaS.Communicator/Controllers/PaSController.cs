using Microsoft.AspNetCore.Mvc;
using MedStaff.DaS.Domain.Interfaces;

namespace MedStaff.DaS.Communicator.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController(), Route(template: "[controller]", Name = nameof(PaSController), Order = 0)]
    public sealed partial class PaSController : ControllerBase, IPaSEndPoints
    {
        private readonly 
    }
}