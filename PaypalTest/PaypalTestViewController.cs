using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Paypal;
using System.Diagnostics;

namespace PaypalTest
{
    public partial class PaypalTestViewController : UIViewController
    {
        public PaypalTestViewController (IntPtr handle) : base(handle)
        {
   
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var config = new PayPalConfiguration() { 
                AcceptCreditCards = true, 
                LanguageOrLocale = "en",
                MerchantName = "Merchant",
                MerchantPrivacyPolicyURL = new NSUrl("https://www.paypal.com/webapps/mpp/ua/privacy-full"),
                MerchantUserAgreementURL = new NSUrl("https://www.paypal.com/webapps/mpp/ua/useragreement-full")
            };

            var item1 = new PayPalItem() {    
                Name = "DoofesDing", 
                Price = new NSDecimalNumber("10.00"),
                Currency = "EUR",
                Quantity = 1,
                Sku = "FOO-29376"
            };

            var item2 = new PayPalItem() {    
                Name = "DoofesDing2", 
                Price = new NSDecimalNumber("15.00"),
                Currency = "EUR",
                Quantity = 1,
                Sku = "FOO-23476"
            };

            var items = new PayPalItem[]{ item1, item2 };

            var payment = new PayPalPayment() { 
                Amount = new NSDecimalNumber("25.00"),
                CurrencyCode = "EUR",
                ShortDescription = "Stuffz",
                Items = items
            };

            var myDel = new PPDelegate();
                    
            var paypalVC = new PayPalPaymentViewController(payment, config, myDel);

            var payBtn = new UIButton(new RectangleF(10, 10, 200, 200));
            payBtn.SetTitle("Pay", UIControlState.Normal);
            payBtn.TouchUpInside += (object sender, EventArgs e) => {
                this.PresentViewController(paypalVC, true, null);
            };
            Add(payBtn);
        }
            
    }

    public class PPDelegate: PayPalPaymentDelegate
    {
        public override void DidCancelPayment(PayPalPaymentViewController paymentViewController)
        {
            Debug.WriteLine("Payment Cancelled");
        }

        public override void DidCompletePayment(PayPalPaymentViewController paymentViewController, PayPalPayment completedPayment)
        {
            Debug.WriteLine("Payment Completed");
        }
    }
}

