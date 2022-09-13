using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Utilities
{
    public class SerializeHelper
    {  //序列化
        public static byte[] rawSerialize(object obj)
        {
            int rawsize = Marshal.SizeOf(obj);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(obj, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }

        //反序列化
        public static T rawDeserialize<T>(byte[] rawdatas) where T : struct
        {
            //Type anytype = typeof(TestStruct);
            //int rawsize = Marshal.SizeOf(anytype);
            //if (rawsize > rawdatas.Length) return new TestStruct();
            //IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            //Marshal.Copy(rawdatas, 0, buffer, rawsize);
            //object retobj = Marshal.PtrToStructure(buffer, anytype);
            //Marshal.FreeHGlobal(buffer);
            //return (TestStruct)retobj; 
            GCHandle gch = GCHandle.Alloc(rawdatas, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                gch.Free();
            }
        }
    }
}
