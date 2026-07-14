using System;
using System.Text;

namespace testapp.mylib.canopen.eds
{
    public static class DataTypeMap
    {
        public static Type ToNetType(byte canopenType)
        {
            switch (canopenType)
            {
                case 0x01: return typeof(bool);
                case 0x02: return typeof(sbyte);
                case 0x03: return typeof(short);
                case 0x04: return typeof(int);
                case 0x05: return typeof(byte);
                case 0x06: return typeof(ushort);
                case 0x07: return typeof(uint);
                case 0x08: return typeof(float);
                case 0x09: return typeof(string);
                case 0x0B: return typeof(int);
                case 0x0C: return typeof(double);
                default:   return typeof(byte[]);
            }
        }

        public static int GetByteCount(byte canopenType)
        {
            switch (canopenType)
            {
                case 0x01: return 1;
                case 0x02: return 1;
                case 0x03: return 2;
                case 0x04: return 4;
                case 0x05: return 1;
                case 0x06: return 2;
                case 0x07: return 4;
                case 0x08: return 4;
                case 0x0B: return 3;
                case 0x0C: return 8;
                default:   return -1;
            }
        }

        public static object FromBytes(byte canopenType, byte[] data)
        {
            if (data == null) return null;
            switch (canopenType)
            {
                case 0x01: return data.Length >= 1 && data[0] != 0;
                case 0x02: return data.Length >= 1 ? (sbyte)data[0] : (sbyte)0;
                case 0x03: return data.Length >= 2 ? BitConverter.ToInt16(data, 0) : (short)0;
                case 0x04: return data.Length >= 4 ? BitConverter.ToInt32(data, 0) : 0;
                case 0x05: return data.Length >= 1 ? data[0] : (byte)0;
                case 0x06: return data.Length >= 2 ? BitConverter.ToUInt16(data, 0) : (ushort)0;
                case 0x07: return data.Length >= 4 ? BitConverter.ToUInt32(data, 0) : 0U;
                case 0x08: return data.Length >= 4 ? BitConverter.ToSingle(data, 0) : 0f;
                case 0x09: return Encoding.ASCII.GetString(data ?? new byte[0]);
                case 0x0C: return data.Length >= 8 ? BitConverter.ToDouble(data, 0) : 0.0;
                default:   return data;
            }
        }

        public static byte[] ToBytes(byte canopenType, object value)
        {
            if (value == null) return new byte[0];
            switch (canopenType)
            {
                case 0x01: return new byte[] { (bool)value ? (byte)1 : (byte)0 };
                case 0x02: return new byte[] { (byte)(sbyte)value };
                case 0x03: return BitConverter.GetBytes((short)value);
                case 0x04: return BitConverter.GetBytes((int)value);
                case 0x05: return new byte[] { (byte)value };
                case 0x06: return BitConverter.GetBytes((ushort)value);
                case 0x07: return BitConverter.GetBytes((uint)value);
                case 0x08: return BitConverter.GetBytes((float)value);
                case 0x09: return Encoding.ASCII.GetBytes(value?.ToString() ?? "");
                case 0x0C: return BitConverter.GetBytes((double)value);
                default:   return value as byte[] ?? new byte[0];
            }
        }
    }
}
