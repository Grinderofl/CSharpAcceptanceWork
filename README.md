CSharpAcceptanceWork
====================

Testable skills:
• The ability to quickly and accurately write code that meets the given requirements


Assignment:
Write a console application that has a minimum of two independent threads, interacting with each other. Threads in turn consider a simple numerical arithmetic progression (take Fibonacci sequence as an example) so that the only one thread is working in a time and all other threads are sleeping (do nothing). But control can be passed to another thread thru the configuration file by manual changes in configuration file.  Next thread continues with the previous. So the natural numerical arithmetic progression order persists. Frequency of calculation operations progression for each of the threads is set separately in a single configuration file, which is stored inside the jar-file (its resources) and in the external directory. In the same file the current working thread given. 


The application should search and read the configuration from a manually specified external file, and if no external file found – read it from internal resource (like from a backup). The application should read the configuration from time to time, and then transfer control to one of the threads, according to the specified settings. Refresh rate settings as specified in the configuration file. All current information is displayed on the console. It is important to know which thread is working, what is the current result, the number of iteration and computing speed in a minute, when was the last configuration update made and when will be the next.

Additional plus if:

• The log4net logging system is used;

• The application does not have more threads than necessary for the task;
