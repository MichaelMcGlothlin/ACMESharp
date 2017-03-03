using System;
using System.ComponentModel.Composition;

namespace ACMESharp.ACME {
 [MetadataAttribute]
 [AttributeUsage ( AttributeTargets.Class, AllowMultiple = false )]
 public class ChallengeHandlerProviderAttribute : ExportAttribute {
  public ChallengeHandlerProviderAttribute ( String name,
          ChallengeTypeKind supportedTypes ) : base ( typeof ( IChallengeHandlerProvider ) ) {
   Name = name;
   SupportedTypes = supportedTypes;
  }

  public String Name { get; }

  public ChallengeTypeKind SupportedTypes { get; }

  public String[] Aliases { get; set; }

  public String Label { get; set; }

  public String Description { get; set; }
 }
}