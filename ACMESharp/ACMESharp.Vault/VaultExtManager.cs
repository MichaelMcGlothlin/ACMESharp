﻿using ACMESharp.Ext;
using ACMESharp.Vault.Providers;
using System;
using System.Collections.Generic;

namespace ACMESharp.Vault {
 /// <summary>
 /// Extension Manager for the Vault subsystem.
 /// </summary>
 [ExtManager]
 public static class VaultExtManager {
  public static readonly Type DEFAULT_PROVIDER_TYPE =
          typeof ( LocalDiskVaultProvider );

  private static Config _config;
  private static String _defaultProviderName;
  private static readonly Object _lockObject = new Object ();

  public static String DefaultProviderName {
   get {
    AssertInit ();
    return _defaultProviderName;
   }
  }

  public static IEnumerable<NamedInfo<IVaultProviderInfo>> GetProviderInfos () {
   AssertInit ();
   foreach ( var pi in _config ) {
    yield return new NamedInfo<IVaultProviderInfo> (
            pi.Key, pi.Value.Metadata );
   }
  }

  public static IVaultProviderInfo GetProviderInfo ( String name ) {
   AssertInit ();
   return _config.Get ( name )?.Metadata;
  }

  public static IEnumerable<String> GetAliases () {
   AssertInit ();
   return _config.Aliases.Keys;
  }

  /// <remarks>
  /// An optional name may be given to distinguish between different
  /// available provider implementations.
  /// <!--
  /// Additionally, an optional
  /// set of named initialization parameters may be provided to
  /// further configure or qualify the Provider instance that is
  /// returned and ultimately the Components that the Provider
  /// produces.
  /// -->
  /// </remarks>
  public static IVaultProvider GetProvider ( String name = null,
      IReadOnlyDictionary<String, Object> reservedLeaveNull = null ) {
   AssertInit ();
   name = name ?? _defaultProviderName;

   return _config.Get ( name )?.Value;
  }

  /// <summary>
  /// Release existing configuration and registry and
  /// tries to rediscover and reload any providers.
  /// </summary>
  public static void Reload () => _config = ExtCommon.ReloadExtConfig<Config> ( _config );

  private static void AssertInit () {
   if ( _config == null ) {
    lock ( _lockObject ) {
     if ( _config == null ) {
      Reload ();
     }
    }
   }
   if ( _config == null ) {
    throw new InvalidOperationException ( "could not initialize provider configuration" );
   }
  }

  private class Config : ExtRegistry<IVaultProvider, IVaultProviderInfo> {
   public Config () : base ( _ => _.Name ) { }

   protected override void PostRegisterProvider (
           IVaultProviderInfo providerInfo, IVaultProvider provider, Boolean registered ) {
    // Special case for the default provider
    if ( _defaultProviderName == null && DEFAULT_PROVIDER_TYPE == provider.GetType () ) {
     _defaultProviderName = providerInfo.Name;
    }
   }
  }
 }
}