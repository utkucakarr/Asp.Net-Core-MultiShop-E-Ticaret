namespace MultiShop.RapidApiWebUI.Models
{
    public class ECommerceViewModel
    {

        public class Rootobject
        {
            public string status { get; set; }
            public string request_id { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public Product[] products { get; set; }
            public object[] sponsored_products { get; set; }
            public object[] filters { get; set; }
        }

        public class Product
        {
            public string product_id { get; set; }
            public string product_title { get; set; }
            public string product_description { get; set; }
            public string[] product_photos { get; set; }
            public Product_Videos[] product_videos { get; set; }
            public Product_Attributes product_attributes { get; set; }
            public float product_rating { get; set; }
            public string product_page_url { get; set; }
            public int product_num_reviews { get; set; }
            public int product_num_offers { get; set; }
            public string[] typical_price_range { get; set; }
            public Current_Product_Variant_Properties current_product_variant_properties { get; set; }
            public Product_Variants product_variants { get; set; }
            public Reviews_Insights reviews_insights { get; set; }
            public Offer offer { get; set; }
        }

        public class Product_Attributes
        {
            public string TuşSayısı { get; set; }
            public string Hassasiyet { get; set; }
            public string BağlantıTürü { get; set; }
            public string MouseTipi { get; set; }
            public string Özellik { get; set; }
            public string Işıklı { get; set; }
            public string Görünüm { get; set; }
            public string Makrolu { get; set; }
            public string Elkullanımı { get; set; }
            public string Marka { get; set; }
            public string Renk { get; set; }
            public string Bluetooth { get; set; }
            public string Seyahatİçin { get; set; }
            public string TürYeni { get; set; }
            public string DüğmeSayısı { get; set; }
            public string Duyarlılık { get; set; }
            public string Miktar { get; set; }
            public string Programlanabilir { get; set; }
            public string Bağlantı { get; set; }
            public string Sensör { get; set; }
            public string Tipi { get; set; }
            public string BağlantıTipi { get; set; }
            public string DPI { get; set; }
            public string DPIAyarı { get; set; }
            public string FormFaktörü { get; set; }
            public string ÜrünBoyutu { get; set; }
            public string ÜrünAğırlığı { get; set; }
            public string RGB { get; set; }
            public string Aydınlatma { get; set; }
            public string İleriGeriTuşu { get; set; }
            public string AçmaKapatmaTuşu { get; set; }
            public string Garanti { get; set; }
            public string GarantiTipi { get; set; }
            public string Bağlantılar { get; set; }
            public string MouseHassasiyetiDpi { get; set; }
            public string Frekansbandıveişletimkanalları { get; set; }
            public string Hareketçözünürlüğü { get; set; }
            public string Dönenteker { get; set; }
            public string Hareketalgılamateknolojisi { get; set; }
            public string Tuşsayısı { get; set; }
            public string Araçarayüzü { get; set; }
            public string Ürünrengi { get; set; }
            public string Piltürü { get; set; }
            public string Desteklenenpilsayısı { get; set; }
            public string Desteklenendiğerçalışmasistemleri { get; set; }
            public string DesteklenenLinuxçalışmasistemleri { get; set; }
            public string DesteklenenMacçalışmasistemleri { get; set; }
            public string DesteklenenWindowsçalışmasistemleri { get; set; }
            public string Paketağırlığı { get; set; }
            public string Ambalajyüksekliği { get; set; }
            public string Ambalajderinliği { get; set; }
            public string Ambalajgenişliği { get; set; }
            public string Mastırkartonağırlığı { get; set; }
            public string Anakartonuzunluğu { get; set; }
            public string Mastırkartonuzunluğu { get; set; }
            public string Anakartongenişliği { get; set; }
            public string Uyumluişletimsistemi { get; set; }
            public string Donanımönkoşulları { get; set; }
            public string Kablouzunluğu { get; set; }
        }

        public class Current_Product_Variant_Properties
        {
            public string Renk { get; set; }
        }

        public class Product_Variants
        {
            public Renk[] Renk { get; set; }
        }

        public class Renk
        {
            public string name { get; set; }
            public string product_id { get; set; }
            public string thumbnail { get; set; }
        }

        public class Reviews_Insights
        {
        }

        public class Offer
        {
            public string offer_id { get; set; }
            public string offer_title { get; set; }
            public string offer_page_url { get; set; }
            public string price { get; set; }
            public string shipping { get; set; }
            public string offer_badge { get; set; }
            public bool on_sale { get; set; }
            public object original_price { get; set; }
            public string product_condition { get; set; }
            public string store_name { get; set; }
            public object store_rating { get; set; }
            public int store_review_count { get; set; }
            public string store_reviews_page_url { get; set; }
            public string store_favicon { get; set; }
            public object payment_methods { get; set; }
        }

        public class Product_Videos
        {
            public string title { get; set; }
            public string url { get; set; }
            public string source { get; set; }
            public string publisher { get; set; }
            public string thumbnail { get; set; }
            public string preview_url { get; set; }
            public int duration_ms { get; set; }
        }

    }
}
