namespace GORAKSHANA.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }


    public enum Common
    {
        SponserType,
        EventType,
        TamilYear,
        TamilMonth,
        Star,
        Patcam,
        Tithi


    }
    public enum sponserType
    {
        myself,
        others
    }

    public class SelectListItem
    {
        public string Value { get; set; }

        public string Text { get; set; }

        public string template
        {
            get
            {
                return $"<option value={Value}>{Text}</option>";
            }
        }
    }

    public enum paymentMode
    {
        Cash, Cheque, Netbanking, others
    }

    public enum eventType
    {
        Birthday,
        Memorial,
        Remembrance,
        Wedding,
        Others
    }

    public enum referenceType
    {
        Direct,
        Others
    }

    public enum tamilYr
    {
        Prabhava,
        Vibhava,
        Sukla,
        Pramodoota,
        Prachorpaththi,
        Aangirasa,
        Srimukha,
        Bhava,
        Yuva,
        Dhaatu,
        Eesvara,
        Vehudhanya,
        Pramathi,
        Vikrama,
        Vishu,
        Chitrabaanu,
        Subhaanu,
        Dhaarana,
        Paarthiba,
        Viya,
        Sarvajith,
        Sarvadhari,
        Virodhi,
        Vikruthi,
        Kara,
        Nandhana,
        Vijaya,
        Jaya,
        Manmatha,
        Dhunmuki,
        Hevilambi,
        Vilambi,
        Vikari,
        Sarvari,
        Plava,
        Subakrith,
        Sobakrith,
        Krodhi,
        Visuvaasuva,
        Parabhaava,
        Plavanga,
        Keelaka,
        Saumya,
        Sadharana,
        Virodhikrithu,
        Paridhaabi,
        Pramaadhisa,
        Aanandha,
        Rakshasa,
        Nala,
        Pingala,
        Kalayukthi,
        Siddharthi,
        Raudhri,
        Dunmathi,
        Dhundubhi,
        Rudhrodhgaari,
        Raktakshi,
        Krodhana,
        Akshaya
    }

    public enum tamilMon
    {
        Chitthirai,
        Vaikasi,
        Aani,
        Aadi,
        Aavaṇi,
        Purattasi,
        Aippasi,
        Karthikai,
        Markazhi,
        Thai,
        Masi,
        Panguni
    }

    public enum star
    {
        Asvini,
        Bharani,
        Krttika,
        Rohini,
        Mrgasirsa,
        Ardra,
        Punarvasu,
        Pusya,
        Aslesa,
        Magha,
        Purva,
        Uttara,
        Hasta,
        Chitrā,
        Svati,
        Visakha,
        Anuradha,
        Jyestha,
        Mula,
        Purva_Asadha,
        Uttara_Asadha,
        Sravana,
        Sravistha,
        Satabhisa,
        Purva_Bhadrapada,
        Uttara_Bhadrapada,
        Revati
    }

    public enum Patcam
    {
        Sukla,
        Krishna
    }
    public enum thithi
    {
        Prathama,
        Dwitiya,
        Tritiya,
        Chaturthi,
        Panchami,
        Shashti,
        Saptami,
        Ashtami,
        Navami,
        Dashami,
        Ekadashi,
        Dwadashi,
        Thrayodashi,
        Chaturdashi,
        Purnima,
        Amavasai
    }
}
