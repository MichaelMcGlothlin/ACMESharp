using System;
using System.Runtime.InteropServices;

// From:
//    http://blogs.msdn.com/b/dcook/archive/2008/11/25/creating-a-self-signed-certificate-in-c.aspx

namespace ACMESharp {
 internal static partial class Certificate {
  [StructLayout ( LayoutKind.Sequential )]
  private struct CryptoApiBlob {
#pragma warning disable RCS1169 // Mark field as read-only.
   public Int32 DataLength;

   public IntPtr Data;
#pragma warning restore RCS1169 // Mark field as read-only.

   public CryptoApiBlob ( Int32 dataLength, IntPtr data ) {
    DataLength = dataLength;
    Data = data;
   }
  }
 }
}