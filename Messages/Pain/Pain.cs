// https://www.iso20022.org/iso-20022-message-definitions?business-domain=1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Iso20022.Messages.Pain
{

    // Namespace declaration to match the XML namespace in the XSD
    [XmlRoot(ElementName = "Document", Namespace = "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03")]
    public class CustomerCreditTransferInitiationDocument
    {
        [XmlElement("CstmrCdtTrfInitn")]
        public CustomerCreditTransferInitiation? CustomerCreditTransferInitiation { get; set; }
    }

    public class CustomerCreditTransferInitiation
    {
        [XmlElement("GrpHdr")]
        public GroupHeader? GroupHeader { get; set; }

        [XmlElement("PmtInf")]
        public List<PaymentInformation> PaymentInformationList { get; set; }

        public CustomerCreditTransferInitiation()
        {
            PaymentInformationList = [];
        }
    }

    public class GroupHeader
    {
        [XmlElement("MsgId")]
        [Required, MaxLength(35)]
        public string? MessageIdentification { get; set; }

        [XmlElement("CreDtTm")]
        public DateTime CreationDateTime { get; set; }

        [XmlElement("NbOfTxs")]
        public string? NumberOfTransactions { get; set; }

        [XmlElement("CtrlSum")]
        public decimal? ControlSum { get; set; } // Optional, so it's nullable

        [XmlElement("InitgPty")]
        public Party? InitiatingParty { get; set; }
    }

    public class Party
    {
        [XmlElement("Nm")]
        [MaxLength(70)]
        public string? Name { get; set; }

        // Include additional properties for PostalAddress, OrganisationIdentification, etc.
        // Depending on the XSD structure, these can be complex types
    }

    public class PaymentInformation
    {
        [XmlElement("PmtInfId")]
        [Required, MaxLength(35)]
        public string PaymentInformationIdentification { get; set; }

        [XmlElement("PmtMtd")]
        public string PaymentMethod { get; set; } // Consider using an enumeration for defined values

        [XmlElement("BtchBookg")]
        public bool? BatchBooking { get; set; } // Optional, so it's nullable

        // Include other properties like RequestedExecutionDate, Debtor, ChargeBearer, etc.

        [XmlElement("CdtTrfTxInf")]
        public List<CreditTransferTransactionInformation> CreditTransferTransactionInformationList { get; set; }

        public PaymentInformation()
        {
            CreditTransferTransactionInformationList = new List<CreditTransferTransactionInformation>();
        }
    }

    // Define other necessary classes like CreditTransferTransactionInformation, Debtor, Creditor, etc.
    // Include properties based on the XSD structure, paying attention to types, multiplicity, and validation rules.

}