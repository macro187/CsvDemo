Brief
=====

Would you mind completing the exercise described below.  Hopefully the task itself will be very
simple to implement, but we'd like to see you demonstrate how you would decompose the problem as
well as unit test the components you write.  Feel free to use the libraries that you’re most
comfortable with, but could you write the code itself in C#.

Can you provide us with a program that reads a CSV file and outputs two text files. 

The first should show the frequency of the first and last names ordered by frequency and then
alphabetically. 

The second should show the addresses sorted alphabetically by street name. 

As mentioned can you also include a unit test project using which ever unit testing framework you’re
most comfortable with.



Requirements
============

    REQ-01  A program

    REQ-02  Written in C#

    REQ-03  Reads a CSV file

    REQ-04  Outputs a text file containing frequency of the first and last names ordered by
            frequency and then alphabetically

    REQ-05  Outputs a text file containing addresses sorted alphabetically by street name

    REQ-06  Includes a unit test project

    REQ-07  Design consisting of multiple components, each reflecting a logical part of the overall
            problem

    REQ-08  Unit test suite for each component



Assumptions
===========

    -   REQ-05 after StreetName, also sort by additional fields to break ties and ensure order
        stability: StreetNumber, City, State, PostCode

    -   CSV header present

    -   CSV fields are in a known order

    -   No invalid/unexpected/missing CSV data

    -   No CSV data that differs by case only

    -   No CSV data containing comma, quote, or newline characters

    -   Input CSV file is UTF8 w/ native line-endings

    -   Output CSV files are UTF8 w/ native line-endings



Tasks
=====

    [x] Produce a test CSV file containing data that, if processed, could demonstrate all required
        functionality

        To demonstrate REQ-04:

        -   FirstName field

        -   LastName field

        -   Records consisting of a variety of first and last names, to demonstrate alphabetical sorting

        -   Records consisting of duplicate first and last names, not necessarily as part of the
            same full names, in various quantities, to demonstrate frequency counting and sorting

        To demonstrate REQ-05:

        -   StreetNumber field

        -   StreetName field

        -   City field

        -   State field

        -   Postcode field

        -   Records consisting of a variety of street names, to demonstrate sorting

        -   Records consisting of duplicate addresses, to demonstrate deduplication


    [x] Produce a hand-written REQ-04 output file correct for the example CSV


    [x] Produce a hand-written REQ-05 output file correct for the example CSV


    [x] C# console program that reads and parses all lines from the test CSV file, maintains
        necessary counts/tallies as it goes, and then produces the required report files

        Delivers:
        REQ-01
        REQ-02
        REQ-03
        REQ-04
        REQ-05


    [x] C# unit test project that invokes the above console program and verifies the resulting
        report files against the hand-written solutions

        Delivers:
        REQ-06


    [x]  Use new test CSV data


    [x]  Produce hand-written REQ-04 output file for the new example CSV


    [x]  Produce hand-written REQ-05 output file for the new example CSV


    []  Adjust code to process new CSV data


    []  Use a CSV reader from NuGet to eliminate CSV reading/writing limitations


    []  Factor monolithic console program out into modules as per REQ-07


    []  Produce unit test suites for each module as per REQ-08


    []  Improve documentation comments / readme

