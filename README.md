# Run-an-Embedded-encrypted-dll-file-from-resource
In this program i first created a dll file which has a function named "add", this add fucntions takes 2 arguments and returns the sum of two numbers. 
That dll was encrypted with SHA-256 and than it was added as embedded resource of this program. 
So when this program is run, it first decrypt the encrypted dll file inside memory and than use the "add" function of dll file.


