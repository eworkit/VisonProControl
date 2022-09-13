using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;

namespace VisionControl
{
    public class TriggerFifo
    {
        public CogToolDisplay pDisplay;//显示图片以及图像
        public int m_iVideoFormats = 0;//视频格式
        public CogAcqFifoTool AcqFifoTool;//FifoTool
        public CogImage8Grey OneShotImage, image;

        public CogAcqInfo info = new CogAcqInfo();
        //Image is not flipped or rotated，default value:None（Image is not flipped or rotated.）
        CogIPOneImageFlipRotateOperationConstants m_enRotateType = CogIPOneImageFlipRotateOperationConstants.None;


        public TriggerFifo(CogAcqFifoTool CFT, CogToolDisplay CTD)
        {

            pDisplay = CTD;
            AcqFifoTool = CFT;
            FindCCDAndInitial();
        }

        private bool FindCCDAndInitial()
        {


            AcqFifoTool.Operator.TimeoutEnabled = false;
            //拍照完成触发事件
            AcqFifoTool.Operator.Complete += new CogCompleteEventHandler(Acquisition_Complete);
            return true;
        }

        /*  void CogCompleteEventHandler(Object sender,CogCompleteEventArgs e){};
        * Represents the method that will handle the Complete event of an ICogAcqFifo. 
        * The method must have the same parameters as this delegate.
        * 
        */
        public void Acquisition_Complete(object sender, CogCompleteEventArgs e)
        {

            //vpp运行过程
            /*numPending:
             Type: System..::..Int32
             * The number of acquisitions in the pending state. This is the number of acquisitions requested by StartAcquire()()()() for which acquisition has not started. * 
             *To achieve frame rate acquisition in manual trigger mode, the FIFO must always have one or more pending acquisitions.
             *
             * numReady
                Type: System..::..Int32

                The number of acquisition requests ready to be completed.
               
                busy 
                Type: System..::..Boolean
             * True if the oldest outstanding acquisition is waiting for a trigger signal or is acquiring an image. For master/slave acquisitions, it becomes true only after the master and all slaves are ready for acquisition.
             */
            int Acq_numPending = 0;
            int Acq_numReady = 0;
            bool Acq_busy = false;

            ICogAcqFifo AcqCapture = (ICogAcqFifo)sender;
            //得到CCD狀態,Returns the instantaneous state of the fifo.
            AcqCapture.GetFifoState(out Acq_numPending, out Acq_numReady, out Acq_busy);

            if (Acq_numReady > 0)
            {

                /*
               * Completes the acquisition specified by the requested ticke and returns the acquired image. 
               * This method is the same as CompleteAcquire(Int32, Int32, Int32) but passes and returns its results in an ICogAcqInfo.
               * 
               * */
                image = (CogImage8Grey)AcqCapture.CompleteAcquireEx(info);//捕获到图像

                //----------------------
                #region
                //CogIPOneImageTool:Tool that operates on a single input image to produce a single output image
                CogIPOneImageTool pOneImage = new CogIPOneImageTool();
                //CogIPOneImageFlipRotate:Class that flips and/or applies rotation to an image.
                CogIPOneImageFlipRotate ipFlipRotat = new CogIPOneImageFlipRotate();
                //The operation used to re - orient the image.
                ipFlipRotat.OperationInPixelSpace = m_enRotateType;

                //拍照获取图像使用的是取像工具的属性Operator，它是ICogAcqFifo类型。
                /*
                 * Operater属性：
                 * Returns the collection of ICogIPOneImageOperators. 
                 * When the tool is run, each operator in the collection will be executed sequentially with the output of the first passed on to the second, and so on.
                 */
                pOneImage.Operators.Add(ipFlipRotat);
                pOneImage.InputImage = image;
                pOneImage.Run();
                var bitmap = pOneImage.OutputImage.ToBitmap();
                CogImage8Grey pimage = new CogImage8Grey(bitmap);

                #endregion

                OneShotImage = pimage;
                ///-----------
                if (pDisplay != null)
                {

                    pDisplay.Display.Image = OneShotImage;
                }
                ///-----------
            }

        }
    }
}
