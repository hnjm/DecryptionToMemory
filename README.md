# Decryption To Memory
A C# tool to decrypt information to the process memory only. A major part of my upcoming password manager

## Contents
- Information
    * [Purpose](#purpose)
    * [Testing Screenshots](#testing-screenshots)

- Setup
    * [Test Instructions](#test-instructions)
    * [Must Change Information](#must-change-information)

- Bugs/Unplanned Features
   * [Unicode VS. UTF8](#unicode-vs-utf8)
   * [Decryption Problems](#decryption-problems)
   
- Conclusions
   * [Thank you!](#thank-you)
    
# Information

## Purpose

I am currently in the process of building a fully open source password manager in C# as a personal project. I spent a little bit figuring out the decryption to memory and figured I would share the code incase someone else has the same issue/idea as me. The code is not the cleanest and can be fixed up in a few spots, but it is functional and works as expected in it's current state.

This project will take a text document and encrypt it with AES and a password of your choosing as the salt. After that, you can decrypt the item without actually impacting the physical file. This allows for the storage of sensitive data in a moderatly secure way as the raw data will not sit on the computer but is still accessable through hooking the process memory.

## Testing Screenshots

The data will will be working with for testing
<img src="https://i.imgur.com/GcLpZXP.png" height="386">

File after encryption (Will not change from this)
<img src="https://i.imgur.com/1zfcGYN.png" height="386">

Decrypted data available in the executable
<img src="https://i.imgur.com/66Cmjmp.png">

# Setup

## Test Instructions
To begin testing, head into the Test File directory and copy the bdlog.txt into the bin/debug directory.

Once that has been completed, open up the Decryption to memory.exe and encrypt the file with a password.

Once the encryption is complete (Should be instant) inspect the documents content in the text editor of your choice.

Finally, head back into the Decryption to memory.exe, enter the password and decrypt the file. You will see the plain text of the document inside of the executable BUT the file is never modified.

## Must Change Information

On line 78 and 108, you will see byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

The byte array will need to be changed to ensure the security of the program. You can set any numbers you like, I am unsure of the range myself, but try to keep the value under 100 (Not tested). The values will need to be kept the same or it will cause issues later on.

On line 27 you will see ff = "bdlog.txt";

This is the target file path, feel free to change this to whatever you like. I used the relative path to the executable to keep it simple for later modification

# Bugs/Unplanned Features

## Unicode VS. UTF8

Right now, this is built to only work with Unicode type files. If you wish to change this, head to line 159 where you will see richTextBox1.Text = System.Text.Encoding.Unicode.GetString(bytesDecrypted, 0, bytesDecrypted.Length);

Simply change Unicode to the type you wish to use and try to decrypt the file again.

## Decryption Problems

I have noticed that sometimes if a file is too small or too large, it will not properly decrypt. I am unsure of why this happens and am investigating it. If you know a fix, feel free to open a pull request and I will evaluate it!

# Conclusions

## Thank you!

This was a few hour project, where most of the time was spent troubleshooting decryption not being completed successfully. As I said, this will be used in an open source password manager tool. If you see any security flaws with this, please let me know so I can be sure not to include them into my future project
