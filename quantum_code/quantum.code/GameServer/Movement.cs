using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class Movement : SystemMainThreadFilter<Movement.Player>, ISignalOnPlayerDataSet
    {

        public override void Update(Frame f, ref Player player)
        {
            f.Events.UpdateColorHealth(player.Entity);
            var input = f.GetPlayerInput(player.Link->Player);
            var moveDir = input->Direction.XOY;
            if (moveDir == default)
                player.Move->Velocity = FPVector3.Lerp(player.Move->Velocity, default, player.Link->braking * f.DeltaTime);
            else
            {
             
                player.Move->Velocity += moveDir * f.DeltaTime * player.Link->accelation;
                player.Move->Velocity = FPVector3.ClampMagnitude(player.Move->Velocity, player.Link->maxSpeed);
                player.Transform->Rotation = FPQuaternion.Slerp(player.Transform->Rotation, FPQuaternion.LookRotation(moveDir), player.Link->rotationSpeed * f.DeltaTime);
            }

            if (player.Link->curTimeRespawn > 0)
            {
                player.Link->curTimeRespawn -= f.DeltaTime;
                player.Transform->Position = new FPVector3(0, -100, 0);
            }

            if (player.Link->curTimeRespawn < 0) {
                player.Link->curTimeRespawn = 0;
                player.Link->currentHealth = player.Link->maxHealth;
                f.Events.Alive(player.Entity);
                f.Events.UpdateHealth(player.Entity, player.Link->currentHealth, player.Link->maxHealth);
                player.Transform->Position = new FPVector3(f.RNG->Next(-15, 15), 1/2, f.RNG->Next(-10, 10));
            }

            if(f.Unsafe.TryGetPointer<Transform3D>(player.Link->visualTurret, out var turret))
            {
                turret->Position = player.Transform->Position + new FPVector3(0, 1/5, 0);
            }
        }

        public void OnPlayerDataSet(Frame f, PlayerRef player)
        {
            //Log.Info(f.PlayerToActorId(player));
            
            var data = f.GetPlayerData(player);
            var prototype = f.FindAsset<EntityPrototype>(data.Prefab.Id);
            var e = f.Create(prototype);
            if (f.Unsafe.TryGetPointer<Tank>(e, out var Link))
            {   
                Link->Player = player;
                if (f.PlayerToActorId(Link->Player) == null)
                    Log.Info("chimmmmmm");

                Link->currentHealth = Link->maxHealth;
                f.Events.Alive(e);
                f.Events.UpdateHealth(e, Link->currentHealth, Link->maxHealth);
                f.Events.UpdateColorHealth(e);

                if (f.Unsafe.TryGetPointer<TurretPrefab>(e, out var TUR))
                {
                    var tur = f.FindAsset<EntityPrototype>(TUR->turret.Id);
                    Link->visualTurret = f.Create(tur);
                }
                    
            }
                
            if (f.Unsafe.TryGetPointer<Transform3D>(e, out var Transform))
            {
                Transform->Position = new FPVector3(f.RNG->Next(-15,15), 2, f.RNG->Next(-10, 10));
            }
            
        }

        public unsafe struct Player
        {
            public EntityRef Entity;            //Đối tượng
            public PhysicsBody3D* Move;         //Điều khiển
            public Transform3D* Transform;      //Trạng thái vật lý
            public Tank* Link;                  //Đối tượng truyền vào
        }
    }
}
