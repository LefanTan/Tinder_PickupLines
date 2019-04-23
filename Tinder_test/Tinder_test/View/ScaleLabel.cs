using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Tinder_test.View{

    public class ScaleLabel : Label{

        public static readonly BindableProperty FontSizeFactorProperty = BindableProperty.Create(
            "FontSizeFactor",typeof(double),typeof(ScaleLabel),
            defaultValue:1.0, propertyChanged:OnFontFactorChanged
            );

        public double FontSizeFactor {
            get { return (double)GetValue(FontSizeFactorProperty); }
            set { SetValue(FontSizeFactorProperty, value); }
        }

        private static void OnFontFactorChanged (BindableObject bindable, object oldValue, object newValue) {
            var control = (ScaleLabel)bindable;
            control.OnFontSizeChangeImp();
        }

        public static readonly BindableProperty NamedFontSizeProperty = BindableProperty.Create(
            "NamedFontSized", typeof(NamedSize), typeof(ScaleLabel),
            defaultValue: NamedSize.Default, propertyChanged:OnNamedFontSizeChanged
            );
        
        public NamedSize NamedFontSized {
            get { return (NamedSize)GetValue(NamedFontSizeProperty);  }
            set { SetValue(NamedFontSizeProperty, value); }
        }

        private static void OnNamedFontSizeChanged (BindableObject bindable, object oldValue, object newValue) {
            var control = (ScaleLabel)bindable;
            control.OnFontSizeChangeImp();
        }

        private void OnFontSizeChangeImp() {
            if (FontSizeFactor != 1)
                FontSize = (FontSizeFactor * Device.GetNamedSize(NamedFontSized, typeof(Label))) ;
        }

    }

}
