# KP - key-password keeper -

A key-password store integrated with the clipboard.

Windows only. (PR's welcome)

## NO WARRANTY

I don't claim that this is a safe or secure thing to use.

It is almost definitely unsafe in multiple ways. But it might NOT be unsafe. I wouldn't trust me to write security focused software, even if I said I was certain. I lack the expertise to be able to audit the code myself, and I bet those clever security boffins would be able to drive a comet through the gaps in my code. For example, after unencryptring the password it places it on the clipboard, and that probably exposes it to just about any process on your machine.

KP is a commandline "password keeper", that uses Windows "securestring" to store passwords in an encrypted manner.

It is very easy to use. Inspired by: https://secretgeek.net/kv


## Usage

Here is how to use `kp` for a number of tasks.

### Store a username/password against a KEY


    kp hotmail user@hotmail.com


Here we have saved the username, 'user@hotmail.com' under the key, 'hotmail'
...and you will be prompted for the password to be stored under that key.


### Collect password and username against a Key

    kp hotmail

This command shows the username associated with the key 'hotmail' (i.e. 'user@hotmail.com' from example above) and copies the password to the clipboard. **The password is never displayed on screen.** It *is* on your clipboard as plain text.

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

<div class="css-1h3rtzg css-f7ay7b css-5p9vjy css-zjik7"><div class="css-1h3rtzg css-f7ay7b css-ln7vnr css-zjik7"><div class="css-1l1io5o" style="width: 30px; height: 30px; border-width: 3px;"></div></div><div class="css-joi6zj" data-testid="editor-canvas-background" style="background: rgb(255, 255, 255); box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 0px 0.5px inset;"></div><div class="css-1vs2kf0" data-testid="editor-canvas-artboard"><div style="transform: scale(1);"><svg width="220.83737978468977" height="304.915625" viewBox="0 0 100 138.0724700217346" class="css-1j8o68f"><defs id="SvgjsDefs1811"></defs><g id="SvgjsG1812" featurekey="41S5F6-0" transform="matrix(0.4,0,0,0.4,30,0)" fill="#a558c8"><path xmlns="http://www.w3.org/2000/svg" style="" d="M68.75,12.5c10.34,0,18.75,8.41,18.75,18.75S79.09,50,68.75,50c-0.977,0-2.051-0.122-3.394-0.366  l-6.47-1.196l-4.65,4.65l-0.574,0.574L50,57.324V62.5H37.5V75H25v12.5H12.5v-7.324l34.412-34.412l4.65-4.65l-1.196-6.47  C50.116,33.301,50,32.227,50,31.25C50,20.91,58.41,12.5,68.75,12.5 M68.75,0C51.489,0,37.5,13.989,37.5,31.25  c0,1.953,0.232,3.833,0.574,5.676L0,75v25h37.5V87.5H50V75h12.5V62.5l0.574-0.574c1.843,0.342,3.723,0.574,5.676,0.574  C86.011,62.5,100,48.511,100,31.25S86.011,0,68.75,0L68.75,0z" fill="#a558c8"></path><path xmlns="http://www.w3.org/2000/svg" style="" d="M75.024,31.25c0,3.455-2.796,6.25-6.25,6.25s-6.25-2.795-6.25-6.25S65.32,25,68.774,25  S75.024,27.795,75.024,31.25z" fill="#a558c8"></path></g><g id="SvgjsG1813" featurekey="RGwKHf-0" transform="matrix(4.664178573360321,0,0,4.664178573360321,-7.74253405204072,30.895526769777355)" fill="#891e57"><path d="M11.96 19.32 q-0.56 0.84 -1.8 0.84 q-0.7 0 -1.05 -0.36 t-0.63 -1.22 l-0.92 -2.62 q-0.36 -1.04 -0.83 -1.47 t-1.27 -0.43 q-0.2 0 -1.12 0.04 l0 5.9 q-0.52 0.1 -1.34 0.1 t-1.34 -0.1 l0 -13.66 q0.52 -0.1 1.34 -0.1 t1.34 0.1 l0 5.5 l0.8 -0.02 q0.32 0 0.48 -0.13 t0.36 -0.49 l2.22 -3.96 q0.3 -0.54 0.6 -0.77 t0.84 -0.23 t1.62 0.08 l0.16 0.2 l-2.72 4.86 q-0.46 0.82 -0.98 1.24 q0.94 0.2 1.44 0.77 t0.88 1.59 l0.94 2.68 q0.14 0.36 0.17 0.43 t0.13 0.31 q0.28 0.56 0.44 0.7 t0.24 0.22 z M18.36 15.46 l-1.84 0 l0 4.54 q-0.56 0.1 -1.34 0.1 t-1.34 -0.1 l0 -13.56 l0.14 -0.12 q1.62 -0.04 2.71 -0.06 t1.67 -0.02 q2.16 0 3.45 1.18 t1.29 3.42 q0 1.12 -0.34 1.98 t-0.96 1.44 t-1.49 0.89 t-1.95 0.31 z M16.52 8.32 l0 5.1 q0.78 -0.02 1.33 -0.02 t0.94 -0.07 t0.66 -0.26 t0.49 -0.57 q0.2 -0.34 0.3 -0.77 t0.1 -0.87 q0 -0.46 -0.1 -0.91 t-0.34 -0.81 t-0.64 -0.58 t-1 -0.22 l-1.01 0 t-0.73 -0.02 z"></path></g><g id="SvgjsG1814" featurekey="UxezYZ-0" transform="matrix(0.21131151480250285,0,0,0.21131151480250285,9.809819515764328,133.78284562637222)" fill="#a558c8"><path d="M12.76 18.5 c-1.28 1.1 -2.9 1.7 -4.7 1.7 c-3.64 0 -7.16 -2.96 -7.16 -7.2 s3.52 -7.2 7.16 -7.2 c1.78 0 3.38 0.6 4.64 1.66 l-1.02 1.16 c-0.98 -0.78 -2.24 -1.24 -3.5 -1.24 c-2.86 0 -5.56 2.32 -5.56 5.62 s2.7 5.62 5.56 5.62 c1.28 0 2.56 -0.48 3.54 -1.28 z M31.708000000000006 5.800000000000001 c3.64 0 7.16 2.96 7.16 7.2 s-3.52 7.2 -7.16 7.2 c-3.66 0 -7.16 -2.96 -7.16 -7.2 s3.5 -7.2 7.16 -7.2 z M31.708000000000006 18.62 c2.74 0 5.44 -2.32 5.44 -5.62 s-2.7 -5.62 -5.44 -5.62 c-2.76 0 -5.44 2.32 -5.44 5.62 s2.68 5.62 5.44 5.62 z M60.916000000000004 6 l1.66 0 l0 14 l-1.28 0 l-8.48 -10.96 l0 10.96 l-1.66 0 l0 -14 l1.28 0 l8.48 10.98 l0 -10.98 z M79.464 5.76 c2.18 0 3.78 1.44 4.38 2.94 l-1.26 0.68 c-0.64 -1.28 -1.66 -2.12 -3.12 -2.12 c-1.54 0 -2.6 0.88 -2.6 2.1 c0 1.14 0.72 1.86 2.24 2.44 l1.02 0.38 c2.52 0.94 4.1 2.02 4.1 4.24 c0 2.4 -2.3 3.88 -4.68 3.88 s-4.34 -1.44 -4.88 -3.44 l1.34 -0.64 c0.48 1.48 1.74 2.56 3.54 2.56 c1.6 0 2.96 -0.92 2.96 -2.34 c0 -1.6 -1.12 -2.2 -2.78 -2.84 l-1.04 -0.4 c-2.12 -0.8 -3.48 -1.9 -3.48 -3.88 c0 -2.04 1.8 -3.56 4.26 -3.56 z M103.172 5.800000000000001 c3.64 0 7.16 2.96 7.16 7.2 s-3.52 7.2 -7.16 7.2 c-3.66 0 -7.16 -2.96 -7.16 -7.2 s3.5 -7.2 7.16 -7.2 z M103.172 18.62 c2.74 0 5.44 -2.32 5.44 -5.62 s-2.7 -5.62 -5.44 -5.62 c-2.76 0 -5.44 2.32 -5.44 5.62 s2.68 5.62 5.44 5.62 z M124.27999999999999 18.44 l6.28 0 l0 1.56 l-7.94 0 l0 -14 l1.66 0 l0 12.44 z M144.40800000000002 18.44 l6.66 0 l0 1.56 l-7.06 0 l-1.26 0 l0 -14 l1.66 0 l6.48 0 l0 1.56 l-6.48 0 l0 4.64 l5.04 0 l0 1.52 l-5.04 0 l0 4.72 z M184.10399999999998 6 c3.14 0 4.96 1.92 4.96 4.52 s-1.82 4.44 -4.96 4.44 l-2.9 0 l0 5.04 l-1.66 0 l0 -14 l4.56 0 z M184.064 13.48 c2.02 0 3.34 -1.04 3.34 -2.96 c0 -1.94 -1.32 -2.96 -3.34 -2.96 l-2.86 0 l0 5.92 l2.86 0 z M212.472 20 l-1.44 -3.3 l-7.48 0 l-1.44 3.3 l-1.76 0 l6.24 -14 l1.38 0 l6.26 14 l-1.76 0 z M204.172 15.3 l6.24 0 l-3.12 -7.12 z M230.32 5.76 c2.18 0 3.78 1.44 4.38 2.94 l-1.26 0.68 c-0.64 -1.28 -1.66 -2.12 -3.12 -2.12 c-1.54 0 -2.6 0.88 -2.6 2.1 c0 1.14 0.72 1.86 2.24 2.44 l1.02 0.38 c2.52 0.94 4.1 2.02 4.1 4.24 c0 2.4 -2.3 3.88 -4.68 3.88 s-4.34 -1.44 -4.88 -3.44 l1.34 -0.64 c0.48 1.48 1.74 2.56 3.54 2.56 c1.6 0 2.96 -0.92 2.96 -2.34 c0 -1.6 -1.12 -2.2 -2.78 -2.84 l-1.04 -0.4 c-2.12 -0.8 -3.48 -1.9 -3.48 -3.88 c0 -2.04 1.8 -3.56 4.26 -3.56 z M251.468 5.76 c2.18 0 3.78 1.44 4.38 2.94 l-1.26 0.68 c-0.64 -1.28 -1.66 -2.12 -3.12 -2.12 c-1.54 0 -2.6 0.88 -2.6 2.1 c0 1.14 0.72 1.86 2.24 2.44 l1.02 0.38 c2.52 0.94 4.1 2.02 4.1 4.24 c0 2.4 -2.3 3.88 -4.68 3.88 s-4.34 -1.44 -4.88 -3.44 l1.34 -0.64 c0.48 1.48 1.74 2.56 3.54 2.56 c1.6 0 2.96 -0.92 2.96 -2.34 c0 -1.6 -1.12 -2.2 -2.78 -2.84 l-1.04 -0.4 c-2.12 -0.8 -3.48 -1.9 -3.48 -3.88 c0 -2.04 1.8 -3.56 4.26 -3.56 z M286.436 6 l-4.52 14 l-1.08 0 l-3.86 -10.94 l-3.84 10.94 l-1.08 0 l-4.54 -14 l1.72 0 l3.42 10.58 l3.74 -10.58 l1.16 0 l3.76 10.58 l3.42 -10.58 l1.7 0 z M305.284 5.800000000000001 c3.64 0 7.16 2.96 7.16 7.2 s-3.52 7.2 -7.16 7.2 c-3.66 0 -7.16 -2.96 -7.16 -7.2 s3.5 -7.2 7.16 -7.2 z M305.284 18.62 c2.74 0 5.44 -2.32 5.44 -5.62 s-2.7 -5.62 -5.44 -5.62 c-2.76 0 -5.44 2.32 -5.44 5.62 s2.68 5.62 5.44 5.62 z M335.012 20 l-1.8 0 l-3.54 -5.04 l-0.38 0 l-2.9 0 l0 5.04 l-1.66 0 l0 -14 l4.56 0 c3.14 0 4.96 1.92 4.96 4.52 c0 2 -1.08 3.56 -3 4.16 z M326.392 7.5600000000000005 l0 5.92 l2.86 0 c2.02 0 3.34 -1.04 3.34 -2.96 c0 -1.94 -1.32 -2.96 -3.34 -2.96 l-2.86 0 z M351.15999999999997 6 c4.08 0 7.08 3.1 7.08 7 s-3 7 -7.08 7 l-4.16 0 l0 -14 l4.16 0 z M351.13999999999993 18.42 c3.34 0 5.4 -2.42 5.4 -5.42 s-2.06 -5.42 -5.4 -5.42 l-2.48 0 l0 10.84 l2.48 0 z M374.72799999999995 5.76 c2.18 0 3.78 1.44 4.38 2.94 l-1.26 0.68 c-0.64 -1.28 -1.66 -2.12 -3.12 -2.12 c-1.54 0 -2.6 0.88 -2.6 2.1 c0 1.14 0.72 1.86 2.24 2.44 l1.02 0.38 c2.52 0.94 4.1 2.02 4.1 4.24 c0 2.4 -2.3 3.88 -4.68 3.88 s-4.34 -1.44 -4.88 -3.44 l1.34 -0.64 c0.48 1.48 1.74 2.56 3.54 2.56 c1.6 0 2.96 -0.92 2.96 -2.34 c0 -1.6 -1.12 -2.2 -2.78 -2.84 l-1.04 -0.4 c-2.12 -0.8 -3.48 -1.9 -3.48 -3.88 c0 -2.04 1.8 -3.56 4.26 -3.56 z"></path></g></svg></div></div></div>

## See also


## See also

- [kv can remember it for you, wholesale](https://secretgeek.net/kv)
- [Stashy is a Really simple Key Value store](https://secretgeek.net/stashy_gist)
- [kv: Simple Console Key Value store](https://github.com/secretGeek/kv)
