using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private AttackStyle defaultWeapon;
    private AttackStyle currentWeapon;
    private float bulletAmount = 0;
    private float interval = 0;

    void Start()
    {
        defaultWeapon.Getup();
        currentWeapon = defaultWeapon;
    }

    void Update()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
            return;
        }
        if (!Input.GetMouseButton(0))
            return;
        FireABullet();
    }

    public void ChangeAttaclStyle(AttackStyle attackStyle)
    {
        currentWeapon = attackStyle;
        interval = attackStyle.interval;
        bulletAmount = attackStyle.bulletAmount;
    }

    private void FireABullet()
    {
        currentWeapon.Fire(transform.position, transform.forward, transform.localRotation);
        bulletAmount--;
        interval = currentWeapon.interval;
        if (bulletAmount <= 0)
            ChangeAttaclStyle(defaultWeapon);
    }
}

