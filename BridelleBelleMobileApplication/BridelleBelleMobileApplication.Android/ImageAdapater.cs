using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Support.V4.View;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BridelleBelleMobileApplication.Droid
{
    public class ImageAdapater:PagerAdapter
    {
        private Context context;
        private int[] imageList =
        {
            Resource.Drawable.AH_WP,
            Resource.Drawable.FunHaus_WP
        };

        public ImageAdapater (Context context)
        {
            this.context = context;
        }

        public override int Count
        {
            get
            {
                return imageList.Length;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == ((ImageView)objectValue);
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            ImageView imageView = new ImageView(context);
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            ((ViewPager)container).AddView(imageView, 0);
            return imageView;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object objectValue)
        {
            ((ViewPager)container).RemoveView((ImageView)objectValue);
        }
    }
}