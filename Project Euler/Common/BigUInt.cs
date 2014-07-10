using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectEuler.Common
{
    public sealed class BigUInt : IComparable<BigUInt>
    {
        // Array containing the digits of the BigUint. The digit with index 0 is the
        // Least Significant digit. The digit with the highest index is the Most Significat digit.
        private byte[] _digits;
        bool _isPostive = true;

        #region constructors

        // private default constructor. So it cannot be used.
        private BigUInt() {
        }

        public BigUInt(string s) {
            if (string.IsNullOrEmpty(s)) {
                throw new ArgumentException("Input string cannot be null or empty.");
            }
            SetDigits(s);

            // Check for the value -0 (minus zero).
            // If the absolute value of this number is 0 (zero) the sign should be positive.
            if (_digits.Length == 1 && _digits[0] == 0) {
                _isPostive = true;
            }
        }

        public BigUInt(int i) {
            SetDigits(i.ToString());
        }

        public BigUInt(uint i) {
            SetDigits(i.ToString());
        }

        public BigUInt(long l) {
            SetDigits(l.ToString());
        }

        public BigUInt(ulong l) {
            SetDigits(l.ToString());
        }

        public BigUInt(BigUInt bi) {
            // Because we are copying another BigUInt object we can trust the values contained 
            // in this number. Simply copy the digits and the sign without further checks.
            _digits = new byte[bi.Length];
            Array.Copy(bi.Digits, _digits, bi.Length);
            _isPostive = bi.IsPostive;
        }

        public BigUInt(bool isPositive, byte[] digitArray) {
            SetDigits(digitArray);
            _isPostive = isPositive;

            // Check for the value -0 (minus zero).
            // If the absolute value of this number is 0 (zero) the sign should be positive.
            if (_digits.Length == 1 && _digits[0] == 0) {
                _isPostive = true;
            }
        }

        #endregion

        private void SetDigits(string digitString) {
            // Remove leading zeros. In a string the most significant digit is the one with the lowest index. So we must
            // start checking for leading zero's at the start of the string.
            int startingIndex = 0;
            while (startingIndex < digitString.Length - 1) {
                if (digitString[startingIndex] != '0') {
                    break; // break while loop if we found a digit that is non-zero.
                }
                startingIndex++;
            }

            _digits = new byte[digitString.Length - startingIndex];

            for (int i = startingIndex; i < digitString.Length; i++) {
                byte b;
                try {
                    b = byte.Parse(digitString[i].ToString());
                }
                catch (FormatException e) {
                    throw new ArgumentException("The string with digits must contain values 0 through 9 only.", e);
                }
                _digits[digitString.Length - 1 - i] = b;
            }
        }

        private void SetDigits(byte[] digitArray) {
            if (digitArray == null)
                throw new ArgumentNullException("digitArray");

            if (digitArray.Length == 0)
                throw new ArgumentOutOfRangeException("The array with digits must contain at least 1 value.");
            
            // Check contents of digitArray
            foreach (byte b in digitArray) {
                if (b > 9) {
                    throw new ArgumentOutOfRangeException("The array with digits must contain values 0 through 9 only.");
                }
            }
            
            // Remove leading zeros. The most significant digit is the one with the highest index. So we must
            // start checking for leading zero's at the end of the digitArray.
            int endingIndex = digitArray.Length - 1;
            while (endingIndex >= 1) {
                if(digitArray[endingIndex] != 0) {
                    break; // break while loop if we found a digit that is non-zero.
                }
                endingIndex--;
            }
            
            _digits = new byte[endingIndex + 1];
            Array.Copy(digitArray, _digits, _digits.Length);
        }

        public int Length {
            get { return _digits.Length; }
        }
        
        // Indexer
        public byte this[int index] {
            get { return _digits[index]; }
        }

        public bool IsPostive {
            get { return _isPostive; }
        }

        public bool IsNegative {
            get { return !_isPostive; }
        }

        private byte[] Digits {
            get { return _digits; }
        }

        public BigUInt Add(BigUInt valueToAdd) {
            int biggestLength = Math.Max(_digits.Length, valueToAdd.Length);
            byte[] answer = new byte[biggestLength + 1];

            byte operand1 = 0;
            byte operand2 = 0;
            byte result = 0;
            byte carry = 0;
            for (int i = 0; i < biggestLength; i++) {
                operand1 = (i < _digits.Length) ? _digits[i] : (byte)0;
                operand2 = (i < valueToAdd.Length) ? valueToAdd[i] : (byte)0;
                result = (byte)(operand1 + operand2 + carry);

                if (result > 9) {
                    result -= 10;
                    carry = 1;
                }
                else {
                    carry = 0;
                }

                answer[i] = result;
            }
            if (carry != 0) {
                answer[biggestLength] = carry;
            }

            return new BigUInt(true, answer);
        }

        /// <summary>
        /// Simple multiplication algoritm.
        /// Multiply by repeated addition.
        /// </summary>
        /// <param name="valueToMultiply"></param>
        /// <returns></returns>
        public BigUInt Multiply(BigUInt valueToMultiply) {
            throw new NotImplementedException(); 
        }
        

        #region IComparable implemetation

        public int CompareTo(BigUInt other) {
            // If this BigUInt has more digits than the other, it is obviously greater than the other.
            if (Length < other.Length)
                return -1;

            // If this BigUInt has less digits than the other, it is obviously less than the other.
            if (Length > other.Length)
                return 1;
            
            // Both BigUInt objects have the same number of digits.
            // Compare the individual digits starting with the most significant one.
            // If we find a digit that is greater than the corresponding digit of the other then 
            // this BigUint is greater than the other.
            // If we find a digit that is less than the corresponding digit of the other then 
            // this BigUint is less than the other.
            // If all digits are equal then both numbers are equal
            for (int i = Length - 1; i >= 0; i--) {
                byte thisDigit = this[i];
                byte otherDigit = other[i];

                if (thisDigit < otherDigit)
                    return -1;

                if (thisDigit > otherDigit)
                    return 1;
            }

            return 0;
        }
        
        #endregion

        #region Boilerplate Equals, ToString, GetHashCode Implementation

        public override string ToString() {
            StringBuilder sb = new StringBuilder(_digits.Length);

            if (IsNegative)
                sb.Append("-");
            for (int i = _digits.Length - 1; i >= 0; i--) {
                sb.Append(_digits[i]);
            }

            return sb.ToString();
        }

        public override bool Equals(object obj) {

            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;

            BigUInt other = obj as BigUInt;

            // The two numbers are not equal if they don't have the same sign
            if (IsPostive != other.IsPostive)
                return false;

            // The two numbers are not equal if they have a different number of digits
            if (Length != other.Length)
                return false;

            // The two numbers are not equal if they don't have the same digits.
            for (int i = Length - 1; i >= 0; i--) {
                if (this[i] != other[i])
                    return false;
            }
            
            // If we come this far, the two numbers are equal.
            return true;
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        #endregion

        public static bool operator ==(BigUInt b1, BigUInt b2) {

            if ((object)b1 == null)
                if ((object)b2 == null)
                    return true;
                else
                    return false;

            return (b1.Equals(b2));
        }

        public static bool operator !=(BigUInt b1, BigUInt b2) {

            return !(b1 == b2);
        }


    }
}
