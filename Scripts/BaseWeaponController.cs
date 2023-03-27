
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class BaseWeaponController : UdonSharpBehaviour
{
    [Header("Components")]
    [SerializeField]
    VRC_Pickup m_vRCPickup;
    [SerializeField]
    Animator m_animator;
    [SerializeField]
    Transform m_muzzle;
    [SerializeField]
    Transform m_grip;

    [Header("Debugging")]
    [SerializeField]
    bool m_debugShowBarrelRaycast = false;

    [Header("State")]
    [SerializeField]
    bool m_safety = false;
    [SerializeField]
    bool m_triggerPulled;
    [SerializeField, Range(0, 255)]
    byte m_ammoCount = 0;
    [SerializeField]
    bool m_magazineLoaded = false;
    [SerializeField, Range(0, 2)]
    byte m_currentFireMode = 0;

    [Header("Animator")]
    [SerializeField]
    bool m_animTogglingSafety = false;
    bool m_animFiring = false;


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
        m_vRCPickup.orientation = VRC_Pickup.PickupOrientation.Gun;
        m_vRCPickup.AutoHold = VRC_Pickup.AutoHoldMode.Yes;
        m_vRCPickup.ExactGun = m_grip.transform;
        m_grip.transform.rotation *= Quaternion.Euler(-90, 0, -90);
    }

    void Update()
    {
        HandleInput();
    }

    public override void InputUse(bool value, UdonInputEventArgs args)
    {
        if (value)
        {
            Debug.Log("Bang!");
            Fire();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            ToggleSafety();
        }
    }

    public void Fire()
    {
        if (AnimatorHas("Fire"))
        {
            if (m_animFiring)
            {
                return;
            }
            m_animator.SetBool("Fire", true);
        }
    }

    public void ToggleSafety()
    {
        if (AnimatorHas("Safety"))
        {
            if (m_animTogglingSafety)
            {
                return;
            }
            m_animator.SetBool("Safety", !m_animator.GetBool("Safety"));
        }
        m_safety = !m_safety;
    }

    public void AnimFireStart()
    {
        m_animFiring = true;
    }

    public void AnimFireFinish()
    {
        m_animFiring = false;
        m_animator.SetBool("Fire", false);
    }

    public void AnimTogglingSafetyStart()
    {
        m_animTogglingSafety = true;
    }

    public void AnimTogglingSafetyFinish()
    {
        m_animTogglingSafety = false;
    }

    bool AnimatorHas(string name)
    {
        foreach (var param in m_animator.parameters)
        {
            if (param.name == name)
            {
                return true;
            }
        }
        return false;
    }

}
