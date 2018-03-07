using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    [DisplayName("Select your race")]
    public class RaceEthnicity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }

        //ASIAN -------------------------------------------------------------
        [DisplayName("Asian Indian")]
        public bool isAsianIndian { get; set; }
        [DisplayName("Cambodian")]
        public bool isCambodian { get; set; }
        [DisplayName("Chinese")]
        public bool isChinese { get; set; }
        [DisplayName("Filipino")]
        public bool isFilipino { get; set; }
        [DisplayName("Hmong")]
        public bool isHmong { get; set; }
        [DisplayName("Indonesian")]
        public bool isIndonesian { get; set; }
        [DisplayName("Japanese")]
        public bool isJapanese { get; set; }
        [DisplayName("Korean")]
        public bool isKorean { get; set; }
        [DisplayName("Laotian")]
        public bool isLaotian { get; set; }
        [DisplayName("Malaysian")]
        public bool isMalaysian { get; set; }
        [DisplayName("Pakistani")]
        public bool isPakistani { get; set; }
        [DisplayName("Singaporean")]
        public bool isSingaporean { get; set; }
        [DisplayName("Taiwanese")]
        public bool isTaiwanese { get; set; }
        [DisplayName("Thai")]
        public bool isThai { get; set; }
        [DisplayName("Vietnamese")]
        public bool isVietnamese { get; set; }
        [DisplayName("Other Asian")]
        public bool isOtherAsian { get; set; }

    }
}