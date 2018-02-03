using System.ComponentModel;

namespace IGrad.Models.User
{
    public class RaceEthnicity
    {
        [DisplayName("Not Hispanic Or Latino")]
        public bool isNotHispanicOrLatino { get; set; }
        [DisplayName("Cuban")]
        public bool isCuban { get; set; }
        [DisplayName("Dominican")]
        public bool isDominican { get; set; }
        [DisplayName("Spanish")]
        public bool isSpaniard { get; set; }
        [DisplayName("Puerto RIcan")]
        public bool isPuertoRican { get; set; }
        [DisplayName("Mexican")]
        public bool isMexican { get; set; }
        [DisplayName("Central American")]
        public bool isCentralAmerican { get; set; }
        [DisplayName("South American")]
        public bool isSouthAmerican { get; set; }
        [DisplayName("Latin American")]
        public bool isLatinAmerican { get; set; }
        [DisplayName("Other Hispanic / Latino")]
        public bool isOtherHispanicLatino { get; set; }

    }
}