using UnityEngine;

namespace Assets.Code.WeaponModule.Interfaces
{
    public interface IWeapon
    {
        float ShootingForce { get; set; }

        WeaponType WeaponType { get; set; }

        IWeaponProjectile WeaponProjectile { get; set; }

        /// <summary>
        /// Set handle point for weapon
        /// </summary>
        /// <param name="mainHandleTransform">Main handle point for weapon.</param>
        /// <param name="secondaryHandleTransform">Secondary handle point for weapon (null if onehanded weapon type).</param>
        void SetHandlePoint(Transform mainHandleTransform, Transform secondaryHandleTransform = null);
    }
}