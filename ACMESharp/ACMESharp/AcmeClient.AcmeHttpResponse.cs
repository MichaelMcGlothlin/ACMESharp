using ACMESharp.HTTP;
using ACMESharp.Messages;
using System;
using System.IO;
using System.Net;

namespace ACMESharp {
 public partial class AcmeClient : IDisposable {
  public class AcmeHttpResponse {
   private String _ContentAsString;

   public AcmeHttpResponse ( HttpWebResponse resp ) {
    StatusCode = resp.StatusCode;
    Headers = resp.Headers;
    Links = new LinkCollection ( Headers.GetValues ( AcmeProtocol.HEADER_LINK ) );

    var rs = resp.GetResponseStream ();
    using ( var ms = new MemoryStream () ) {
     rs.CopyTo ( ms );
     RawContent = ms.ToArray ();
    }
   }

   public HttpStatusCode StatusCode { get; set; }

   public WebHeaderCollection Headers { get; set; }

   public LinkCollection Links { get; set; }

   public Byte[] RawContent { get; set; }

   public String ContentAsString {
    get {
     if ( _ContentAsString == null ) {
      if ( RawContent == null || RawContent.Length == 0 ) {
       return null;
      }

      using ( var ms = new MemoryStream ( RawContent ) ) {
       using ( var sr = new StreamReader ( ms ) ) {
        _ContentAsString = sr.ReadToEnd ();
       }
      }
     }
     return _ContentAsString;
    }
   }

   public Boolean IsError { get; set; }

   public Exception Error { get; set; }

   public ProblemDetailResponse ProblemDetail { get; set; }
  }
 }
}