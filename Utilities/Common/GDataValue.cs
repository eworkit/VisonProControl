using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class GDataValue
    {
        object s;
        public bool HasValue { get { return s != null; } }
        public GDataValue(object s)
        {
            this.s = s;
        }
        static public implicit operator GDataValue(string roman)
        {
            return new GDataValue(roman);
        }
        static public implicit operator GDataValue(double roman)
        {
            return new GDataValue(roman);
        }
        static public implicit operator GDataValue(int roman)
        {
            return new GDataValue(roman);
        }
        static public implicit operator GDataValue(DateTime roman)
        {
            return new GDataValue(roman);
        }
        static public implicit operator GDataValue(bool roman)
        {
            return new GDataValue(roman);
        }
        static public implicit operator string(GDataValue gs)
        {
            return gs.s.ToString();
        }
        static public implicit operator int(GDataValue r)
        {
            return (int)Convert.ToDouble(r.s);
        }
        static public implicit operator uint(GDataValue r)
        {
            return (uint)Convert.ToDouble(r.s);
        }

        public override string ToString()
        {
            return s.ToString();
        }
        static public implicit operator double(GDataValue r)
        {
            return Convert.ToDouble(r.s);
        }
        static public implicit operator bool(GDataValue r)
        {
            return Convert.ToBoolean(r.s);
        }
        static public implicit operator DateTime(GDataValue r)
        {
            return Convert.ToDateTime(r.s);
        }
        static public bool operator >(GDataValue d1, GDataValue d2)
        {
            try
            {
                return (double)d1 > (double)d2;
            }
            catch { }
            try { return (DateTime)d1 > (DateTime)d2; }
            catch { }
            return string.Compare(d1, d2) > 0;
        }
        static public bool operator <(GDataValue d1, GDataValue d2)
        {
            try
            {
                return (double)d1 < (double)d2;
            }
            catch { }
            try { return (DateTime)d1 < (DateTime)d2; }
            catch { }
            return string.Compare(d1, d2) < 0;
        }
        static public bool operator ==(GDataValue d1, GDataValue d2)
        {
            try
            {
                return (double)d1 == (double)d2;
            }
            catch { }
            try { return (DateTime)d1 == (DateTime)d2; }
            catch { }
            return string.Compare(d1, d2) == 0;
        }
        static public bool operator !=(GDataValue d1, GDataValue d2)
        {
            try
            {
                return (double)d1 != (double)d2;
            }
            catch { }
            try { return (DateTime)d1 != (DateTime)d2; }
            catch { }
            return string.Compare(d1, d2) != 0;
        }
        static public GDataValue operator +(GDataValue d1, GDataValue d2)
        {
            try
            {
                return (double)d1 + (double)d2;
            }
            catch { }

            return d1.ToString() + d2.ToString();
        }
        static public GDataValue operator *(GDataValue d1, GDataValue d2)
        {
            try
            {
                return (double)d1 * (double)d2;
            }
            catch { }
            try
            {
                int n = d2;
                string sRtn = "";
                for (int i = 0; i < n; i++)
                {
                    sRtn += d1;
                }
                return sRtn;
            }
            catch { } try
            {
                int n = d1;
                string sRtn = "";
                for (int i = 0; i < n; i++)
                {
                    sRtn += d2;
                }
                return sRtn;
            }
            catch { }
            return d1.ToString() + d2.ToString();
        }
    }
}
