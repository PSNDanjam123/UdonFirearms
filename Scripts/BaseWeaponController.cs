
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class BaseWeaponController : UdonSharpBehaviour
{
    [Header("Components")]
    [SerializeField]
    VRC_Pickup m_vRCPickup;
    [SerializeField]
    Transform m_muzzle;
    [SerializeField]
    Transform m_grip;

    [Header("Debugging")]
    [SerializeField]
    bool m_debugShowBarrelRaycast = false;

    [Header("State")]
    [SerializeField]
    bool m_triggerPulled;
    [SerializeField, Range(0, 255)]
    byte m_ammoCount = 0;
    [SerializeField]
    bool m_magazineLoaded = false;
    [SerializeField, Range(0, 2)]
    byte m_currentFireMode = 0;


    [Header("Ammunition")]
    [SerializeField, Range(0, 3), Tooltip("Bullet = 0, Projectile = 1, TODO TODO")]
    byte m_ammoType = 0;
    [SerializeField, Range(1, 255)]

    [Header("Reloading")]
    byte m_magazineSize = 6;
    [SerializeField, Tooltip("Reload Speed Per Second")]
    float m_reloadSpeed = 0;

    [Header("Fire Mode")]
    [SerializeField, Range(0, 2)]
    byte m_defaultFireMode = 0;
    [SerializeField, Tooltip("Single = 0")]
    bool m_canSingleFireMode = true;
    [SerializeField, Tooltip("Burst = 1")]
    bool m_canBurstFireMode = false;
    [SerializeField, Tooltip("Auto = 2")]
    bool m_canAutoFireMode = false;

    [Header("Fire Rate")]
    [SerializeField, Tooltip("Single Fire Rate Per Second")]
    float m_singleFireRate = 0.0f;
    [SerializeField, Tooltip("Burst Fire Rate Per Second")]
    float m_burstFireRate = 0.0f;
    [SerializeField, Tooltip("Auto Fire Rate Per Second")]
    float m_autoFireRate = 0.0f;

    void Start()
    {
        if (m_vRCPickup == null)
        {
            m_vRCPickup = GetComponent<VRC_Pickup>();
        }
        m_vRCPickup.orientation = VRC_Pickup.PickupOrientation.Gun;
        m_vRCPickup.AutoHold = VRC_Pickup.AutoHoldMode.Yes;
        m_vRCPickup.ExactGun = m_grip.transform;
        m_grip.transform.rotation *= Quaternion.Euler(-90, 0, -90);
    }

}
