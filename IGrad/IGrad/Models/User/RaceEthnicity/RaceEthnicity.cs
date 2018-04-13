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

        //AMERICAN INDIAN & ALASKAN NATIVE -------------------------------------------------------------
        public bool isAmericanIndian { get; set; }
        [DisplayName("Alaska Native")]
        public bool isAlaskaNative { get; set; }
        [DisplayName("Chehalis")]
        public bool isChehalis { get; set; }
        [DisplayName("Colville")]
        public bool isColville { get; set; }
        [DisplayName("Cowlitz")]
        public bool isCowlitz { get; set; }
        [DisplayName("Hoh")]
        public bool isHoh { get; set; }
        [DisplayName("Hames")]
        public bool isHames { get; set; }
        [DisplayName("Kalispel")]
        public bool isKalispel { get; set; }
        [DisplayName("Lower Elwha")]
        public bool isLowerElwha { get; set; }
        [DisplayName("Lummi")]
        public bool isLummi { get; set; }
        [DisplayName("Makah")]
        public bool isMakah { get; set; }
        [DisplayName("Muckleshoot")]
        public bool isMuckleshoot { get; set; }
        [DisplayName("Nisqually")]
        public bool isNisqually { get; set; }
        [DisplayName("Nooksack")]
        public bool isNooksack { get; set; }
        [DisplayName("Port Gamble Clallam")]
        public bool isPortGambleClallam { get; set; }
        [DisplayName("Puyallup")]
        public bool isPuyallup { get; set; }
        [DisplayName("Quileute")]
        public bool isQuileute { get; set; }
        [DisplayName("Quinault")]
        public bool isQuinault { get; set; }
        [DisplayName("Samish")]
        public bool isSamish { get; set; }
        [DisplayName("Sauk-Suiattle")]
        public bool isSauk_Suiattle { get; set; }
        [DisplayName("Shoalwater")]
        public bool isShoalwater { get; set; }
        [DisplayName("Skokomish")]
        public bool isSkokomish { get; set; }
        [DisplayName("Snoqualmie")]
        public bool isSnoqualmie { get; set; }
        [DisplayName("Spokane")]
        public bool isSpokane { get; set; }
        [DisplayName("Squaxin Island")]
        public bool isSquaxinIsland { get; set; }
        [DisplayName("Stillaguamish")]
        public bool isStillaguamish { get; set; }
        [DisplayName("Suquamish")]
        public bool isSuquamish { get; set; }
        [DisplayName("Swinomish")]
        public bool isSwinomish { get; set; }
        [DisplayName("Tulalip")]
        public bool isTulalip { get; set; }
        [DisplayName("Upper Skagit")]
        public bool isUpperSkagit { get; set; }
        [DisplayName("Yakama")]
        public bool isYakama { get; set; }
        [DisplayName("Other Indian")]
        public bool isOtherWashingtonIndian { get; set; }
        [DisplayName("Other North, Central, or South American Indian")]
        public bool isOtherNorthCentralOrSouthAmericanIndian { get; set; }


        //HISPANIC / LATINO -------------------------------------------------------------
        [DisplayName("Not Hispanic/Latino")]
        public bool isNotHispanic { get; set; }
        [DisplayName("Cuban")]
        public bool isCuban { get; set; }
        [DisplayName("Dominican")]
        public bool isDominican { get; set; }
        [DisplayName("Spaniard")]
        public bool isSpaniard { get; set; }
        [DisplayName("Puerto Rican")]
        public bool isPuertoRican { get; set; }
        [DisplayName("Mexican")]
        public bool isMexican { get; set; }
        [DisplayName("Central American")]
        public bool isCentralAmerican { get; set; }
        [DisplayName("South American")]
        public bool isSouthAmerican { get; set; }
        [DisplayName("Latin American")]
        public bool isLatinAmerican { get; set; }
        [DisplayName("Other Hispanic")]
        public bool isOtherHispanic { get; set; }


        //NON HISPANIC -------------------------------------------------------------
        [DisplayName("African")]
        public bool isAfrican { get; set; }
        [DisplayName("African American")]
        public bool isAfricanAmerican { get; set; }
        [DisplayName("Black")]
        public bool isBlack { get; set; }
        [DisplayName("Haitian")]
        public bool isHaitian { get; set; }
        [DisplayName("Ethiopian")]
        public bool isEthiopian { get; set; }
        [DisplayName("White")]
        public bool isWhite { get; set; }
        [DisplayName("Caucasian")]
        public bool isCaucasian { get; set; }
        [DisplayName("European")]
        public bool isEuropean { get; set; }
        [DisplayName("Russian")]
        public bool isRussian { get; set; }
        [DisplayName("Middle Eastern")]
        public bool isMiddleEastern { get; set; }
        [DisplayName("North African")]
        public bool isNorthAfrican { get; set; }


        //NATIVE HAWAIIAN / OTHER PACIFIC ISLANDER ------------------------------------------------
        [DisplayName("Native Hawaiiwan")]
        public bool isNativeHawaiiwan { get; set; }
        [DisplayName("Fijian")]
        public bool isFijian { get; set; }
        [DisplayName("Guamanian")]
        public bool isGuamanian { get; set; }
        [DisplayName("Chamorro")]
        public bool isChamorro { get; set; }
        [DisplayName("Marianna Islander")]
        public bool isMariannaIslander { get; set; }
        [DisplayName("Melanesian")]
        public bool isMelanesian { get; set; }
        [DisplayName("Samoan")]
        public bool isSamoan { get; set; }
        [DisplayName("Tongan")]
        public bool isTongan { get; set; }
        [DisplayName("Other Pacific Islander")]
        public bool isOtherPacificIslander { get; set; }

    }
}