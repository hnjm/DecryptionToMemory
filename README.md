# Decryption To Memory
A C# tool to decrypt information to the process memory only. A major part of my upcoming password manager

## Contents
- Information
    * [Purpose](#purpose)

- Setup
    * [Test Instructions](#test-instructions)
    
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
