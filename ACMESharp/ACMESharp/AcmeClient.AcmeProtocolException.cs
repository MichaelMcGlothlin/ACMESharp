using System;
using System.Runtime.Serialization;

namespace ACMESharp {
 public partial class AcmeClient : IDisposable {
  public class AcmeProtocolException : AcmeException {
   public AcmeProtocolException ( String message, AcmeHttpResponse response = null, Exception innerException = null ) : base ( message, innerException ) => Response = response;

   protected AcmeProtocolException ( SerializationInfo info, StreamingContext context ) : base ( info, context ) { }

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