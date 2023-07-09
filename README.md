# KP - key-password keeper -

A key-password store integrated with the clipboard.

## NO WARRANTY

I don't claim that this is a safe or secure thing to use. It is almost definitely unsafe in multiple ways. But it might NOT be unsafe. I wouldn't trust me, even if I said I was certain.

KP is a commandline password keeper, that uses "securestring" to store passwords in an encrypted manner.

It is very easy to use. Inspired by: https://secretgeek.net/kv

Usage:

### Store a username/password against a KEY

    kp hotmail user@hotmail.com

Save the username, 'user@hotmail.com' under the key, 'hotmail'
...and you will be prompted for the password to be stored under that key.

    kp hotmail

shows the username associated with the key 'hotmail' (i.e. 'user@hotmail.com' from example above) and copies the password to the clipboard. **The password is never displayed on screen.** It *is* on your clipboard as plain text.

The key should be a simple, self-evident string that has meaning for you. It might be the name of a website, the name of a machine/context in which the credentials are used.

### List all keys

    kp

Running kp with no parameters will list all of the keys, and just the keys (no usernames or passwords)


    kp ho*

List all keys that match the pattern 'ho*'

If the first parameter contains a * or a ? then it is treated as a search string, and compared with the keys.


### Remove a key

To remove the key 'hotmail' (and its username/password) from your store:

    kp -r hotmail

