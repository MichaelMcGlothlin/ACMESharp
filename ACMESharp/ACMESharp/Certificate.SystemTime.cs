using System;
using System.Runtime.InteropServices;

// From:
//    http://blogs.msdn.com/b/dcook/archive/2008/11/25/creating-a-self-signed-certificate-in-c.aspx

namespace ACMESharp {
 internal static partial class Certificate {
  [StructLayout ( LayoutKind.Sequential )]
  private struct SystemTime {
   public readonly Int16 Year;
   public readonly Int16 Month;
   public readonly Int16 DayOfWeek;
   public readonly Int16 Day;
   public readonly Int16 Hour;
   public readonly Int16 Minute;
   public readonly Int16 Second;
   public readonly Int16 Milliseconds;
  }
 }
}