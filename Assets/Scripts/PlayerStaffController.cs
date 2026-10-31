
using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [Header("Primary Attack")]
    [SerializeField, Min(0.1f)] private float fireRate = 3f;

    [Header("Secondary Attack")]
    [SerializeField, Min(0.1f)] private float secondaryCooldown = 1.5f;
    [SerializeField, Range(0f, 45f)] private float spreadAngle = 15f;

    [Header("Projectile References")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform tip;

    [Header("Shooting Audio")]
    [SerializeField] private AudioClip shootingAudio;

    private float nextFireTime;
    private float nextSecondaryFireTime;
    private Vector2 lookDirection;

    private void Update()
    {
        // Prevent aiming and shooting while paused
        if (Time.timeScale == 0f)
            return;

        SetLookDirection();
        RotateStaff();

        // Primary attack: hold left mouse button
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        // Secondary attack: right mouse button
        if (Input.GetMouseButtonDown(1) &&
            Time.time >= nextSecondaryFireTime)
        {
            ShootSpread();
            nextSecondaryFireTime = Time.time + secondaryCooldown;
        }
    }

    private void SetLookDirection()
    {
        // Convert cursor position into world coordinates
        Vector3 mousePosition =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lookDirection =
            ((Vector2)mousePosition - (Vector2)transform.position).normalized;
    }

    private void RotateStaff()
    {
        float angle =
            Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        SpawnProjectile(lookDirection);
        PlayShootingSound();
    }

    private void ShootSpread()
    {
        // Shoot three projectiles in a fan pattern
        SpawnProjectile(RotateDirection(lookDirection, -spreadAngle));
        SpawnProjectile(lookDirection);
        SpawnProjectile(RotateDirection(lookDirection, spreadAngle));

        PlayShootingSound();
    }

    private Vector2 RotateDirection(Vector2 direction, float angle)
    {
        // Rotate the firing direction around the Z axis
        return Quaternion.Euler(0f, 0f, angle) * direction;
    }

    private void SpawnProjectile(Vector2 direction)
    {
        if (projectile == null || tip == null)
        {
            Debug.LogWarning("Projectile or Tip reference is missing!");
            return;
        }

        Projectile newProjectile =
            Instantiate(projectile, tip.position, Quaternion.identity);

        newProjectile.InitializeProjectile(direction);
    }

    private void PlayShootingSound()
    {
        if (shootingAudio != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayAudio(
                shootingAudio,
                SoundType.SFX,
                0.15f,
                false
            );
        }
    }
}
