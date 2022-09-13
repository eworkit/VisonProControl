using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
    public class RadializedCharacters
    {
        void a()
        {
            using (Font fnt = new Font("Arial", 60, FontStyle.Bold))//定义字体
            {
                var Image = (Bitmap)ImageLightEffect("通过", fnt, Color.Black, Color.White, 10);//调用自定义方法ImageLightEffect
            }
        }
        public static Image ImageLightEffect(string Str, Font F, Color ColorFore, Color ColorBack, int BlurConsideration)
        {
            Bitmap Var_Bitmap = null;//实例化Bitmap类
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))//实例化Graphics类
            {
                SizeF Var_Size = g.MeasureString(Str, F);//对字符串进行测量
                using (Bitmap Var_bmp = new Bitmap((int)Var_Size.Width, (int)Var_Size.Height))//通过文字的大小实例化Bitmap类
                using (Graphics Var_G_Bmp = Graphics.FromImage(Var_bmp))//实例化Bitmap类
                using (SolidBrush Var_BrushBack = new SolidBrush(Color.FromArgb(16, ColorBack.R, ColorBack.G, ColorBack.B)))//根据RGB的值定义画刷
                using (SolidBrush Var_BrushFore = new SolidBrush(ColorFore))//定义画刷
                {
                    Var_G_Bmp.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                    Var_G_Bmp.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                    Var_G_Bmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                    Var_G_Bmp.DrawString(Str, F, Var_BrushBack, 0, 0);//给制文字
                    Var_Bitmap = new Bitmap(Var_bmp.Width + BlurConsideration, Var_bmp.Height + BlurConsideration);//根据辉光文字的大小实例化Bitmap类
                    using (Graphics Var_G_Bitmap = Graphics.FromImage(Var_Bitmap))//实例化Graphics类
                    {
                        Var_G_Bitmap.SmoothingMode = SmoothingMode.HighQuality;//设置为高质量
                        Var_G_Bitmap.InterpolationMode = InterpolationMode.HighQualityBilinear;//设置为高质量的收缩
                        Var_G_Bitmap.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;//消除锯齿
                        //遍历辉光文字的各象素点
                        for (int x = 5; x <= BlurConsideration; x++)
                        {
                            for (int y = 3; y <= BlurConsideration; y++)
                            {
                                Var_G_Bitmap.DrawImageUnscaled(Var_bmp, x, y);//绘制辉光文字的点
                            }
                        }
                        Var_G_Bitmap.DrawString(Str, F, Var_BrushFore, BlurConsideration / 2, BlurConsideration / 2);//绘制文字
                    }
                }
            }

            return Var_Bitmap;

        }
    }
}
