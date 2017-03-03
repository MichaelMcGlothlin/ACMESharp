using System;

namespace ACMESharp.PKI.RSA {
 public class RsaPrivateKey : PrivateKey {
  public RsaPrivateKey ( Int32 bits, String e, String pem ) {
   Bits = bits;
   E = e;
   Pem = pem;
  }

  public Int32 Bits { get; }

  public String E { get; }

  public Object BigNumber { get; set; }

  public String Pem { get; }
 }
}