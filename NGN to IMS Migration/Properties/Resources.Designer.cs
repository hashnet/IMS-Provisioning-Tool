﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NGN_to_IMS_Migration.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NGN_to_IMS_Migration.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon icon {
            get {
                object obj = ResourceManager.GetObject("icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap logo {
            get {
                object obj = ResourceManager.GetObject("logo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to itbilling.
        /// </summary>
        internal static string pgwPass {
            get {
                return ResourceManager.GetString("pgwPass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to itbilling.
        /// </summary>
        internal static string pgwUser {
            get {
                return ResourceManager.GetString("pgwUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Huawei!@34.
        /// </summary>
        internal static string spgPass {
            get {
                return ResourceManager.GetString("spgPass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to bsstest.
        /// </summary>
        internal static string spgUser {
            get {
                return ResourceManager.GetString("spgUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://10.215.32.30:8001;http://10.215.34.30:8001;.
        /// </summary>
        internal static string UrlPGW {
            get {
                return ResourceManager.GetString("UrlPGW", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://10.215.134.00:8080/spg;http://10.215.23.12:8080.
        /// </summary>
        internal static string urlSPG {
            get {
                return ResourceManager.GetString("urlSPG", resourceCulture);
            }
        }
    }
}
