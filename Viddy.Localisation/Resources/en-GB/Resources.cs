//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// --------------------------------------------------------------------------------------------------
// <auto-generatedInfo>
// 	This code was generated by ResW File Code Generator (http://reswcodegen.codeplex.com)
// 	ResW File Code Generator was written by Christian Resma Helle
// 	and is under GNU General Public License version 2 (GPLv2)
// 
// 	This code contains a helper class exposing property representations
// 	of the string resources defined in the specified .ResW file
// 
// 	Generated: 02/27/2015 22:12:04
// </auto-generatedInfo>
// --------------------------------------------------------------------------------------------------
namespace Viddy.Localisation
{
    using Windows.ApplicationModel.Resources;
    
    
    public partial class Resources
    {
        
        private static ResourceLoader resourceLoader;
        
        static Resources()
        {
            string executingAssemblyName;
            executingAssemblyName = Windows.UI.Xaml.Application.Current.GetType().AssemblyQualifiedName;
            string[] executingAssemblySplit;
            executingAssemblySplit = executingAssemblyName.Split(',');
            executingAssemblyName = executingAssemblySplit[1];
            string currentAssemblyName;
            currentAssemblyName = typeof(Resources).AssemblyQualifiedName;
            string[] currentAssemblySplit;
            currentAssemblySplit = currentAssemblyName.Split(',');
            currentAssemblyName = currentAssemblySplit[1];
            if (executingAssemblyName.Equals(currentAssemblyName))
            {
                resourceLoader = ResourceLoader.GetForCurrentView("Resources");
            }
            else
            {
                resourceLoader = ResourceLoader.GetForCurrentView(currentAssemblyName + "/Resources");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "account"
        /// </summary>
        public static string AppBarAccount
        {
            get
            {
                return resourceLoader.GetString("AppBarAccount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "anonymous account"
        /// </summary>
        public static string AppBarAnonymousAccount
        {
            get
            {
                return resourceLoader.GetString("AppBarAnonymousAccount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "pin"
        /// </summary>
        public static string AppBarPin
        {
            get
            {
                return resourceLoader.GetString("AppBarPin");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "pin to start"
        /// </summary>
        public static string AppBarPinLong
        {
            get
            {
                return resourceLoader.GetString("AppBarPinLong");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "unpin"
        /// </summary>
        public static string AppBarUnpin
        {
            get
            {
                return resourceLoader.GetString("AppBarUnpin");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "unpin from start"
        /// </summary>
        public static string AppBarUnpinLong
        {
            get
            {
                return resourceLoader.GetString("AppBarUnpinLong");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Viddy"
        /// </summary>
        public static string AppName
        {
            get
            {
                return resourceLoader.GetString("AppName");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Anonymous"
        /// </summary>
        public static string Anonymous
        {
            get
            {
                return resourceLoader.GetString("Anonymous");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "add"
        /// </summary>
        public static string AppBarAdd
        {
            get
            {
                return resourceLoader.GetString("AppBarAdd");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "edit profile"
        /// </summary>
        public static string AppBarEditProfile
        {
            get
            {
                return resourceLoader.GetString("AppBarEditProfile");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "manage account"
        /// </summary>
        public static string AppBarManageAccount
        {
            get
            {
                return resourceLoader.GetString("AppBarManageAccount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "sign in"
        /// </summary>
        public static string AppBarSignIn
        {
            get
            {
                return resourceLoader.GetString("AppBarSignIn");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0} Details"
        /// </summary>
        public static string AppDetails
        {
            get
            {
                return resourceLoader.GetString("AppDetails");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Client ID"
        /// </summary>
        public static string ClientId
        {
            get
            {
                return resourceLoader.GetString("ClientId");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Client Secret"
        /// </summary>
        public static string ClientSecret
        {
            get
            {
                return resourceLoader.GetString("ClientSecret");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0}d ago"
        /// </summary>
        public static string DaysAgo
        {
            get
            {
                return resourceLoader.GetString("DaysAgo");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "No comments have been left for this video, why not leave one?"
        /// </summary>
        public static string EmptyComments
        {
            get
            {
                return resourceLoader.GetString("EmptyComments");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "No comments have been left for this video, why not sign in and leave one?"
        /// </summary>
        public static string EmptyCommentsAnon
        {
            get
            {
                return resourceLoader.GetString("EmptyCommentsAnon");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "There was an error when adding your app"
        /// </summary>
        public static string ErrorAddAppGeneric
        {
            get
            {
                return resourceLoader.GetString("ErrorAddAppGeneric");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Error adding comment"
        /// </summary>
        public static string ErrorAddingComment
        {
            get
            {
                return resourceLoader.GetString("ErrorAddingComment");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Error deleting comment"
        /// </summary>
        public static string ErrorDeletingComment
        {
            get
            {
                return resourceLoader.GetString("ErrorDeletingComment");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Image too small"
        /// </summary>
        public static string ErrorImageTooSmall
        {
            get
            {
                return resourceLoader.GetString("ErrorImageTooSmall");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "There was an error signing in"
        /// </summary>
        public static string ErrorSigninGeneric
        {
            get
            {
                return resourceLoader.GetString("ErrorSigninGeneric");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Username and/or password is incorrect"
        /// </summary>
        public static string ErrorSigninUsernamePassword
        {
            get
            {
                return resourceLoader.GetString("ErrorSigninUsernamePassword");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "An error ocurred signing you up."
        /// </summary>
        public static string ErrorSignupGeneric
        {
            get
            {
                return resourceLoader.GetString("ErrorSignupGeneric");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "This username is already in use"
        /// </summary>
        public static string ErrorSignupUsernameExists
        {
            get
            {
                return resourceLoader.GetString("ErrorSignupUsernameExists");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "You are still uploading a video, leaving now will disrupt this. You wouldn't want to disrupt it."
        /// </summary>
        public static string ErrorStillUploadingBack
        {
            get
            {
                return resourceLoader.GetString("ErrorStillUploadingBack");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "You are still uploading a video, sharing now will disrupt this. You wouldn't want to disrupt it."
        /// </summary>
        public static string ErrorStillUploadingShare
        {
            get
            {
                return resourceLoader.GetString("ErrorStillUploadingShare");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Careful now!"
        /// </summary>
        public static string ErrorTitle
        {
            get
            {
                return resourceLoader.GetString("ErrorTitle");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "An error ocurred updating your profile"
        /// </summary>
        public static string ErrorUpdatingProfile
        {
            get
            {
                return resourceLoader.GetString("ErrorUpdatingProfile");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "follow"
        /// </summary>
        public static string Follow
        {
            get
            {
                return resourceLoader.GetString("Follow");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "following"
        /// </summary>
        public static string Following
        {
            get
            {
                return resourceLoader.GetString("Following");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0}h ago"
        /// </summary>
        public static string HoursAgo
        {
            get
            {
                return resourceLoader.GetString("HoursAgo");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "like"
        /// </summary>
        public static string LabelLike
        {
            get
            {
                return resourceLoader.GetString("LabelLike");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "my anonymous videos"
        /// </summary>
        public static string LabelMyAnonymousVideos
        {
            get
            {
                return resourceLoader.GetString("LabelMyAnonymousVideos");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "my videos"
        /// </summary>
        public static string LabelMyVideos
        {
            get
            {
                return resourceLoader.GetString("LabelMyVideos");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "sign in"
        /// </summary>
        public static string LabelSignIn
        {
            get
            {
                return resourceLoader.GetString("LabelSignIn");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "sign out"
        /// </summary>
        public static string LabelSignOut
        {
            get
            {
                return resourceLoader.GetString("LabelSignOut");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "unlike"
        /// </summary>
        public static string LabelUnlike
        {
            get
            {
                return resourceLoader.GetString("LabelUnlike");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Add location?"
        /// </summary>
        public static string LocationAddLocation
        {
            get
            {
                return resourceLoader.GetString("LocationAddLocation");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Failed to find you"
        /// </summary>
        public static string LocationFailedToFindYou
        {
            get
            {
                return resourceLoader.GetString("LocationFailedToFindYou");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Finding you..."
        /// </summary>
        public static string LocationFindingYou
        {
            get
            {
                return resourceLoader.GetString("LocationFindingYou");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Nothing nearby"
        /// </summary>
        public static string LocationNothingNearby
        {
            get
            {
                return resourceLoader.GetString("LocationNothingNearby");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Turn location on"
        /// </summary>
        public static string LocationTurnLocationOn
        {
            get
            {
                return resourceLoader.GetString("LocationTurnLocationOn");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Are you sure?"
        /// </summary>
        public static string MessageAreYouSureTitle
        {
            get
            {
                return resourceLoader.GetString("MessageAreYouSureTitle");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Changes saved"
        /// </summary>
        public static string MessageChangesSaved
        {
            get
            {
                return resourceLoader.GetString("MessageChangesSaved");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0}m ago"
        /// </summary>
        public static string MinutesAgo
        {
            get
            {
                return resourceLoader.GetString("MinutesAgo");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Name"
        /// </summary>
        public static string NameText
        {
            get
            {
                return resourceLoader.GetString("NameText");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "no"
        /// </summary>
        public static string NoText
        {
            get
            {
                return resourceLoader.GetString("NoText");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Ok"
        /// </summary>
        public static string Ok
        {
            get
            {
                return resourceLoader.GetString("Ok");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "1 day"
        /// </summary>
        public static string OneDay
        {
            get
            {
                return resourceLoader.GetString("OneDay");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "1 follower"
        /// </summary>
        public static string OneFollower
        {
            get
            {
                return resourceLoader.GetString("OneFollower");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "1 hour"
        /// </summary>
        public static string OneHour
        {
            get
            {
                return resourceLoader.GetString("OneHour");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "This is the token for this app, are you sure you wish to revoke this token? If you do, you will have to sign in again."
        /// </summary>
        public static string RevokeViddyTokenBody
        {
            get
            {
                return resourceLoader.GetString("RevokeViddyTokenBody");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0}s ago"
        /// </summary>
        public static string SecondsAgo
        {
            get
            {
                return resourceLoader.GetString("SecondsAgo");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Check out this video by {0}"
        /// </summary>
        public static string ShareVideoMessage
        {
            get
            {
                return resourceLoader.GetString("ShareVideoMessage");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Check out this video"
        /// </summary>
        public static string ShareVideoTitle
        {
            get
            {
                return resourceLoader.GetString("ShareVideoTitle");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "6 hours"
        /// </summary>
        public static string SixHours
        {
            get
            {
                return resourceLoader.GetString("SixHours");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Viddy is an app that lets you browse and upload videos to VidMe\n\n{0}"
        /// </summary>
        public static string TellAFriendBody
        {
            get
            {
                return resourceLoader.GetString("TellAFriendBody");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Check out Viddy"
        /// </summary>
        public static string TellAFriendTitle
        {
            get
            {
                return resourceLoader.GetString("TellAFriendTitle");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "30 minutes"
        /// </summary>
        public static string ThirtyMinutes
        {
            get
            {
                return resourceLoader.GetString("ThirtyMinutes");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "this is you"
        /// </summary>
        public static string ThisIsYou
        {
            get
            {
                return resourceLoader.GetString("ThisIsYou");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "12 hours"
        /// </summary>
        public static string TwelveHours
        {
            get
            {
                return resourceLoader.GetString("TwelveHours");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Unknown user"
        /// </summary>
        public static string UnknownUser
        {
            get
            {
                return resourceLoader.GetString("UnknownUser");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0:N0} followers"
        /// </summary>
        public static string UserFollowers
        {
            get
            {
                return resourceLoader.GetString("UserFollowers");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0:N0} plays"
        /// </summary>
        public static string UserPlays
        {
            get
            {
                return resourceLoader.GetString("UserPlays");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0:N0} videos"
        /// </summary>
        public static string UserVideoCount
        {
            get
            {
                return resourceLoader.GetString("UserVideoCount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "On a Windows Phone? View the video in Viddy by clicking here: {0}"
        /// </summary>
        public static string ViddyWindowsPhone
        {
            get
            {
                return resourceLoader.GetString("ViddyWindowsPhone");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "{0}w ago"
        /// </summary>
        public static string WeeksAgo
        {
            get
            {
                return resourceLoader.GetString("WeeksAgo");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "yes"
        /// </summary>
        public static string YesText
        {
            get
            {
                return resourceLoader.GetString("YesText");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "0 followers"
        /// </summary>
        public static string ZeroFollowers
        {
            get
            {
                return resourceLoader.GetString("ZeroFollowers");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Channel subscription"
        /// </summary>
        public static string NotificationChannelSubscription
        {
            get
            {
                return resourceLoader.GetString("NotificationChannelSubscription");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Comment reply"
        /// </summary>
        public static string NotificationCommentReply
        {
            get
            {
                return resourceLoader.GetString("NotificationCommentReply");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "User subscription"
        /// </summary>
        public static string NotificationUserSubscription
        {
            get
            {
                return resourceLoader.GetString("NotificationUserSubscription");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Video comment"
        /// </summary>
        public static string NotificationVideoComment
        {
            get
            {
                return resourceLoader.GetString("NotificationVideoComment");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Video up vote"
        /// </summary>
        public static string NotificationVideoUpVote
        {
            get
            {
                return resourceLoader.GetString("NotificationVideoUpVote");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Anonymous account"
        /// </summary>
        public static string AnonymousAccount
        {
            get
            {
                return resourceLoader.GetString("AnonymousAccount");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Check out my video"
        /// </summary>
        public static string LabelCheckOutMyVideo
        {
            get
            {
                return resourceLoader.GetString("LabelCheckOutMyVideo");
            }
        }
        
        /// <summary>
        /// Localized resource similar to "Viddy can only accept one file at a time"
        /// </summary>
        public static string ErrorOneFileAtATime
        {
            get
            {
                return resourceLoader.GetString("ErrorOneFileAtATime");
            }
        }
    }
}
