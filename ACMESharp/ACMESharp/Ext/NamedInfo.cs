namespace ACMESharp.Ext {
 public struct NamedInfo<TInfo> {
  public NamedInfo ( System.String name, TInfo info ) {
   Name = name;
   Info = info;
  }

  public System.String Name { get; }

  public TInfo Info { get; }
 }
}