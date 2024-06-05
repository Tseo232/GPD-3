using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyControl : MonoBehaviour
{
    public VisualEffect vfx;
    private PlayerHealth ph;
    private PlayerStateHandler pm;
    private EnergyBar eb;
    private float MaxEnergyLvl=100f;
    private float currentEnergy;
    private bool canRegenerate = false;
    private float energyRegenRate = 1f; // Energy regenerated per second
    private float energyConsumptionRate = 5f;


    // Start is called before the first frame update
    void Start()
    {
        vfx = GetComponent<VisualEffect>();
        ph = GetComponenth<PlayerHealth>();
        eb = GetComponenth<EnergyBar>();
        eb.SetMaxEnergy(MaxEnergyLvl);

        MaxEnergyLvl=CurrenrtEnergy;

    }

    void Update()
    {
      EnergyRegen();
    }

    void EnergyMeter()
    {
        if(pm.dashing||pm.sliding||pm.wallrunning||)
        {
            currentEnergy -= energyConsumptionRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
            canRegenerate = false;
        }

    }

    void EnergyRegen()
    {

        if (!canRegenerate)
        {
            regenerationTimer += Time.deltaTime;
            if (regenerationTimer >= regenerationDelay)
            {
                canRegenerate = true;
            }
        }
        else
        {
            float amountToRegenerate = energyRegenRate * Time.deltaTime;
            CurrenrtEnergy = Mathf.Min(CurrenrtEnergy + amountToRegenerate, MaxEnergyLvl);
            eb.SetEnergy(currentEnergy); 

            if (currentEnergy >= MaxEnergyLvl)
            {
                canRegenerate = false;
                regenerationTimer = 0f;
            }
        }
    }
}
