using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GunMode : PlayerShoot
{
    public TextMeshProUGUI displayGun;

    // Define gun modes
    public enum GunModes { Assault, BattleRifle, Burst }

    // Current gun mode
    public GunModes currentGunMode = GunModes.Assault;

    // Switch gun mode key
    public KeyCode switchModeKey = KeyCode.Mouse2;

    public new void Update()
    {
        // Call base class Update method
        base.Update();

        // Check if switch mode key is pressed
        if (Input.GetKeyDown(switchModeKey))
        {
            // Switch to the next gun mode
            SwitchGunMode();
        }

        // Update gun mode display
        if (displayGun != null)
        {
            displayGun.SetText(currentGunMode.ToString());
        }
    }

    // Method to switch between gun modes
    private void SwitchGunMode()
    {
        // Get the index of the current gun mode
        int currentModeIndex = (int)currentGunMode;

        // Calculate the index of the next gun mode
        int nextModeIndex = (currentModeIndex + 1) % GunModes.GetNames(typeof(GunModes)).Length;

        // Set the next gun mode
        currentGunMode = (GunModes)nextModeIndex;

        // Apply settings based on the selected gun mode
        switch (currentGunMode)
        {
            case GunModes.Assault:
                fullAuto = true;
                bulletsPerTap = 1;
                damage = 15;
                magSize = 45;
                reloadTime = 1.3f;
                pm.CantedAim = true;
                break;
            case GunModes.Burst:
                fullAuto = false;
                bulletsPerTap = 3;
                damage = 10;
                magSize = 30;
                reloadTime = 2;
                pm.CantedAim = false;
                break;
            case GunModes.BattleRifle:
                fullAuto = false;
                bulletsPerTap = 1;
                damage = 35;
                magSize = 15;
                reloadTime = 2;
                pm.CantedAim = false;
                break;
        }

        // Update ammo left to reflect new mag size
        ammoLeft = magSize;
    }
}
