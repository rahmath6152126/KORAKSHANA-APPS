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

    public enum sponserType
    {
        myself,
        myparent,
        myspouse,
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

    

    public enum eventType
    {
        Birthday,
        Memorial
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
        Vaikāsi,
        Āani,
        Āadi,
        Āavaṇi,
        Puraṭṭāsi,
        Aippasi,
        Kārthikai,
        Mārkazhi,
        Thai,
        Māsi,
        Paṅguni
    }

    public enum star
    {
        Aśvini,
        Bharaṇi,
        Kṛttikā,
        Rohiṇī,
        Mṛgaśīrṣā,
        Ārdrā,
        Punarvasu,
        Puṣya,
        Āśleṣā,
        Maghā,
        Pūrva,
        Uttara,
        Hasta,
        Chitrā,
        Svāti,
        Viśākhā,
        Anurādhā,
        Jyeṣṭhā,
        Mūla,
        Pūrva_Aṣāḍhā,
        Uttara_Aṣāḍhā,
        Śrāvaṇa,
        Śrāviṣṭhā,
        Śatabhiṣā,
        Pūrva_Bhādrapadā,
        Uttara_Bhādrapadā,
        Revati
    }

    public enum patcam
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
        Purnima
    }
}
