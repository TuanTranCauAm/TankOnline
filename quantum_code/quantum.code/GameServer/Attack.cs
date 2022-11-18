using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class Attack : SystemMainThreadFilter<Attack.Player>
    {

        public override void Update(Frame f, ref Player player)
        {
            if (f.Unsafe.TryGetPointer<Turret>(player.Link->visualTurret, out var turret))
            {
                if (turret->currentWeapon == null) {
                    turret->currentWeapon = turret->defaultAttack;
                }
           
                var input = f.GetPlayerInput(player.Link->Player);
                if (turret->interval > 0)
                {
                    turret->interval -= 1; //Time.deltaTime;
                    return;
                }
            
                if (!input->Fire)
                    return;

                FireABullet(f, ref player);
            }
            
        }
        
        public void ChangeAttackStyle(Frame f, ref AssetRefAttackStyle attackStyle, ref Player player)
        {
            if (f.Unsafe.TryGetPointer<Turret>(player.Link->visualTurret, out var turret))
            {
                turret->currentWeapon = attackStyle;
                var weapon = f.FindAsset<AttackStyle>(attackStyle.Id);
                turret->interval = weapon.interval;
                turret->bulletAmount = weapon.bulletAmount;
            }      
        }
        
        private void FireABullet(Frame f, ref Player player)
        {
            if (f.Unsafe.TryGetPointer<Turret>(player.Link->visualTurret, out var turret))
            {
                var weapon = f.FindAsset<AttackStyle>(turret->currentWeapon.Id);
                var indexPlayer = player.Entity;
                if (f.Unsafe.TryGetPointer<Transform3D>(player.Link->visualTurret, out var fire) && indexPlayer != null)
                {
                    weapon.Fire(f, fire->Position, fire->Forward, indexPlayer);
                    turret->bulletAmount -= 1;
                    turret->interval = weapon.interval;
                    if (turret->bulletAmount <= 0)
                        ChangeAttackStyle(f, ref turret->defaultAttack, ref player);
                }
            }
            
        }

        public struct Player
        {
            public EntityRef Entity;          //Đối tượng
            public PhysicsBody3D* Move;       //Điều khiển
            public Transform3D* Transform;    //Trạng thái vật lý
            public Tank* Link;                //Đối tượng truyền vào
        }
    }
}
