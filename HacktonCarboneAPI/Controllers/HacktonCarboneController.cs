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

        // Get api/<HacktonCarboneController>/habitation
        [HttpGet("habitation")]
        public async Task<Double> CalculateHabitation([FromBody] Habitation habitation)
        {
            double result = await Task.Run(() => HacktonCarboneService.CalculateHabitation(habitation));
            return result;
        }

        // Get api/<HacktonCarboneController>/vehicule
        [HttpGet("vehicule")]
        public async Task<double> CalculateVehicule([FromBody] Vehicule vehicule)
        {
            double result = await Task.Run(() => HacktonCarboneService.CalculateVehicule(vehicule));
            return result;
        }
    }
}