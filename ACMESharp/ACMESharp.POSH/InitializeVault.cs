using ACMESharp.Vault.Model;
using ACMESharp.Vault.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace ACMESharp.POSH {
 [Cmdlet ( VerbsData.Initialize, @"Vault", DefaultParameterSetName = PSET_BASE_SERVICE )]
 public class InitializeVault : Cmdlet {
  public const String PSET_BASE_SERVICE = @"BaseService";
  public const String PSET_BASE_URI = @"BaseUri";

  public const String WELL_KNOWN_LE = @"LetsEncrypt";
  public const String WELL_KNOWN_LESTAGE = @"LetsEncrypt-STAGING";

  public static readonly IReadOnlyDictionary<String, String> WELL_KNOWN_BASE_SERVICES =
          new ReadOnlyDictionary<String, String> ( new IndexedDictionary<String, String> {
           [ WELL_KNOWN_LE ] = @"https://acme-v01.api.letsencrypt.org/",
           [ WELL_KNOWN_LESTAGE ] = @"https://acme-staging.api.letsencrypt.org/",
          } );

  [Parameter ( ParameterSetName = PSET_BASE_SERVICE )]
  [ValidateSet ( WELL_KNOWN_LE, WELL_KNOWN_LESTAGE, IgnoreCase = true )]
  public String BaseService { get; set; } = WELL_KNOWN_LE;

  [Parameter ( ParameterSetName = PSET_BASE_URI, Mandatory = true )]
  [ValidateNotNullOrEmpty]
  public String BaseUri { get; set; }

  [Parameter]
  public SwitchParameter Force { get; set; }

  [Parameter]
  public String Alias { get; set; }

  [Parameter]
  public String Label { get; set; }

  [Parameter]
  public String Memo { get; set; }

  [Parameter]
  public String VaultProfile { get; set; }

  protected override void ProcessRecord () {
   var baseUri = BaseUri;
   if ( String.IsNullOrEmpty ( baseUri ) ) {
    if ( !String.IsNullOrEmpty ( BaseService ) && WELL_KNOWN_BASE_SERVICES.ContainsKey ( BaseService ) ) {
     baseUri = WELL_KNOWN_BASE_SERVICES[ BaseService ];
     WriteVerbose ( $"Resolved Base URI from Base Service [{baseUri}]" );
    } else {
     throw new PSInvalidOperationException ( @"Either a base service or URI is required" );
    }
   }

   using ( var vlt = Util.VaultHelper.GetVault ( VaultProfile ) ) {
    WriteVerbose ( @"Initializing Storage Backend" );
    vlt.InitStorage ( Force );
    var v = new VaultInfo {
     Id = EntityHelper.NewId (),
     Alias = Alias,
     Label = Label,
     Memo = Memo,
     BaseService = BaseService,
     BaseUri = baseUri,
     ServerDirectory = new AcmeServerDirectory ()
    };

    vlt.SaveVault ( v );
   }
  }
 }
}