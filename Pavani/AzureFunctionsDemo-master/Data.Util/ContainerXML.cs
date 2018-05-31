using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Util
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class EnumerationResults
    {
        private EnumerationResultsContainers containersField;

        private object nextMarkerField;

        private string[] textField;

        private string serviceEndpointField;

        /// <remarks/>
        public EnumerationResultsContainers Containers
        {
            get
            {
                return this.containersField;
            }
            set
            {
                this.containersField = value;
            }
        }

        /// <remarks/>
        public object NextMarker
        {
            get
            {
                return this.nextMarkerField;
            }
            set
            {
                this.nextMarkerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ServiceEndpoint
        {
            get
            {
                return this.serviceEndpointField;
            }
            set
            {
                this.serviceEndpointField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class EnumerationResultsContainers
    {
        private EnumerationResultsContainersContainer[] containerField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Container")]
        public EnumerationResultsContainersContainer[] Container
        {
            get
            {
                return this.containerField;
            }
            set
            {
                this.containerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class EnumerationResultsContainersContainer
    {
        private string nameField;

        private EnumerationResultsContainersContainerProperties propertiesField;

        private string[] textField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public EnumerationResultsContainersContainerProperties Properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class EnumerationResultsContainersContainerProperties
    {
        private string lastModifiedField;

        private string etagField;

        private string leaseStatusField;

        private string leaseStateField;

        private string publicAccessField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Last-Modified")]
        public string LastModified
        {
            get
            {
                return this.lastModifiedField;
            }
            set
            {
                this.lastModifiedField = value;
            }
        }

        /// <remarks/>
        public string Etag
        {
            get
            {
                return this.etagField;
            }
            set
            {
                this.etagField = value;
            }
        }

        /// <remarks/>
        public string LeaseStatus
        {
            get
            {
                return this.leaseStatusField;
            }
            set
            {
                this.leaseStatusField = value;
            }
        }

        /// <remarks/>
        public string LeaseState
        {
            get
            {
                return this.leaseStateField;
            }
            set
            {
                this.leaseStateField = value;
            }
        }

        /// <remarks/>
        public string PublicAccess
        {
            get
            {
                return this.publicAccessField;
            }
            set
            {
                this.publicAccessField = value;
            }
        }
    }
}