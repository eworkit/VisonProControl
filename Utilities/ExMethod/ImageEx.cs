using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Utilities.ExMethod
{
    public static class ImageEx
    {
        public static Icon GetIcon(this Bitmap img)
        {
            return Icon.FromHandle(img.GetHicon());
        }
        public static  Bitmap ToGray(this Bitmap bmp, bool mode = true)
        {
            if (bmp == null)
            {
                return null;
            }


            int w = bmp.Width;
            int h = bmp.Height;
            try
            {
                byte newColor = 0;
                BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)srcData.Scan0.ToPointer();
                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {


                            if (mode) //加权平均
                            {
                                newColor = (byte)((float)p[0] * 0.114f + (float)p[1] * 0.587f + (float)p[2] * 0.299f);
                            }
                            else　　　　// 算数平均
                            {
                                newColor = (byte)((float)(p[0] + p[1] + p[2]) / 3.0f);
                            }
                            p[0] = newColor;
                            p[1] = newColor;
                            p[2] = newColor;


                            p += 3;
                        }
                        p += srcData.Stride - w * 3;
                    }
                    bmp.UnlockBits(srcData);
                    return bmp;
                }
            }
            catch
            {
                return null;
            }


        }


    }
}
