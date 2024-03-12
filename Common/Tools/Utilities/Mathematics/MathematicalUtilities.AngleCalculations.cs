﻿using Microsoft.Xna.Framework;
using Terraria;

namespace KarmaLibrary.Common.Utilities
{
    public static partial class Utilities
    {
        /// <summary>
        /// Wraps an angle similar to <see cref="MathHelper.WrapAngle(float)"/>, except with a range of 0 to 2pi instead of -pi to pi.
        /// </summary>
        /// <param name="theta">The angle to wrap.</param>
        public static float WrapAngle360(float theta)
        {
            theta = WrapAngle(theta);
            if (theta < 0f)
                theta += TwoPi;

            return theta;
        }

        /// <summary>
        /// Determines the angular distance between two vectors based on dot product comparisons. This method ensures underlying normalization is performed safely.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        public static float AngleBetween(this Vector2 v1, Vector2 v2) => Acos(Vector2.Dot(v1.SafeNormalize(Vector2.Zero), v2.SafeNormalize(Vector2.Zero)));

        /// <summary>
        /// Determines the inverse of a given quaternion.
        /// </summary>
        /// <param name="rotation">The quaternion to calculate the inverse of.</param>
        public static Quaternion Inverse(this Quaternion rotation)
        {
            float x = rotation.X;
            float y = rotation.Y;
            float z = rotation.Z;
            float w = rotation.W;
            float inversionFactor = 1f / (x.Squared() + y.Squared() + z.Squared() + w.Squared());
            return new Quaternion(x, -y, -z, -w) * inversionFactor;
        }

        /// <summary>
        /// Rotates a given vector by a given quaternion rotation.
        /// </summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="rotation">The quaternion to rotate by.</param>
        public static Vector3 RotatedBy(this Vector3 vector, Quaternion rotation)
        {
            return Vector3.Transform(Vector3.Transform(vector, rotation), rotation.Inverse());
        }
    }
}
