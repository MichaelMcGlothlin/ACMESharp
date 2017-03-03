using System;
using System.Runtime.InteropServices;

// From:
//    http://blogs.msdn.com/b/dcook/archive/2008/11/25/creating-a-self-signed-certificate-in-c.aspx

namespace ACMESharp {
 internal static partial class Certificate {
  [StructLayout ( LayoutKind.Sequential )]
  private struct CryptKeyProviderInformation {
#pragma warning disable RCS1169 // Mark field as read-only.
   [MarshalAs ( UnmanagedType.LPWStr )]
   public String ContainerName;

   [MarshalAs ( UnmanagedType.LPWStr )]
   public String ProviderName;

   public Int32 ProviderType;

   public Int32 Flags;

   public Int32 ProviderParameterCount;

   public IntPtr ProviderParameters; // PCRYPT_KEY_PROV_PARAM

   public Int32 KeySpec;
#pragma warning restore RCS1169 // Mark field as read-only.
  }
 }
}