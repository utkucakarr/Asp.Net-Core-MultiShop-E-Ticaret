using MultiShop.Payment.Dtos.PaymentDtos;
using System.Text;
using System.Security.Cryptography;
using MultiShop.Payment.Repositories;
using MultiShop.Payment.Entities;
using System.Xml.Linq;

namespace MultiShop.Payment.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<PaymentInfo> _repository;

        public PaymentService(IRepository<PaymentInfo> repository)
        {
            _repository = repository;
        }

        public async Task<CreatePaymentResponseDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var orderId = "MLT-SHP-" + createPaymentDto.OrderingId.ToString(); //Guid.NewGuid().ToString("N");
            var paymentAmounth = ulong.Parse(createPaymentDto.PaymentAmounth) * 100;
            //Güvenlik için hash oluşturuuyor. 949 Para kodu, 123qweASK/ şifre ve 30691297'de Terminal Id
            var hashData = GetHashData("123qweASD/", "30691297", orderId, createPaymentDto.CardNumber, paymentAmounth, 949);
            var xmlData = $"<?xml version='1.0' encoding='iso-8859-9'?>\n" +
                      $"<GVPSRequest>\n" +
                      $"    <Mode>TEST</Mode>\n" +
                      $"    <Version>512</Version>\n" +
                      $"    <Terminal>\n" +
                      $"        <ProvUserID>PROVAUT</ProvUserID>\n" +
                      $"        <HashData>{hashData}</HashData>\n" +
                      $"        <UserID>PROVAUT</UserID>\n" +
                      $"        <ID>30691297</ID>\n" +
                      $"        <MerchantID>7000679</MerchantID>\n" +
                      $"    </Terminal>\n" +
                      $"    <Customer>\n" +
                      $"        <IPAddress>192.168.0.1</IPAddress>\n" +
                      $"        <EmailAddress>eticaret@garanti.com.tr</EmailAddress>\n" +
                      $"    </Customer>\n" +
                      $"    <Card>\n" +
                      $"        <Number>{createPaymentDto.CardNumber}</Number>\n" +
                      $"        <ExpireDate>{createPaymentDto.Month+createPaymentDto.Year.Substring(createPaymentDto.Year.Length-2)}</ExpireDate>\n" +
                      $"        <CVV2>{createPaymentDto.Cvv}</CVV2>\n" +
                      $"    </Card>\n" +
                      $"    <Order>\n" +
                      $"        <OrderID>{orderId}</OrderID>\n" +
                      $"        <GroupID />\n" +
                      $"    </Order>\n" +
                      $"    <Transaction>\n" +
                      $"        <Type>sales</Type>\n" +
                      $"        <Amount>{paymentAmounth}</Amount>\n" +
                      $"        <CurrencyCode>949</CurrencyCode>\n" +
                      $"        <CardholderPresentCode>0</CardholderPresentCode>\n" +
                      $"        <MotoInd>N</MotoInd>\n" +
                      $"    </Transaction>\n" +
                      $"</GVPSRequest>";

            var client = new HttpClient();

            var requestContent = new StringContent(xmlData, Encoding.GetEncoding("iso-8859-9"), "application/xml");

            var response = await client.PostAsync("https://sanalposprovtest.garantibbva.com.tr/VPServlet", requestContent);

            string responseString = await response.Content.ReadAsStringAsync();

            var xml = XDocument.Parse(responseString); // Gelen xml'e dönüştürüyor.
            var code = xml.Descendants("Code").FirstOrDefault()?.Value;
            var reasonCode = xml.Descendants("ReasonCode").FirstOrDefault()?.Value;
            var message = xml.Descendants("Message").FirstOrDefault()?.Value;
            var errorMessage = xml.Descendants("ErrorMsg").FirstOrDefault()?.Value;

            var isSuccess = code == "00" && reasonCode == "00" && message == "Approved";

            await _repository.CreateAsync(new PaymentInfo
            {
                UserId = createPaymentDto.UserId,
                OrderingId = createPaymentDto.OrderingId,
                BankResponse = responseString,
                IsSuccess = isSuccess,
                CreatedDate = DateTime.Now,
                CardNumber = createPaymentDto.CardNumber,
                Amount = ulong.Parse(createPaymentDto.PaymentAmounth)
            });

            var paymentResponse = new CreatePaymentResponseDto
            {
                ErrorMessage = errorMessage,
                IsSuccess = isSuccess
            };

            return paymentResponse;
        }

        //Eski sistemlerde kullanıyor daha az güvenli açık var
        public static string Sha1(string text)
        {
            var provider = CodePagesEncodingProvider.Instance; // Iso "ISO-8859-9" kodunu getiriyor
            Encoding.RegisterProvider(provider); //Türkçe karaktere çevirmek için iso kodunu

            var cryptoServiceProvider = new SHA1CryptoServiceProvider(); //cryptoservice'e sha1 şifreleme işlemini uyguluyoruz.
            var inputbytes = cryptoServiceProvider.ComputeHash(Encoding.GetEncoding("ISO-8859-9").GetBytes(text)); // hash ile şifreliyoruz.

            var builder = new StringBuilder();
            for (int i = 0; i < inputbytes.Length; i++)
            {
                builder.Append(string.Format("{0,2:x}", inputbytes[i]).Replace(" ", "0")); //Bu satırda her bir byte hexadecimal (onaltılık) forma çevrilir, tek basamaklı olanlara başına 0 konur.
            }

            return builder.ToString().ToUpper(); //Sonuç olarak elde edilen değer büyük harflerle döndürülür:
        }

        //Yeni sistemlerde kullanılıyor
        public static string Sha512(string text)
        {
            var provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            var cryptoServiceProvider = new SHA512CryptoServiceProvider();
            var inputbytes = cryptoServiceProvider.ComputeHash(Encoding.GetEncoding("ISO-8859-9").GetBytes(text));

            var builder = new StringBuilder();
            for (int i = 0; i < inputbytes.Length; i++)
            {
                builder.Append(string.Format("{0,2:x}", inputbytes[i]).Replace(" ", "0"));
            }

            return builder.ToString().ToUpper();
        }

        public static string GetHashData(string userPassword, string terminalId, string orderId, string cardNumber, ulong amount, int currencyCode)
        {
            var hashedPassword = Sha1(userPassword + "0" + terminalId);
            return Sha512(orderId + terminalId + cardNumber + amount + currencyCode + hashedPassword).ToUpper();
        }
    }
}
