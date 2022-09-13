using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;

namespace Utilities.IteLicense
{
    public class IteLicense : License
    {
        private String mLicenseKey = null;
        private IteLicenseProvider mProvider = null;
        public IteLicense(IteLicenseProvider provider, String key)
        {
            this.mProvider = provider;
            this.mLicenseKey = key;
        }
        public override string LicenseKey
        {
            get { return this.mLicenseKey; }
        }
        public override void Dispose()
        {
            this.mProvider = null;
            this.mLicenseKey = null;
        }
    }
   public  class IteLicenseProvider : LicenseProvider
   {
       private String GetAssemblyPath(LicenseContext context)
       {
           String fileName = null;
           Type type = this.GetType();
           ITypeResolutionService service = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
           if (service != null)
           {
               fileName = service.GetPathOfAssembly(type.Assembly.GetName());
           }
           if (fileName == null)
           {
               fileName = type.Module.FullyQualifiedName;
           }
           return Path.GetDirectoryName(fileName);
       }
       public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
       {
           IteLicense license = null;

           if (context != null)
           {
               string rNum = IteRegist.getRNum();
               string encrypt = SignVerifyEnvelope.GetHash(rNum);
               if (context.UsageMode == LicenseUsageMode.Runtime)
               {
                   String savedLicenseKey = context.GetSavedLicenseKey(type, null);
                 
                   if (encrypt  .Equals(savedLicenseKey))
                   {
                       return new IteLicense(this, encrypt);
                   }
               }
               if (license != null)
               {
                   return license;
               } 
               if(SignVerifyEnvelope.VerifyXmlFile(encrypt))
                   license = new IteLicense(this, encrypt);

               if (license != null)
               {
                   context.SetSavedLicenseKey(type, encrypt);
               }
           }
           if (license == null)
           {
               // System.Windows.Forms.MessageBox.Show("!!!尚未注册!!!");
               string s = ITS9000Registry.GetValue("SetupDate");
               try
               {
                   s = Utilities.Security.DESEncrypt.Decode(s);
                   if (DateTime.Today - DateTime.Parse(s) <=  TimeSpan.FromDays(30))
                        return new IteLicense(this, "Full");
               }
               catch { }
               return new IteLicense(this, "evaluate");
           }
           return license;
       }
    }
}
