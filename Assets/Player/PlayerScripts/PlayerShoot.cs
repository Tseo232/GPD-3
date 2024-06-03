using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerShoot : MonoBehaviour
{
    // Bullet
    public GameObject bullet;
    public int damage;

    // Bullet force
    public float shootForce;
    public float upwardForce;

    // Gun stats
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;
    public int magSize;
    public int bulletsPerTap;
    protected bool fullAuto;

    protected int ammoLeft;
    protected int bulletsShot;

    // Recoil
    public Rigidbody playerRb;
    public float recoilForce;

    // Bools
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;

    // References
    public CinemachineVirtualCamera cinemachineCam;
    private Camera mainCamera;
    public Transform attackPoint;
    protected PlayerStateHandler pm;
    protected PlayerCam pc;
    public LayerMask aimColliderLayerMask;

    // Graphics
    public TextMeshProUGUI ammunitionDisplay;

    public KeyCode aimKey = KeyCode.Mouse1;
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    // Bug fixing
    private bool allowInvoke = true;

    private void Awake()
    {
        // Make sure magazine is full
        ammoLeft = magSize;
        readyToShoot = true;
        pm = GetComponent<PlayerStateHandler>();
        mainCamera = Camera.main; // Get the main camera
    }

    protected void Update()
    {
        MyInput();
        UpdateAmmoDisplay();
    }

    private void MyInput()
    {
        shooting = fullAuto ? Input.GetKey(shootKey) : Input.GetKeyDown(shootKey);

        if (Input.GetKeyDown(reloadKey) && ammoLeft < magSize && !reloading)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && ammoLeft <= 0)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && ammoLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
        else
        {
            pm.Shoot = false;
        }

        if (Input.GetKey(aimKey))
        {
            pm.aiming = true;
           // cinemachineCam.m_Lens.FieldOfView = 50f;
        }
        else
        {
            pm.aiming = false;
           // cinemachineCam.m_Lens.FieldOfView = 80f;
        }
    }

    private void UpdateAmmoDisplay()
    {
        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText($"Ammo: {ammoLeft / bulletsPerTap} / {magSize / bulletsPerTap}");
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        Vector3 aimDir = (mouseWorldPosition - attackPoint.position).normalized;

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
        currentBullet.GetComponent<Rigidbody>().AddForce(aimDir * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(mainCamera.transform.up * upwardForce, ForceMode.Impulse);

        ammoLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting);
            allowInvoke = false;
            playerRb.AddForce(-aimDir * recoilForce, ForceMode.Impulse);
        }

        if (bulletsShot < bulletsPerTap && ammoLeft > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 9999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            
        }
        else
        {
            Vector3 cameraDirection = mainCamera.transform.forward;
            Vector3 cameraPosition = mainCamera.transform.position;
            float distanceFromCamera = 10f;

            mouseWorldPosition = cameraPosition + cameraDirection * distanceFromCamera;
        }

        return mouseWorldPosition;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        ammoLeft = magSize;
        reloading = false;
    }
}
