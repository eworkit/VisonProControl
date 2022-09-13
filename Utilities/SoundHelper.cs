using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
　using System.Runtime.InteropServices;
using System.Threading; 
　
namespace Utilities
{
    public  class SoundHelper
    {
        public static uint SND_ASYNC = 0x0001;
        public static uint SND_FILENAME = 0x00020000;
        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand,
        string lpstrReturnString, uint uReturnLength, uint hWndCallback);
        static  void PlayWait(string file)
        {
            mciSendString(@"close temp_alias", null, 0, 0);
            mciSendString(string.Format(@"open {0} alias temp_alias",file), null, 0, 0);
           // mciSendString("play temp_alias repeat", null, 0, 0);
            mciSendString("play temp_alias wait", null, 0, 0);
            mciSendString(@"close temp_alias", null, 0, 0);
        }
        static void PlayRepeat(string file)
        {
            mciSendString(@"close temp_alias", null, 0, 0);
            mciSendString(string.Format(@"open {0}  type mpegvideo alias temp_alias", file), null, 0, 0);
            mciSendString("play temp_alias repeat", null, 0, 0);
        }  
        /// <summary>
        /// 播放音频文件
        /// </summary>
        /// <param name="file">音频文件路径</param>
        /// <param name="times">播放次数，0：循环播放 大于0：按指定次数播放</param>
        public static void Play(string file, int times=1)
        {
           var thread =  new System.Threading.Thread(() =>
               {
                   if (times == 0)
                   {
                       PlayRepeat(file);
                   }
                   else if (times > 0)
                   {
                       for (int i = 0; i < times; i++)
                       {
                           PlayWait(file);
                       }
                   }
               }); 
            //线程必须为单线程
             thread.SetApartmentState(ApartmentState.STA);
             thread.IsBackground = true;
             thread.Start();
        }

    }
}
