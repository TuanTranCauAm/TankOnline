using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class Shell : SystemMainThreadFilter<Shell.Bullet>, ISignalOnCollisionEnter3D
    {
        public void OnCollisionEnter3D(Frame f, CollisionInfo3D info)
        {

            var entity = info.Entity;       // Tank
            var other = info.Other;         // Item
            if (f.Unsafe.TryGetPointer<Tank>(entity, out var x) && f.Unsafe.TryGetPointer<ItemPower>(other, out var y))
            {
                if(f.Unsafe.TryGetPointer<Turret>(x->visualTurret, out var x1)) {
                    x1->currentWeapon = y->weapon;
                    var weapon = f.FindAsset<AttackStyle>(y->weapon.Id);
                    x1->interval = weapon.interval;
                    x1->bulletAmount = weapon.bulletAmount;
                    if (f.Unsafe.TryGetPointer<Transform3D>(other, out var Tran))
                        Tran->Position = new FPVector3(f.RNG->Next(-15,15), 120, f.RNG->Next(-10, 10));
                    
                    else
                        f.Destroy(other); 
                }
                
            }
            if (f.Unsafe.TryGetPointer<Weapon> (entity, out var t1)) {
                if (f.Unsafe.TryGetPointer<Tank>(other, out var t3))
                {
                    if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var exploredPosition))
                        f.Events.ShellExplored(exploredPosition->Position);

                    if (t3->currentHealth <= 0)
                    {
                        f.Events.Death(other);
                        t3->curTimeRespawn = t3->timeRespawn;
                        f.Events.UpdatePoint(t1->whoShell);       // Plus to this player
                    }
                    else { 
                        t3->currentHealth -= t1->damage;
                        f.Events.UpdateHealth(other, t3->currentHealth, t3->maxHealth);
                    }
                }
                f.Destroy(info.Entity);
            }
        }

        public override void Update(Frame f, ref Bullet bullet)
        {
            if (bullet.Link->TimeToDesTroy > 0)
            {
                bullet.Link->TimeToDesTroy -= f.DeltaTime;
                if (bullet.Link->TimeToDesTroy <= 0)
                {
                    f.Destroy(bullet.Entity);
                }
            }
        }

        public unsafe struct Bullet
        {
            public EntityRef Entity;            //Đối tượng
            public PhysicsBody3D* Move;         //Điều khiển
            public Transform3D* Transform;      //Trạng thái vật lý
            public Weapon* Link;                //Đối tượng truyền vào
        }
    }
}
