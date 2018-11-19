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
    /// A single precision 4D vector.
    /// </summary>
    public struct Vector4 : IEquatable<Vector4>
    {
        #region Static Read-Only
        /// <summary>
        /// The zero vector.
        /// </summary>
        public static readonly Vector4 zero = new Vector4(0, 0, 0, 0);
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
        /// <summary>
        /// The w component.
        /// </summary>
        public float W;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W); }
        }

        /// <summary>
        /// Gets the squared magnitude of this vector.
        /// </summary>
        public float MagnitudeSqr
        {
            get { return (X * X + Y * Y + Z * Z + W * W); }
        }

        /// <summary>
        /// Gets a normalized vector from this vector.
        /// </summary>
        public Vector4 Normalized
        {
            get
            {
                Vector4 result;
                Normalize(ref this, out result);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets a specific component by index in this vector.
        /// </summary>
        /// <param name="index">The component index.</param>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    case 3:
                        return W;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    case 3:
                        W = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector4 index!");
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new vector with one value for all components.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector4(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        /// <summary>
        /// Creates a new vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <param name="z">The z value.</param>
        /// <param name="w">The w value.</param>
        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        /// <summary>
        /// Scales the vector uniformly.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="d">The scaling value.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 operator *(Vector4 a, float d)
        {
            return new Vector4(a.X * d, a.Y * d, a.Z * d, a.W * d);
        }

        /// <summary>
        /// Scales the vector uniformly.
        /// </summary>
        /// <param name="d">The scaling value.</param>
        /// <param name="a">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 operator *(float d, Vector4 a)
        {
            return new Vector4(a.X * d, a.Y * d, a.Z * d, a.W * d);
        }

        /// <summary>
        /// Divides the vector with a float.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="d">The dividing float value.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 operator /(Vector4 a, float d)
        {
            return new Vector4(a.X / d, a.Y / d, a.Z / d, a.W / d);
        }

        /// <summary>
        /// Subtracts the vector from a zero vector.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector4 operator -(Vector4 a)
        {
            return new Vector4(-a.X, -a.Y, -a.Z, -a.W);
        }

        /// <summary>
        /// Returns if two vectors equals eachother.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <returns>If equals.</returns>
        public static bool operator ==(Vector4 lhs, Vector4 rhs)
        {
            return (lhs - rhs).MagnitudeSqr < Epsilon;
        }

        /// <summary>
        /// Returns if two vectors don't equal eachother.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <returns>If not equals.</returns>
        public static bool operator !=(Vector4 lhs, Vector4 rhs)
        {
            return (lhs - rhs).MagnitudeSqr >= Epsilon;
        }

        /// <summary>
        /// Explicitly converts from a double-precision vector into a single-precision vector.
        /// </summary>
        /// <param name="v">The double-precision vector.</param>
        public static explicit operator Vector4(Vector4d v)
        {
            return new Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
        }

        /// <summary>
        /// Implicitly converts from an integer vector into a single-precision vector.
        /// </summary>
        /// <param name="v">The integer vector.</param>
        public static implicit operator Vector4(Vector4i v)
        {
            return new Vector4(v.x, v.y, v.z, v.w);
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
        /// <param name="w">The w value.</param>
        public void Set(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Multiplies with another vector component-wise.
        /// </summary>
        /// <param name="scale">The vector to multiply with.</param>
        public void Scale(ref Vector4 scale)
        {
            X *= scale.X;
            Y *= scale.Y;
            Z *= scale.Z;
            W *= scale.W;
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
                W /= mag;
            }
            else
            {
                X = Y = Z = W = 0;
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

            if (W < min) W = min;
            else if (W > max) W = max;
        }
        #endregion

        #region Object
        /// <summary>
        /// Returns a hash code for this vector.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2 ^ W.GetHashCode() >> 1;
        }

        /// <summary>
        /// Returns if this vector is equal to another one.
        /// </summary>
        /// <param name="other">The other vector to compare to.</param>
        /// <returns>If equals.</returns>
        public override bool Equals(object other)
        {
            if (!(other is Vector4))
            {
                return false;
            }
            Vector4 vector = (Vector4)other;
            return (X == vector.X && Y == vector.Y && Z == vector.Z && W == vector.W);
        }

        /// <summary>
        /// Returns if this vector is equal to another one.
        /// </summary>
        /// <param name="other">The other vector to compare to.</param>
        /// <returns>If equals.</returns>
        public bool Equals(Vector4 other)
        {
            return (X == other.X && Y == other.Y && Z == other.Z && W == other.W);
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})",
                X.ToString("F1", CultureInfo.InvariantCulture),
                Y.ToString("F1", CultureInfo.InvariantCulture),
                Z.ToString("F1", CultureInfo.InvariantCulture),
                W.ToString("F1", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        /// <param name="format">The float format.</param>
        /// <returns>The string.</returns>
        public string ToString(string format)
        {
            return string.Format("({0}, {1}, {2}, {3})",
                X.ToString(format, CultureInfo.InvariantCulture),
                Y.ToString(format, CultureInfo.InvariantCulture),
                Z.ToString(format, CultureInfo.InvariantCulture),
                W.ToString(format, CultureInfo.InvariantCulture));
        }
        #endregion

        #region Static
        /// <summary>
        /// Dot Product of two vectors.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        public static float Dot(ref Vector4 lhs, ref Vector4 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z + lhs.W * rhs.W;
        }

        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        /// <param name="a">The vector to interpolate from.</param>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="t">The time fraction.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Lerp(ref Vector4 a, ref Vector4 b, float t, out Vector4 result)
        {
            result = new Vector4(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t, a.Z + (b.Z - a.Z) * t, a.W + (b.W - a.W) * t);
        }

        /// <summary>
        /// Multiplies two vectors component-wise.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Scale(ref Vector4 a, ref Vector4 b, out Vector4 result)
        {
            result = new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        /// <summary>
        /// Normalizes a vector.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <param name="result">The resulting normalized vector.</param>
        public static void Normalize(ref Vector4 value, out Vector4 result)
        {
            float mag = value.Magnitude;
            if (mag > Epsilon)
            {
                result = new Vector4(value.X / mag, value.Y / mag, value.Z / mag, value.W / mag);
            }
            else
            {
                result = Vector4.zero;
            }
        }
        #endregion
        #endregion
    }
}