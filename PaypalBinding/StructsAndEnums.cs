using System;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

namespace Paypal
{
    public enum PayPalPaymentIntent
    {
        Sale = 0,
        Authorize = 1
    }

    public enum PayPalPaymentViewControllerState
    {
        Unsent = 0,
        InProgress = 1
    }
           
}

