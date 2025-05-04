using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    private const float bulletForce = 50F;
    private const float timeToShoot = 0.06F;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private Animator _animator;

    private float shootTime = timeToShoot;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (shootTime <= 0)
            {
                Shoot();
                shootTime = timeToShoot;
            }
            else
            {
                shootTime -= Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody bulletRigidibody = bullet.GetComponent<Rigidbody>();
        bulletRigidibody.AddForce(_firePoint.forward * bulletForce, ForceMode.Impulse);
        this.ShootEffect();
    }

    private void ShootEffect()
    {
        _animator.SetTrigger("Fire");
    }
}