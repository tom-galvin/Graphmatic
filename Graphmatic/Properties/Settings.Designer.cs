﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Graphmatic.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        /// <summary>
        /// Default background color for new document pages.
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Default background color for new document pages.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::System.Drawing.Color DefaultPageColor {
            get {
                return ((global::System.Drawing.Color)(this["DefaultPageColor"]));
            }
            set {
                this["DefaultPageColor"] = value;
            }
        }
        
        /// <summary>
        /// Default username for users.
        /// </summary>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsDescriptionAttribute("Default username for users.")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Anonymous")]
        public string Username {
            get {
                return ((string)(this["Username"]));
            }
            set {
                this["Username"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("y")]
        public char DefaultPlottedVariable {
            get {
                return ((char)(this["DefaultPlottedVariable"]));
            }
            set {
                this["DefaultPlottedVariable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("x")]
        public char DefaultVaryingVariable {
            get {
                return ((char)(this["DefaultVaryingVariable"]));
            }
            set {
                this["DefaultVaryingVariable"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfChar xmlns:xsi=\"http://www.w3.or" +
            "g/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <cha" +
            "r>120</char>\r\n  <char>121</char>\r\n</ArrayOfChar>")]
        public char[] DefaultDataSetVariables {
            get {
                return ((char[])(this["DefaultDataSetVariables"]));
            }
            set {
                this["DefaultDataSetVariables"] = value;
            }
        }
    }
}
