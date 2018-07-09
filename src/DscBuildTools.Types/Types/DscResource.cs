using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace DscBuildTools.Types
{
    [RuntimeNameProperty(nameof(Name))]
    public class NameValuePair : DependencyObject
    {
        private string _value;

        public NameValuePair()
        {
            Name = string.Empty;
            Value = string.Empty;
        }

        public NameValuePair(string name)
        {
            Name = name;
            Value = Value;
        }

        [Category("Common")]

        [Description("The value mapped name for a property type.")]
        public string Name { get; set; }

        [Category("Common")]
        [Description("The value mapped value for a property type.")]
        public string Value
        {
            get
            {
                return _value ?? Name;
            }
            set
            {
                _value = value;
            }
        }
    }

    [ContentProperty(nameof(DscResources))]   
    public class DesiredStateConfiguration : DependencyObject, ISupportInitialize
    {
        public DesiredStateConfiguration()
        {
            DscResources = new ObservableCollection<DscResource>();
            RootNamespace = string.Empty;
            OutputDirectory = string.Empty;
        }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public string RootNamespace { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public string OutputDirectory { get; set; }


        public ObservableCollection<DscResource> DscResources { get; set; }

        public void Initialize()
        {
            ((ISupportInitialize)this).EndInit();
        }

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
            if (DscResources != null)
                foreach(var resource in DscResources)
                {
                    resource.Configuration = this;
                    resource.Initialize();
                }
        }
    }

    // [DesignerCategory("Advanced")]
    public class ClassVersion : DependencyObject
    {
        public static ClassVersion Default { get; } = new ClassVersion(0, 0, 0, 0);

        public static ClassVersion From(Version version)
        {
            return new ClassVersion(version.Major, version.Minor, version.Build, version.Revision);
        }
        public ClassVersion(int major, int minor, int build, int revision)
        {
            if (major < 0)
            {
                throw new ArgumentOutOfRangeException("major");
            }
            if (minor < 0)
            {
                throw new ArgumentOutOfRangeException("minor");
            }
            if (build < 0)
            {
                throw new ArgumentOutOfRangeException("build");
            }
            if (revision < 0)
            {
                throw new ArgumentOutOfRangeException("revision");
            }

            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public int Major { get; }
        public int Minor { get; }
        public int Build { get; }
        public int Revision { get; }
    }

    public class ClassVersionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string stringValue)
            {
                if (Version.TryParse(stringValue, out Version result))
                {
                    return ClassVersion.From(result);
                }
            }

            return ClassVersion.Default;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is ClassVersion classVersion)
            {
                return $"{classVersion.Major}.{classVersion.Minor}.{classVersion.Build}.{classVersion.Revision}";
            }

            return string.Empty;
        }

        //public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        //{
        //    return base.GetStandardValuesSupported(context);
        //}
    }

    [RuntimeNameProperty(nameof(Name))]
    [ContentProperty(nameof(Properties))]
    [Description("Represents a Desired State Configuration Rsource.")]
    public class DscResource : DependencyObject, ISupportInitialize
    {
        public DscResource()
        {
            Name = string.Empty;
            FriendlyName = string.Empty;
            ClassVersion = ClassVersion.Default;
            Properties = new ObservableCollection<DscResourceProperty>();
        }

        public void Initialize()
        {
            ((ISupportInitialize)this).EndInit();
        }

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {
            if (Properties != null)
                foreach (var property in Properties)
                {
                    property.OwningResource = this;
                    property.Initialize();
                }
        }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public string Name { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public string FriendlyName { get; set; }

        
        [TypeConverter(typeof(ClassVersionConverter))]
        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public ClassVersion ClassVersion { get; set; }

        public ObservableCollection<DscResourceProperty> Properties { get; set; }

        public DesiredStateConfiguration Configuration { get; internal set; }

    }


    
    public abstract class DscResourceProperty : DependencyObject, ISupportInitialize
    {
        protected DscResourceProperty()
        {
            PossibleValues = new ObservableCollection<NameValuePair>();
            Name = string.Empty;
            Description = string.Empty;
            PropertyType = DscResourceType.String;
        }

        public abstract string Name { get; set; }

        public abstract string Description { get; set; }

        public abstract DscResourceType PropertyType { get; set; }

        public abstract DscResourceAttribute Attribute { get; }

        public abstract ObservableCollection<NameValuePair> PossibleValues { get; set; }

        public string[] ValueMap
        {
            get
            {
                ArrayBuilder<string> builder = new ArrayBuilder<string>();
                foreach(var nvp in PossibleValues)
                {
                    builder.Add(nvp.Name);
                }
                return builder.ToArray();
            }
        }

        public string[] Values
        {
            get
            {
                ArrayBuilder<string> builder = new ArrayBuilder<string>();
                foreach (var nvp in PossibleValues)
                {
                    builder.Add(nvp.Value);
                }
                return builder.ToArray();
            }
        }


        public DscResource OwningResource { get; internal set; }

        public void Initialize()
        {
            ((ISupportInitialize)this).EndInit();
        }

        void ISupportInitialize.BeginInit()
        {
        }

        void ISupportInitialize.EndInit()
        {

        }
    }

    [RuntimeNameProperty(nameof(Name))]
    [ContentProperty(nameof(PossibleValues))]
    public sealed class DscResourceKeyProperty : DscResourceProperty
    {
        public override DscResourceAttribute Attribute => DscResourceAttribute.Key;

        [TypeConverter(typeof(PossibleValuesConverter))]
        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override ObservableCollection<NameValuePair> PossibleValues { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Name { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Description { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override DscResourceType PropertyType { get; set; }
    }

    [RuntimeNameProperty(nameof(Name))]
    [ContentProperty(nameof(PossibleValues))]
    public sealed class DscResourceWriteProperty : DscResourceProperty
    {
        public override DscResourceAttribute Attribute => DscResourceAttribute.Write;

        [TypeConverter(typeof(PossibleValuesConverter))]
        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override ObservableCollection<NameValuePair> PossibleValues { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Name { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Description { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override DscResourceType PropertyType { get; set; }
    }

    [RuntimeNameProperty(nameof(Name))]
    [ContentProperty(nameof(PossibleValues))]
    public sealed class DscResourceReadProperty : DscResourceProperty
    {
        public override DscResourceAttribute Attribute => DscResourceAttribute.Read;

        [TypeConverter(typeof(PossibleValuesConverter))]
        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override ObservableCollection<NameValuePair> PossibleValues { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Name { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Description { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override DscResourceType PropertyType { get; set; }
    }

    [RuntimeNameProperty(nameof(Name))]
    [ContentProperty(nameof(PossibleValues))]
    public sealed class DscResourceRequiredProperty : DscResourceProperty
    {
        public override DscResourceAttribute Attribute => DscResourceAttribute.Required;

        [TypeConverter(typeof(PossibleValuesConverter))]
        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override ObservableCollection<NameValuePair> PossibleValues { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Name { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override string Description { get; set; }

        [DesignerSerializationOptions(DesignerSerializationOptions.SerializeAsAttribute)]
        public override DscResourceType PropertyType { get; set; }
    }

    public class PossibleValuesConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return false;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            StringBuilder builder = new StringBuilder();
            bool first = true;
            if (value is ObservableCollection<NameValuePair> collection)
            {
                foreach(var pair in collection)
                {
                    if (first)
                    {
                        builder.Append(pair.Name);
                        first = false;
                    }
                    else
                    {
                        builder.Append(';');
                        builder.Append(pair.Name);
                    }
                }
            }
            return builder.ToString();
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            ObservableCollection<NameValuePair> collection = new ObservableCollection<NameValuePair>();
            if (value is string stringValue)
            {
                if (stringValue.Contains(";"))
                {
                    foreach (string splitValue in stringValue.Split(';'))
                    {
                        collection.Add(new NameValuePair { Name = splitValue });
                    }
                }
            }
            return collection;
        }
    }

    public enum DscResourceType
    {
        Boolean,
        BooleanArray,
        Char,
        CharArray,
        DateTime,
        DateTimeArray,
        Single,
        SingleArray,
        Double,
        DoubleArray,
        Int16,
        Int16Array,
        Int32,
        Int32Array,
        Int64,
        Int64Array,
        SByte,
        SByteArray,
        String,
        StringArray,
        UInt16,
        UInt16Array,
        UInt32,
        UInt32Array,
        UInt64,
        UInt64Array,
        Byte,
        ByteArray,

        // Embedded Instances
        HashTable,
        HashTableArray,
        PSCredential,
        PSCredentialArray,
    }

    public enum DscResourceAttribute
    {
        Key,
        Required,
        Read,
        Write
    }
}
