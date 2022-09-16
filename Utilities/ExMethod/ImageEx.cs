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
        static ColorMatrix disabledImageColorMatrix;
        private static ColorMatrix DisabledImageColorMatrix
        {
            get
            {
                if (disabledImageColorMatrix == null)
                {
                    disabledImageColorMatrix = MultiplyColorMatrix(matrix2: new float[5][]
                    {
                        new float[5] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f },
                        new float[5] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f },
                        new float[5] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f },
                        new float[5] { 0f, 0f, 0f, 1f, 0f },
                        new float[5] { 0.38f, 0.38f, 0.38f, 0f, 1f }
                    }, matrix1: new float[5][]
                    {
                        new float[5] { 1f, 0f, 0f, 0f, 0f },
                        new float[5] { 0f, 1f, 0f, 0f, 0f },
                        new float[5] { 0f, 0f, 1f, 0f, 0f },
                        new float[5] { 0f, 0f, 0f, 0.7f, 0f },
                        new float[5]
                    });
                }

                return disabledImageColorMatrix;
            }
        }
        internal static ColorMatrix MultiplyColorMatrix(float[][] matrix1, float[][] matrix2)
        {
            int num = 5;
            float[][] array = new float[num][];
            for (int i = 0; i < num; i++)
            {
                array[i] = new float[num];
            }

            float[] array2 = new float[num];
            for (int j = 0; j < num; j++)
            {
                for (int k = 0; k < num; k++)
                {
                    array2[k] = matrix1[k][j];
                }

                for (int l = 0; l < num; l++)
                {
                    float[] array3 = matrix2[l];
                    float num2 = 0f;
                    for (int m = 0; m < num; m++)
                    {
                        num2 += array3[m] * array2[m];
                    }

                    array[l][j] = num2;
                }
            }

            return new ColorMatrix(array);
        }
        public static Image CreateDisabledImage(this Image normalImage, ImageAttributes imgAttrib)
        {
            if (imgAttrib == null)
            {
                imgAttrib = new ImageAttributes();
            }

            imgAttrib.ClearColorKey();
            imgAttrib.SetColorMatrix(DisabledImageColorMatrix);
            Size size = normalImage.Size;
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
                 graphics.DrawImage(normalImage, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, imgAttrib);
            return bitmap;
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
