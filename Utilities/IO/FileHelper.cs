using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.IO
{
    public class FileHelper
    {
        public static   bool IsValidFileName(string fileName)
        {
            var lst = System.IO.Path.GetInvalidFileNameChars().ToList();
            lst.AddRange(new[] { '\'' });
            if (fileName.IndexOfAny(lst.ToArray()) >= 0)
                return false;
            return true;
            bool isValid = true;
            string errChar = "\\/:*?\"<>|";  //
            if (string.IsNullOrEmpty(fileName))
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < errChar.Length; i++)
                {
                    if (fileName.Contains(errChar[i].ToString()))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }
    }
}
