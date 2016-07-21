using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System.Threading.Tasks;

namespace Xamarin.Android.ShortcutBadger
{
    public abstract class ApplicationBadge
    {


        protected Context context;

        protected string PackageName {
            get {
                return context.ApplicationInfo.PackageName;
            }
        }

        protected string LauncherActivity {
            get {

                Intent i = new Intent(Intent.ActionMain);
                i.AddCategory(Intent.CategoryLauncher);

                var a = context.PackageManager.QueryIntentActivities(i, global::Android.Content.PM.PackageInfoFlags.Activities);

                string pn = PackageName;

                return a.FirstOrDefault(x=>x.ActivityInfo.PackageName == pn).ActivityInfo.Name;
            }
        }


        public static ApplicationBadge From(Context context) {
            // detect device launcher type...

            if (Build.Manufacturer.ToLower().Contains("samsung")) {
                return new SamsungApplicationBadge(context);
            }

            if (Build.Manufacturer.ToLower().Contains("sony")) {
                return new SonyApplicationBadge(context);
            }

            return new NotFoundApplicationBadge();
        }


        protected virtual string BadgeField => "badge_count";

        protected virtual string PackageField => "package_name";

        protected virtual string ActivityField => "activity_name";

        protected virtual string ContentUri => "content://com.sonymobile.home.resourceprovider/badge";

        public virtual Task SetCountAsync(int count)
        {



            var a = new AsyncQH(context.ContentResolver);

            ContentValues cv = new ContentValues();
            cv.Put(BadgeField, count);
            cv.Put(PackageField, PackageName);
            cv.Put(ActivityField, LauncherActivity);

            var uri = global::Android.Net.Uri.Parse(ContentUri);

            a.StartInsert(0, null, uri, cv);

            return a.Task;

        }

        public virtual Task ClearCountAsync() {
            return SetCountAsync(0);
        }

    }

    internal class NotFoundApplicationBadge : ApplicationBadge
    {
        public override Task SetCountAsync(int count)
        {

            System.Diagnostics.Debug.Fail("Badge-Error", "No suitable badger found...");

            return Task.FromResult(0);
        }

    }

    internal class SamsungApplicationBadge : ApplicationBadge
    {
        public SamsungApplicationBadge(Context context)
        {
            this.context = context;
        }

        protected override string ActivityField => "class";

        protected override string PackageField => "package";

        protected override string BadgeField => "badgecount";

        protected override string ContentUri => "content://com.sec.badge/apps";
    }

    internal class SonyApplicationBadge : ApplicationBadge
    {

        public SonyApplicationBadge(Context context)
        {
            this.context = context;
        }

    }

    public class AsyncQH : AsyncQueryHandler {

        System.Threading.Tasks.TaskCompletionSource<object> Source;

        public Task Task => Source.Task;


        public AsyncQH(ContentResolver c):base(c)
        {
            Source = new TaskCompletionSource<object>();
        }

        protected override void OnInsertComplete(int token, Java.Lang.Object cookie, global::Android.Net.Uri uri)
        {
            if (uri != null)
            {
                Source.TrySetResult(null);
            }
            else {
                Source.TrySetException(new System.Exception("Insert failed"));
            }


        }

    }


}