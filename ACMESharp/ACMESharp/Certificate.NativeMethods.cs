using System;
using System.Runtime.InteropServices;

// From:
//    http://blogs.msdn.com/b/dcook/archive/2008/11/25/creating-a-self-signed-certificate-in-c.aspx

namespace ACMESharp {
 internal static partial class Certificate {
  private static class NativeMethods {
   [DllImport ( "kernel32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean FileTimeToSystemTime (
       [In] ref Int64 fileTime,
       out SystemTime systemTime );

   [DllImport ( "AdvApi32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CryptAcquireContextW (
       out IntPtr providerContext,
       [MarshalAs ( UnmanagedType.LPWStr )] String container,
       [MarshalAs ( UnmanagedType.LPWStr )] String provider,
       Int32 providerType,
       Int32 flags );

   [DllImport ( "AdvApi32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CryptReleaseContext (
       IntPtr providerContext,
       Int32 flags );

   [DllImport ( "AdvApi32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CryptGenKey (
       IntPtr providerContext,
       Int32 algorithmId,
       Int32 flags,
       out IntPtr cryptKeyHandle );

   [DllImport ( "AdvApi32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CryptDestroyKey (
       IntPtr cryptKeyHandle );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CertStrToNameW (
       Int32 certificateEncodingType,
       IntPtr x500,
       Int32 strType,
       IntPtr reserved,
       [MarshalAs ( UnmanagedType.LPArray )] [Out] Byte[] encoded,
       ref Int32 encodedLength,
       out IntPtr errorString );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   public static extern IntPtr CertCreateSelfSignCertificate (
       IntPtr providerHandle,
       [In] ref CryptoApiBlob subjectIssuerBlob,
       Int32 flags,
       [In] ref CryptKeyProviderInformation keyProviderInformation,
       IntPtr signatureAlgorithm,
       [In] ref SystemTime startTime,
       [In] ref SystemTime endTime,
       IntPtr extensions );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CertFreeCertificateContext (
       IntPtr certificateContext );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   public static extern IntPtr CertOpenStore (
       [MarshalAs ( UnmanagedType.LPStr )] String storeProvider,
       Int32 messageAndCertificateEncodingType,
       IntPtr cryptProvHandle,
       Int32 flags,
       IntPtr parameters );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CertCloseStore (
       IntPtr certificateStoreHandle,
       Int32 flags );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CertAddCertificateContextToStore (
       IntPtr certificateStoreHandle,
       IntPtr certificateContext,
       Int32 addDisposition,
       out IntPtr storeContextPtr );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean CertSetCertificateContextProperty (
       IntPtr certificateContext,
       Int32 propertyId,
       Int32 flags,
       [In] ref CryptKeyProviderInformation data );

   [DllImport ( "Crypt32.dll", SetLastError = true, ExactSpelling = true )]
   [return: MarshalAs ( UnmanagedType.Bool )]
   public static extern Boolean PFXExportCertStoreEx (
       IntPtr certificateStoreHandle,
       ref CryptoApiBlob pfxBlob,
       IntPtr password,
       IntPtr reserved,
       Int32 flags );
  }
 }
}