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
    /// A single precision 2D vector.
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>
    {
        #region Static Read-Only
        /// <summary>
        /// The zero vector.
        /// </summary>
        public static readonly Vector2 zero = new Vector2(0, 0);
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
        #endregion

        #region Properties
        /// <summary>
        /// Gets the magnitude of this vector.
        /// </summary>
        public float Magnitude
        {
            get { return (float)System.Math.Sqrt(X * X + Y * Y); }
        }

        /// <summary>
        /// Gets the squared magnitude of this vector.
        /// </summary>
        public float MagnitudeSqr
        {
            get { return (X * X + Y * Y); }
        }

        /// <summary>
        /// Gets a normalized vector from this vector.
        /// </summary>
        public Vector2 Normalized
        {
            get
            {
                Vector2 result;
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new vector with one value for all components.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        /// <summary>
        /// Creates a new vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Scales the vector uniformly.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="d">The scaling value.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.X * d, a.Y * d);
        }

        /// <summary>
        /// Scales the vector uniformly.
        /// </summary>
        /// <param name="d">The scaling value.</param>
        /// <param name="a">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.X * d, a.Y * d);
        }

        /// <summary>
        /// Divides the vector with a float.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <param name="d">The dividing float value.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.X / d, a.Y / d);
        }

        /// <summary>
        /// Subtracts the vector from a zero vector.
        /// </summary>
        /// <param name="a">The vector.</param>
        /// <returns>The resulting vector.</returns>
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }

        /// <summary>
        /// Returns if two vectors equals eachother.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <returns>If equals.</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return (lhs - rhs).MagnitudeSqr < Epsilon;
        }

        /// <summary>
        /// Returns if two vectors don't equal eachother.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        /// <returns>If not equals.</returns>
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return (lhs - rhs).MagnitudeSqr >= Epsilon;
        }

        /// <summary>
        /// Explicitly converts from a double-precision vector into a single-precision vector.
        /// </summary>
        /// <param name="v">The double-precision vector.</param>
        public static explicit operator Vector2(Vector2d v)
        {
            return new Vector2((float)v.x, (float)v.y);
        }

        /// <summary>
        /// Implicitly converts from an integer vector into a single-precision vector.
        /// </summary>
        /// <param name="v">The integer vector.</param>
        public static implicit operator Vector2(Vector2i v)
        {
            return new Vector2(v.x, v.y);
        }
        #endregion

        #region Public Methods
        #region Instance
        /// <summary>
        /// Set x and y components of an existing vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public void Set(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Multiplies with another vector component-wise.
        /// </summary>
        /// <param name="scale">The vector to multiply with.</param>
        public void Scale(ref Vector2 scale)
        {
            X *= scale.X;
            Y *= scale.Y;
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
            }
            else
            {
                X = Y = 0;
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
        }
        #endregion

        #region Object
        /// <summary>
        /// Returns a hash code for this vector.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

        /// <summary>
        /// Returns if this vector is equal to another one.
        /// </summary>
        /// <param name="other">The other vector to compare to.</param>
        /// <returns>If equals.</returns>
        public override bool Equals(object other)
        {
            if (!(other is Vector2))
            {
                return false;
            }
            Vector2 vector = (Vector2)other;
            return (X == vector.X && Y == vector.Y);
        }

        /// <summary>
        /// Returns if this vector is equal to another one.
        /// </summary>
        /// <param name="other">The other vector to compare to.</param>
        /// <returns>If equals.</returns>
        public bool Equals(Vector2 other)
        {
            return (X == other.X && Y == other.Y);
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1})",
                X.ToString("F1", CultureInfo.InvariantCulture),
                Y.ToString("F1", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Returns a nicely formatted string for this vector.
        /// </summary>
        /// <param name="format">The float format.</param>
        /// <returns>The string.</returns>
        public string ToString(string format)
        {
            return string.Format("({0}, {1})",
                X.ToString(format, CultureInfo.InvariantCulture),
                Y.ToString(format, CultureInfo.InvariantCulture));
        }
        #endregion

        #region Static
        /// <summary>
        /// Dot Product of two vectors.
        /// </summary>
        /// <param name="lhs">The left hand side vector.</param>
        /// <param name="rhs">The right hand side vector.</param>
        public static float Dot(ref Vector2 lhs, ref Vector2 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }
        
        /// <summary>
        /// Performs a linear interpolation between two vectors.
        /// </summary>
        /// <param name="a">The vector to interpolate from.</param>
        /// <param name="b">The vector to interpolate to.</param>
        /// <param name="t">The time fraction.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Lerp(ref Vector2 a, ref Vector2 b, float t, out Vector2 result)
        {
            result = new Vector2(a.X + (b.X - a.X) * t, a.Y + (b.Y - a.Y) * t);
        }

        /// <summary>
        /// Multiplies two vectors component-wise.
        /// </summary>
        /// <param name="a">The first vector.</param>
        /// <param name="b">The second vector.</param>
        /// <param name="result">The resulting vector.</param>
        public static void Scale(ref Vector2 a, ref Vector2 b, out Vector2 result)
        {
            result = new Vector2(a.X * b.X, a.Y * b.Y);
        }

        /// <summary>
        /// Normalizes a vector.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <param name="result">The resulting normalized vector.</param>
        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            float mag = value.Magnitude;
            if (mag > Epsilon)
            {
                result = new Vector2(value.X / mag, value.Y / mag);
            }
            else
            {
                result = Vector2.zero;
            }
        }
        #endregion
        #endregion
    }
}