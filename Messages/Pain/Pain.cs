// https://www.iso20022.org/iso-20022-message-definitions?business-domain=1
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

/// This classes are based on the ISO 20022 message definitions for the pain.001.001.03 message.
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

    public class CreditTransferTransactionInformation
    {
        [XmlElement("PmtId")]
        public PaymentIdentification PaymentIdentification { get; set; }

        [XmlElement("Amt")]
        public Amount Amount { get; set; }

        [XmlElement("ChrgBr")]
        public ChargeBearer ChargeBearer { get; set; }

        [XmlElement("Dbtr")]
        public Party Debtor { get; set; }

        [XmlElement("DbtrAcct")]
        public Account DebtorAccount { get; set; }

        [XmlElement("DbtrAgt")]
        public FinancialInstitution DebtorAgent { get; set; }

        [XmlElement("CdtrAgt")]
        public FinancialInstitution CreditorAgent { get; set; }

        [XmlElement("Cdtr")]
        public Party Creditor { get; set; }

        [XmlElement("CdtrAcct")]
        public Account CreditorAccount { get; set; }

        [XmlElement("RmtInf")]
        public RemittanceInformation RemittanceInformation { get; set; }
    }

    public class PaymentIdentification
    {
        [XmlElement("InstrId")]
        public string InstructionIdentification { get; set; }

        [XmlElement("EndToEndId")]
        public string EndToEndIdentification { get; set; }
    }

    public class Amount
    {
        [XmlElement("InstdAmt")]
        public InstructedAmount InstructedAmount { get; set; }
    }

    public class InstructedAmount
    {
        [XmlAttribute("Ccy")]
        public string Currency { get; set; }

        [XmlText]
        public decimal Value { get; set; }
    }

    public enum ChargeBearer
    {
        DEBT,
        CRED,
        SHAR,
        SLEV
    }

    public class FinancialInstitution
    {
        [XmlElement("FinInstnId")]
        public FinancialInstitutionIdentification FinancialInstitutionIdentification { get; set; }
    }

    public class FinancialInstitutionIdentification
    {
        [XmlElement("BIC")]
        public string BIC { get; set; }
    }

    public class Account
    {
        [XmlElement("Id")]
        public AccountIdentification Id { get; set; }
    }

    public class AccountIdentification
    {
        [XmlElement("IBAN")]
        public string IBAN { get; set; }

        // For non-IBAN countries
        [XmlElement("Othr")]
        public OtherAccountIdentification Other { get; set; }
    }

    public class OtherAccountIdentification
    {
        [XmlElement("Id")]
        public string Identification { get; set; }
    }

    public class RemittanceInformation
    {
        [XmlElement("Ustrd")]
        public List<string> Unstructured { get; set; }

        public RemittanceInformation()
        {
            Unstructured = new List<string>();
        }
    }

}