using NUnit.Framework.Constraints;
using UnityEngine;

namespace Sim
{
    public class Reactor
    {
        // Reactor state
        float rateOfFission;
        float coreTemperature;

        // User control
        float coolantFlowRate;
        float coolantTemperature;

        // Physical constants
        float coolantHeatCapacity = 4f;
        float coolantHeatTransferCoefficient = 0.1f;

        // Physical limits
        // 0 - 1: no reaction,
        // 1 - 2: ramping up,
        // 2 - 3: steady state,
        // 3 - 4: nearly critical,
        // > 4: meltdown
        float[] coreTempRange = { 0f, 500f, 700f, 900f, 1000f };

        float HeatTransferRate()
        {
            return coreTemperature * coolantFlowRate;
        }
        float HeatGenerationRate()
        {
            return rateOfFission * coreTemperature;
        }

        void Update(float deltaTime)
        {
            float heatTransferRate = HeatTransferRate();
            float heatGenerationRate = HeatGenerationRate();

            coreTemperature += (heatGenerationRate - heatTransferRate);
            coolantTemperature += heatTransferRate;
        }
    }
}