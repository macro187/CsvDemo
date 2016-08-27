Brief
=====

Would you mind completing the exercise described below.  Hopefully the task itself will be very simple to implement, but
we'd like to see you demonstrate how you would decompose the problem as well as unit test the components you write.
Feel free to use the libraries that you’re most comfortable with, but could you write the code itself in C#.

Can you provide us with a program that reads a CSV file and outputs two text files. 

The first should show the frequency of the first and last names ordered by frequency and then alphabetically. 

The second should show the addresses sorted alphabetically by street name. 

As mentioned can you also include a unit test project using which ever unit testing framework you’re most comfortable
with.



Requirements
============

REQ-01  A program

REQ-02  Written in C#

REQ-03  Reads a CSV file

REQ-04  Outputs a text file containing frequency of the first and last names ordered by frequency and then
        alphabetically

REQ-05  Outputs a text file containing addresses sorted alphabetically by street name

REQ-06  Includes a unit test project



Assumptions
===========

-   No more than one person at the same address
-   CSV fields are in a known order, no header required
-   No invalid/unexpected/missing CSV data
-   No CSV data that differs by case only
-   No CSV data containing comma, quote, or newline characters
-   Input CSV file is UTF8 w/ native line-endings
-   Output CSV files are UTF8 w/ native line-endings



Tasks
=====

    [x] Produce CSV data that, when processed, demonstrates the required functionality

        To demonstrate REQ-04:

        -   FirstName field
        -   LastName field
        -   Records consisting of a variety of first and last names, to demonstrate alphabetical sorting
        -   Records consisting of duplicate first and last names, not necessarily as part of the same full names, in
            various quantities, to demonstrate frequency counting and sorting

        To demonstrate REQ-05:

        -   StreetNumber field
        -   StreetName field
        -   City field
        -   State field
        -   Postcode field
        -   Records consisting of a variety of street names, to demonstrate sorting


    []  C# class library implementing required functionality in terms of lines/chars/strings


    []  C# unit test project for class library

        Hard-coded test CSV data as per above

        Hard-coded correct REQ-04 output to check against

        Hard-coded correct REQ-05 output to check against

        Test method that pipes test CVS data to library and checks for correct output text data


    []  Thin C# console program wrapper that pipes CSV data from an input file to the library, and then pipes output
        from the library to two text files

