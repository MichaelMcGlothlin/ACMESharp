using ACMESharp.Util;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace ACMESharp {
 public partial class AcmeClient : IDisposable {
  public class AcmeWebException : AcmeException {
   public AcmeWebException ( WebException innerException, String message = null, AcmeHttpResponse response = null ) : base ( message, innerException ) {
    Response = response;
    if ( Response?.ProblemDetail?.OrignalContent != null ) {
     this.With ( nameof ( Response.ProblemDetail ),
             Response.ProblemDetail.OrignalContent );
    }
   }

   protected AcmeWebException ( SerializationInfo info, StreamingContext context ) : base ( info, context ) { }

   public WebException WebException => InnerException as WebException;

   public AcmeHttpResponse Response { get; }

   public override String Message {
    get {
     if ( Response != null ) {
      return base.Message + "\n +Response from server:\n\t+ Code: " + Response.StatusCode.ToString () + "\n\t+ Content: " + Response.ContentAsString;
     } else {
      return base.Message + "\n +No response from server";
     }
    }
   }
  }
 }
}