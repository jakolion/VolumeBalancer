﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VolumeBalancer.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.3.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string mainFocusApplication {
            get {
                return ((string)(this["mainFocusApplication"]));
            }
            set {
                this["mainFocusApplication"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public uint balancePosition {
            get {
                return ((uint)(this["balancePosition"]));
            }
            set {
                this["balancePosition"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string hotkeyIncreaseFocusApplicationVolume {
            get {
                return ((string)(this["hotkeyIncreaseFocusApplicationVolume"]));
            }
            set {
                this["hotkeyIncreaseFocusApplicationVolume"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string hotkeyIncreaseOtherApplicationVolume {
            get {
                return ((string)(this["hotkeyIncreaseOtherApplicationVolume"]));
            }
            set {
                this["hotkeyIncreaseOtherApplicationVolume"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string hotkeyResetBalance {
            get {
                return ((string)(this["hotkeyResetBalance"]));
            }
            set {
                this["hotkeyResetBalance"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string hotkeyActivateMainFocusApplication {
            get {
                return ((string)(this["hotkeyActivateMainFocusApplication"]));
            }
            set {
                this["hotkeyActivateMainFocusApplication"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string hotkeyActivateTemporaryFocusApplication {
            get {
                return ((string)(this["hotkeyActivateTemporaryFocusApplication"]));
            }
            set {
                this["hotkeyActivateTemporaryFocusApplication"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string hotkeyResetAllVolumes {
            get {
                return ((string)(this["hotkeyResetAllVolumes"]));
            }
            set {
                this["hotkeyResetAllVolumes"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string trayIcon {
            get {
                return ((string)(this["trayIcon"]));
            }
            set {
                this["trayIcon"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool balanceSystemSound {
            get {
                return ((bool)(this["balanceSystemSound"]));
            }
            set {
                this["balanceSystemSound"] = value;
            }
        }
    }
}
