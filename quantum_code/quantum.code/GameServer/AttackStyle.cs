using Photon.Deterministic;
using System;

namespace Quantum
{
    public unsafe partial class AttackStyle
    {
        protected FP timeReload;
        public FP interval;
        public FP bulletAmount;
        public FP bulletSpeed;
        public AssetRefEntityPrototype bullet;

        public virtual void Fire(Frame f, FPVector3 position, FPVector3 forward, EntityRef indexPlayer)
        {
            var bulletEntity = f.Create(bullet);
            if (f.Unsafe.TryGetPointer<Weapon>(bulletEntity, out var bulletShell))
                bulletShell->whoShell = indexPlayer;

            if (f.Unsafe.TryGetPointer<Transform3D>(bulletEntity, out var bulletTransform))
            {
                bulletTransform->Position = position + forward*2;
                bulletTransform->Rotation = FPQuaternion.LookRotation(forward);
            }
            if (f.Unsafe.TryGetPointer<PhysicsBody3D>(bulletEntity, out var physics3D))
            {
                //physics3D->AddLinearImpulse(bulletSpeed * forward);
                physics3D->Velocity = bulletSpeed * forward;
            }
        }
    }
}
