
using UnityEngine;

[System.Serializable]
public class TwitchScope
{
    [Header("New Twitch API")]
    public bool analyticsReadExtensions; //View analytics data for your extensions.
    public bool analyticsReadGames; // View analytics data for your games.
    public bool bitsRead; // View Bits information for your channel.
    public bool channelReadSubscriptions; // Get a list of all subscribers to your channel and check if a user is subscribed to your channel
    public bool clipsEdit; // Manage a clip object.
    public bool userEdit; // Manage a user object.
    public bool userEditBroadcast; // Edit your channel’s broadcast configuration, including extension configuration. (This scope implies user:read:broadcast capability.)
    public bool userReadBroadcast; // View your broadcasting configuration, including extension configurations.
    public bool userReadEmail; // Read authorized user’s email address.

    [Header("Chat and PubSub")]
    public bool channelModerate; // Perform moderation actions in a channel.The user requesting the scope must be a moderator in the channel.
    public bool chatEdit; // Send live stream chat and rooms messages.
    public bool chatRead; // View live stream chat and rooms messages.
    public bool whispersRead; // View your whisper messages.
    public bool whispersEdit; // Send whisper messages.


    [Header("Twitch API v5")]
    // v5 scopes
    public bool channel_subscriptions; // Read all subscribers to your channel.

    public string ToQueryString()
    {
        string s = "";
        int scopeCount = 0;

        // New Twitch API
        if (analyticsReadExtensions) {
            if (scopeCount > 0) s += "+";
            s += "analytics:read:extensions";
            scopeCount++;
            }

        if (analyticsReadGames)
        {
            if (scopeCount > 0) s += "+";
            s += "analytics:read:games";
            scopeCount++;
        }

        if (bitsRead)
        {
            if (scopeCount > 0) s += "+";
            s += "bits:read";
            scopeCount++;
        }

        if (channelReadSubscriptions)
        {
            if (scopeCount > 0) s += "+";
            s += "channel:read:subscriptions+channel_subscriptions";
            scopeCount++;
        }

        if (clipsEdit)
        {
            if (scopeCount > 0) s += "+";
            s += "clips:edit";
            scopeCount++;
        }

        if (userEdit)
        {
            if (scopeCount > 0) s += "+";
            s += "user:edit";
            scopeCount++;
        }

        if (userEditBroadcast)
        {
            if (scopeCount > 0) s += "+";
            s += "user:edit:broadcast";
            scopeCount++;
        }

        if (userReadBroadcast)
        {
            if (scopeCount > 0) s += "+";
            s += "user:read:broadcast";
            scopeCount++;
        }

        if (userReadEmail)
        {
            if (scopeCount > 0) s += "+";
            s += "user:read:email";
            scopeCount++;
        }


        // Chat and PubSub
        if (channelModerate)
        {
            if (scopeCount > 0) s += "+";
            s += "channel:moderate";
            scopeCount++;
        }
        if (chatEdit)
        {
            if (scopeCount > 0) s += "+";
            s += "chat:edit";
            scopeCount++;
        }
        if (chatRead)
        {
            if (scopeCount > 0) s += "+";
            s += "chat:read";
            scopeCount++;
        }
        if (whispersRead)
        {
            if (scopeCount > 0) s += "+";
            s += "whispers:read";
            scopeCount++;
        }
        if (whispersEdit)
        {
            if (scopeCount > 0) s += "+";
            s += "whispers:edit";
            scopeCount++;
        }

        // Twitch API v5
        if (channel_subscriptions)
        {
            if (scopeCount > 0) s += "+";
            s += "channel_subscriptions";
            scopeCount++;
        }

        return s;
    }
}