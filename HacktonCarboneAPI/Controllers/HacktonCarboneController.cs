using Microsoft.AspNetCore.Mvc;

namespace HacktonCarboneAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HacktonCarboneController : ControllerBase
    {
        private IHacktonCarboneService HacktonCarboneService { get; set; }

        public HacktonCarboneController(IHacktonCarboneService hacktonCarboneService)
        {
            HacktonCarboneService = hacktonCarboneService;
        }

        // Post api/<HacktonCarboneController>/habitation
        [HttpPost("habitation")]
        public async Task<Double> CalculateHabitation([FromBody] Habitation habitation)
        {
            double result = await Task.Run(() => HacktonCarboneService.CalculateHabitation(habitation));
            return result;
        }

        // Post api/<HacktonCarboneController>/vehicule
        [HttpPost("vehicule")]
        public async Task<double> CalculateVehicule([FromBody] Vehicule vehicule)
        {
            double result = await Task.Run(() => HacktonCarboneService.CalculateVehicule(vehicule));
            return result;
        }
    }
}