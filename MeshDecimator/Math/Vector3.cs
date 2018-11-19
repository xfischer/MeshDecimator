#region License
/*
MIT License

Copyright(c) 2017-2018 Mattias Edlund

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.Globalization;

namespace MeshDecimator.Math
{
    /// <summary>
    /// A single precision 3D vector.
    /// </summary>
    public struct Vector3 : IEquatable<Vector3>
    {
        #region Static Read-Only
        /// <summary>
        /// The zero vector.
        /// </summary>
        public static readonly Vector3 zero = new Vector3(0, 0, 0);
        #endregion

        #region Consts
        /// <summary>
        /// The vector epsilon.
        /// </summary>
        public const float Epsilon = 9.99999944E-11f;
        #endregion

        #region Fields
        /// <summary>
        /// The x component.
        /// </summary>
        public float X;
        /// <summary>
        /// The y component.
        /// </summary>
        public float Y;
        /// <summary>
        /// The z component.
        /// </summary>
        public float Z;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        /// <summary>
        /// Gets the squared magnitude of this vector.
        /// </summary>
        public float MagnitudeSqr
        {
            get { return (X * X + Y * Y + Z * Z); }
        }

        /// <summary>
        /// Gets a normalized vector from this vector.
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                Vector3 result;
                Normalize(ref this, out result);
                return result;
            }
        }

        ///// <summary>
        ///// Gets or sets a specific component by index in this vector.
        ///// </summary>
        ///// <param name="index">The component index.</param>
        //public float this[int index]
        //{
        //    get
        //    {
        //        switch (index)
        //        {
        //            case 0:
        //                return x;
        //            case 1:
        //                return y;
        //            case 2:
        //                return z;
        //            default:
        //                throw new IndexOutOfRangeException("Invalid Vector3 index!");
        //        }
        //    }
        //    set
        //    {
        //        switch (index)
        //        {
        //            case 0:
        //                x = value;
        //                break;
        //            case 1:
        //                y = value;
        //                break;
        //            case 2:
        //                z = value;
        //                break;
        //            default:
        //                throw new IndexOutOfRangeException("Invalid Vector3 index!");
        //        }
        //    }
        //}
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new vector with one value for all components.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector3(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        /// <summary>
        /// Creates a new vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <param name="z">The z value.</param>
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Creates a new vector from a double precision vector.
        /// </summary>
        /// <param name="vector">The double precision vector.</param>
        public Vector3(Vector3d vector)
        {
            this.X = (float)vector.X;
            this.Y = (float)vector.Y;
            this.Z = (float)vector.Z;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Scales the vector uniformly.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="d">The scaling value.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        /// <summary>
        /// Scales the vector uniformly.
        /// </summary>
        /// <param name="d">The scaling value.</param>
        /// <param name="a">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.X * d, a.Y * d, a.Z * d);
        }

        /// <summary>
        /// Divides the vector with a float.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="d">The dividing float value.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.X / d, a.Y / d, a.Z / d);
        }

        /// <summary>
        /// Subtracts the vector from a zero vector.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        /// <summary>
        /// Returns if two vectors equals eachother.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <returns>If equals.</returns>
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return (lhs - rhs).MagnitudeSqr < Epsilon;
        }

        /// <summary>
        /// Returns if two vectors don't equal eachother.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <returns>If not equals.</returns>
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return (lhs - rhs).MagnitudeSqr >= Epsilon;
        }

        /// <summary>
        /// Explicitly converts from a double-precision vector into a single-precision vector.
        /// </summary>
        /// <param name="v">The double-precision vector.</param>
        public static explicit operator Vector3(Vector3d v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        }

        /// <summary>
        /// Implicitly converts from an integer vector into a single-precision vector.
        /// </summary>
        /// <param name="v">The integer vector.</param>
        public static implicit operator Vector3(Vector3i v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        #endregion

        #region Public Methods
        #region Instance
        /// <summary>
        /// Set x, y and z components of an existing vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <param name="z">The z value.</param>
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Multiplies with another vector component-wise.
        /// </summary>
        /// <param name="scale">The vector to multiply with.</param>
        public void Scale(ref Vector3 scale)
        {
            X *= scale.X;
            Y *= scale.Y;
            Z *= scale.Z;
        }

        /// <summary>
        /// Normalizes this vector.
        /// </summary>
        public void Normalize()
        {
            float mag = this.Magnitude;
            if (mag > Epsilon)
            {
                X /= mag;
                Y /= mag;
                Z /= mag;
            }
            else
            {
                X = Y = Z = 0;
            }
        }

        /// <summary>
        /// Clamps this vector between a specific range.
        /// </summary>
        /// <param name="min">The minimum component value.</param>
        /// <param name="max">The maximum component value.</param>
        public void Clamp(float min, float max)
        {
            if (X < min) X = min;
            else if (X > max) X = max;

            if (Y < min) Y = min;
            else if (Y > max) Y = max;

            if (Z < min) Z = min;
            else if (Z > max) Z = max;
        }
        #endregion

        #region Object
        /// <summary>
        /// Returns a hash code for this vector.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;
        }

        /// <summary>
        /// Returns if this vector is equal to another one.
        /// </summary>
        /// <param name="other">The other vector to compare to.</param>
        /// <returns>If equals.</returns>
        public override bool Equals(object other)
        {
            if (!(other is Vector3))
            {
                return false;
            }
            Vector3 vector = (Vector3)other;
            return (X == vector.X && Y == vector.Y && Z == vector.Z);
        }

        /// <summary>
        /// Returns if this vector is equal to another one.
        /// </summary>
        /// <param name="other">The other vector to compare to.</param>
        /// <returns>If equals.</returns>
        public bool Equals(Vector3 other)
        {
            return (X == other.X && Y == other.Y && Z == other.Z);
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})",
                X.ToString("F1", CultureInfo.InvariantCulture),
                Y.ToString("F1", CultureInfo.InvariantCulture),
                Z.ToString("F1", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        /// <param name="format">The float format.</param>
        /// <returns>The string.</returns>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2})",
                X.ToString(format, CultureInfo.InvariantCulture),
                Y.ToString(format, CultureInfo.InvariantCulture),
                Z.ToString(format, CultureInfo.InvariantCulture));
        }
        #endregion

        #region Static
        /// <summary>
        /// Dot Product of two vectors.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        public static float Dot(ref Vector3 lhs, ref Vector3 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        /// <summary>
        /// Cross Product of two vectors.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Cross(ref Vector3 lhs, ref Vector3 rhs, out Vector3 result)
        {
            result = new Vector3(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
        }

        /// <summary>
        /// Calculates the angle between two vectors.
        /// </summary>
        /// <param name="from">The from vector.</param>
        /// <param name="to">The to vector.</param>
        /// <returns>The angle.</returns>
        public static float Angle(ref Vector3 from, ref Vector3 to)
        {
            Vector3 fromNormalized = from.Normalized;
            Vector3 toNormalized = to.Normalized;
            return (float)System.Math.Acos(MathHelper.Clamp(Vector3.Dot(ref fromNormalized, ref toNormalized), -1f, 1f)) * MathHelper.Rad2Deg;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        /// <param name="a">The vector to interpolate from.</param>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="t">The time fraction.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Lerp(ref Vector3 a, ref Vector3 b, float t, out Vector3 result)
        {
            result = new Vector3(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t);
        }

        /// <summary>
        /// Multiplies two vectors component-wise.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Scale(ref Vector3 a, ref Vector3 b, out Vector3 result)
        {
            result = new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        /// <summary>
        /// Normalizes a vector.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <param name="result">The resulting normalized vector.</param>
        public static void Normalize(ref Vector3 value, out Vector3 result)
        {
            float mag = value.Magnitude;
            if (mag > Epsilon)
            {
                result = new Vector3(value.X / mag, value.Y / mag, value.Z / mag);
            }
            else
            {
                result = Vector3.zero;
            }
        }

        /// <summary>
        /// Normalizes both vectors and makes them orthogonal to each other.
        /// </summary>
        /// <param name="normal">The normal vector.</param>
        /// <param name="tangent">The tangent.</param>
        public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
        {
            normal.Normalize();
            Vector3 proj = normal * Vector3.Dot(ref tangent, ref normal);
            tangent -= proj;
            tangent.Normalize();
        }
        #endregion
        #endregion
    }
}