﻿using System;
using System.ComponentModel.Composition;

namespace ACMESharp.ACME {
 [MetadataAttribute]
 [AttributeUsage ( AttributeTargets.Class, AllowMultiple = false )]
 public class ChallengeDecoderProviderAttribute : ExportAttribute {
  public ChallengeDecoderProviderAttribute ( String type,
          ChallengeTypeKind supportedType ) : base ( typeof ( IChallengeDecoderProvider ) ) {
   Type = type;
   SupportedType = supportedType;
  }

  public ChallengeTypeKind SupportedType { get; }

  public String Type { get; }

  public String Label { get; set; }

  public String Description { get; set; }
 }
}