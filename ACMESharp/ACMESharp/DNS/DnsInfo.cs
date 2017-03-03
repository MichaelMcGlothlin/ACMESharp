using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace ACMESharp.DNS {
 public class DnsInfo {
  // TOOD: this is repeated from WebServerInfo, clean this up!
  private static readonly JsonSerializerSettings JSS = new JsonSerializerSettings { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto, };

  public String DefaultDomain { get; set; }

  public IXXXDnsProvider Provider { get; set; }

  public void Save ( Stream s ) {
   using ( var w = new StreamWriter ( s ) ) {
    w.Write ( JsonConvert.SerializeObject ( this, JSS ) );
   }
  }

  public String Save () {
   using ( var w = new MemoryStream () ) {
    Save ( w );
    return Encoding.UTF8.GetString ( w.ToArray () );
   }
  }

  public static DnsInfo Load ( Stream s ) {
   using ( var r = new StreamReader ( s ) ) {
    return JsonConvert.DeserializeObject<DnsInfo> ( r.ReadToEnd (), JSS );
   }
  }

  public static DnsInfo Load ( String json ) {
   using ( var r = new MemoryStream ( Encoding.UTF8.GetBytes ( json ) ) ) {
    return Load ( r );
   }
  }
 }
}