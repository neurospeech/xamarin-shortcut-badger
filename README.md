# Xamarin Shortcut Badges

Xamarin Shortcut Badges

Many thanks to https://github.com/leolin310148/ShortcutBadger

This code is published under MIT license, but you must use this library in accordance with license mentioned at https://github.com/leolin310148/ShortcutBadger

        /// <summary>
        /// Creates a badge
        /// </summary>
        /// <param name="context">Application context</param>
        /// <param name="count">badge count</param>
        public static void ApplyCount(Context context, int count) {
            ME.Leolin.Shortcutbadger.ShortcutBadger.ApplyCount(context, count);
        }

        /// <summary>
        /// Removes badge
        /// </summary>
        /// <param name="context"></param>
        public static void Remove(Context context) {
            ME.Leolin.Shortcutbadger.ShortcutBadger.RemoveCount(context);
        }

