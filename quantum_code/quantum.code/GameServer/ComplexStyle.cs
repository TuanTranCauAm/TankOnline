using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public unsafe partial class ComplexStyle: AttackStyle
    {
        public override void Fire(Frame f, FPVector3 position, FPVector3 forward, EntityRef indexPlayer)
        {
            for (int i = 0; i < 5; i++)
            {
                var bulletEntity = f.Create(bullet);
                if (f.Unsafe.TryGetPointer<Weapon>(bulletEntity, out var bulletShell))
                    bulletShell->whoShell = indexPlayer;

                if (f.Unsafe.TryGetPointer<Transform3D>(bulletEntity, out var bulletTransform))
                {
                    bulletTransform->Position = position + forward * 2;
                    bulletTransform->Rotation = FPQuaternion.LookRotation(forward);
                }

                if (f.Unsafe.TryGetPointer<PhysicsBody3D>(bulletEntity, out var physics3D))
                {
                    physics3D->AddLinearImpulse(bulletSpeed * forward);
                    physics3D->Velocity = (20) * (forward + new FPVector3(forward.Z, 0, -forward.X) * (i - 2) / 10);
                }
                
            }
        }
    }
}
