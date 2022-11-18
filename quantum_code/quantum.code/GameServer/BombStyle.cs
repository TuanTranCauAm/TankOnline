using Photon.Deterministic;
using System;

namespace Quantum
{
    [Serializable]
    public unsafe partial class BombStyle : AttackStyle
    {
        public override void Fire(Frame f, FPVector3 position, FPVector3 forward, EntityRef indexPlayer)
        {
            var bulletEntity = f.Create(bullet);
            if (f.Unsafe.TryGetPointer<Weapon>(bulletEntity, out var bulletShell))
                bulletShell->whoShell = indexPlayer;

            if (!f.Unsafe.TryGetPointer<Transform3D>(bulletEntity, out var bulletTransform))
                return;

            bulletTransform->Position = position + forward * 2 + FPVector3.Up;
            bulletTransform->Rotation = FPQuaternion.LookRotation(forward);

            if (!f.TryGet<Tank>(indexPlayer, out var tank))
                return;

            var input = f.GetPlayerInput(tank.Player);

            var origin = bulletTransform->Position;
            var target = new FPVector3(input->mousePos.X, bulletTransform->Position.Y, input->mousePos.Z);

            if (f.Unsafe.TryGetPointer<PhysicsBody3D>(bulletEntity, out var physics3D))
            {
                var v = CalculateVelocity(target, origin, 2);
                physics3D->AddForce(v * physics3D->Mass / f.DeltaTime);
            }
        }

        private FPVector3 CalculateVelocity(FPVector3 target, FPVector3 origin, FP time)
        {
            FPVector3 distance = target - origin;
            FPVector3 distanceXZ = distance;
            distanceXZ.Y = 0;

            FP Sy = distance.Y;
            FP Sxz = distanceXZ.Magnitude;

            FP Vxz = Sxz / time;
            FP Vy = Sy / time + FP._0_50 * FPMath.Abs(10) * time;
            FPVector3 result = distanceXZ.Normalized;
            result *= Vxz;
            result.Y = Vy;
            return result;
        }
    }
}
