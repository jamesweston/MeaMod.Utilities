
using System;

namespace MeaMod.Utilities
{
    /// <summary>
    /// Description attribute extension for TimeSpan
    /// <para><see href="https://stackoverflow.com/a/4423615"/></para>
	/// <para>Licence: CC BY-SA 3.0</para>
    /// </summary>
    /// 
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableAgeString(this TimeSpan span)
        {
            return string.Format("{0:0}", span.Days / 365.25);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableShortString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0}:", span.Days) : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0}:",span.Hours)  : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0}:", span.Minutes) : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0}", span.Seconds) : string.Empty);

            if (formatted.EndsWith(":")) formatted = formatted.Substring(0, formatted.Length - 1);

            if (string.IsNullOrEmpty(formatted)) formatted = "00:00";

            return formatted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableShortStringEx(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0}:", span.Days) : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0}:", span.Hours) : string.Empty,
                string.Format("{0:0}:", span.Minutes),
                string.Format("{0:0}", span.Seconds));

            if (formatted.EndsWith(":")) formatted = formatted.Substring(0, formatted.Length - 1);

            if (string.IsNullOrEmpty(formatted)) formatted = "00:00";

            return formatted;
        }

        /// <summary>
        /// <para><see href="https://gist.github.com/Rychu-Pawel/fefb89e21b764e97e4993ff517ff0129"/></para>
        /// </summary>
        /// <param name="span"></param>
        /// <param name="justNowSecondsThreshold"></param>
        /// <returns></returns>
        public static string ToReadableAgoString(this TimeSpan span, double justNowSecondsThreshold)
        {
            if (span.TotalSeconds < justNowSecondsThreshold)
                return "just now";

            return span switch
            {
                TimeSpan { TotalDays: > 1 } => string.Format("{0:0} day{1} ago", span.Days, span.Days == 1 ? string.Empty : "s"),
                TimeSpan { TotalHours: > 1 } => string.Format("{0:0} hour{1} ago", span.Hours, span.Hours == 1 ? string.Empty : "s"),
                TimeSpan { TotalMinutes: > 1 } => string.Format("{0:0} minute{1} ago", span.Minutes, span.Minutes == 1 ? string.Empty : "s"),
                _ => string.Format("{0:0} second{1} ago", span.Seconds, span.Seconds == 1 ? string.Empty : "s")
            };
        }
    }
}
