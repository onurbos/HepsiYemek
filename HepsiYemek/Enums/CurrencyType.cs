

using System.Runtime.Serialization;

namespace HepsiYemek.Enums
{
    public enum CurrencyType
    {
        [EnumMember(Value = "")]
        NotSet = 0,

        [EnumMember(Value = "TL")]
        TL = 1,

        [EnumMember(Value = "EURO")]
        EURO = 2,

        [EnumMember(Value = "DOLAR")]
        DOLAR = 3
    }
}
