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

        PPDelegate myDelegate;
        PayPalPaymentViewController paypalVC;

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

            myDelegate = new PPDelegate();
                    
            paypalVC = new PayPalPaymentViewController(payment, config, myDelegate);

            var payBtn = new UIButton(new RectangleF(60, 100, 200, 60));
            payBtn.SetTitle("Pay", UIControlState.Normal);
            payBtn.BackgroundColor = UIColor.Blue;
            payBtn.TouchUpInside += (object sender, EventArgs e) => {
                this.PresentViewController(paypalVC, true, null);
            };
            Add(payBtn);
        }

        protected class PPDelegate: PayPalPaymentDelegate
        {
            UIViewController parent;

            public PPDelegate (UIViewController myParent)
            {
                parent = myParent;
            }

            public override void DidCancelPayment(PayPalPaymentViewController paymentViewController)
            {
                Debug.WriteLine("Payment Cancelled");
                parent.DismissViewController(true, null);
            }

            public override void DidCompletePayment(PayPalPaymentViewController paymentViewController, PayPalPayment completedPayment)
            {
                Debug.WriteLine("Payment Completed");
                parent.DismissViewController(true, null);
            }
        }
            
    }

   
}

