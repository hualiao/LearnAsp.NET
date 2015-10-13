﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApp.Helpers
{
    /// <summary>
    /// Ref:https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.appsettings%28v=vs.110%29.aspx
    /// </summary>
    public class PageAppearanceSection : ConfigurationSection
    {
        [ConfigurationProperty("remoteOnly",DefaultValue="false",IsRequired=false)]
        public Boolean RemoteOnly
        {
            get
            {
                return (Boolean)this["remoteOnly"];
            }
            set
            {
                this["remoteOnly"] = value;
            }
        }

        // Create a "font" element
        [ConfigurationProperty("font")]
        public FontElement Font
        {
            get
            {
                return (FontElement)this["font"];
            }
            set { this["font"] = value; }
        }

        // Create a "color" element
        [ConfigurationProperty("color")]
        public ColorElement Color
        {
            get
            {
                return (ColorElement)this["color"];
            }
            set { this["color"] = value; }
        }

        public class FontElement : ConfigurationElement
        {
            [ConfigurationProperty("name",DefaultValue="Arial",IsRequired=true)]
            [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
            public string Name
            {
                get
                {
                    return (String)this["name"];
                }
                set
                {
                    this["name"] = value;
                }
            }

            [ConfigurationProperty("size", DefaultValue = "12", IsRequired = false)]
            [IntegerValidator(ExcludeRange = false, MaxValue = 24, MinValue = 6)]
            public int Size
            {
                get
                { return (int)this["size"]; }
                set
                { this["size"] = value; }
            }
        }

        // Define the "color" element 
        // with "background" and "foreground" attributes.
        public class ColorElement : ConfigurationElement
        {
            [ConfigurationProperty("background", DefaultValue = "FFFFFF", IsRequired = true)]
            [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
            public String Background
            {
                get
                {
                    return (String)this["background"];
                }
                set
                {
                    this["background"] = value;
                }
            }

            [ConfigurationProperty("foreground", DefaultValue = "000000", IsRequired = true)]
            [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
            public String Foreground
            {
                get
                {
                    return (String)this["foreground"];
                }
                set
                {
                    this["foreground"] = value;
                }
            }

        }
    }
}