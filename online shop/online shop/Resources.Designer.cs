﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace online_shop {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("online_shop.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to You can just have one Seller !!.
        /// </summary>
        internal static string AddSellerException {
            get {
                return ResourceManager.GetString("AddSellerException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Delete wasn`t successfull !!.
        /// </summary>
        internal static string DeleteAddressException {
            get {
                return ResourceManager.GetString("DeleteAddressException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Otp code is not valid !.
        /// </summary>
        internal static string OtpInvalidException {
            get {
                return ResourceManager.GetString("OtpInvalidException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update address wasn`t successfull !!.
        /// </summary>
        internal static string UpdateAddressException {
            get {
                return ResourceManager.GetString("UpdateAddressException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Update Seller wasn`t successfull !!.
        /// </summary>
        internal static string UpdateSellerException {
            get {
                return ResourceManager.GetString("UpdateSellerException", resourceCulture);
            }
        }
    }
}
