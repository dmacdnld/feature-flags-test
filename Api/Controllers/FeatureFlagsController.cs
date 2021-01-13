using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureFlagsController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        public FeatureFlagsController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> IsEnabledAsync(string featureFlagName)
        {
            try
            {
                // Note: `IsEnabledAsync` will return `false` when no feature flag of the provided name is found
                var enabled = await _featureManager.IsEnabledAsync(featureFlagName);

                return Ok(enabled);
            }
            catch (SystemException)
            {
                return Ok(false);
            }
        }
    }
}
