using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinShortcutBadger
{
    /// <summary>
    /// Sets shortcut badges...
    /// </summary>
    public class Badger
    {

        /// <summary>
        /// Creates a badge
        /// </summary>
        /// <param name="context">Application context</param>
        /// <param name="count">badge count</param>
        public static void ApplyCount(Context context, int count) {
            ME.Leolin.Shortcutbadger.ShortcutBadger.With(context).Count(count);
        }

        /// <summary>
        /// Removes badge
        /// </summary>
        /// <param name="context"></param>
        public static void Remove(Context context) {
            ME.Leolin.Shortcutbadger.ShortcutBadger.With(context).Remove();
        }
    }
}