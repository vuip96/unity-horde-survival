
using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField, Min(0.1f)] private float fireRate = 3f;

    private float nextFireTime;

    private void Update()
    {
        // Prevent aiming and shooting while the game is paused
        if (Time.timeScale == 0f)
        {
            return;
        }

        // Rotate the staff towards the mouse cursor
        RotateStaff();

        // Shoot while holding the left mouse button
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();

            // Calculate when the next shot is allowed
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void RotateStaff()
    {
        // Convert mouse position to world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
        );

        // Calculate the direction towards the mouse
        Vector2 lookDirection =
            ((Vector2)mousePosition - (Vector2)transform.position).normalized;

        // Calculate the rotation angle in degrees
        float angle =
            Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Rotate the staff around the Z axis
        transform.rotation = Quaternion.AngleAxis(
            angle,
            Vector3.forward
        );
    }

    private void Shoot()
    {
        // Temporary shooting test
        Debug.Log("BANG!");
    }
}
