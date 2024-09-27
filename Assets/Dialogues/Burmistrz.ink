-> main

=== main ===
I don't recognize you. What are you doing here?
    *[Who are you?]
        -> mayor
    * [What is happening here?]
        -> parade
    * [Sorry, didn't mean to disturb you.]
        -> bye

=== bye ===
Goodbye...
-> DONE
=== mayor ===
I'm the most important person in this city - the mayor. But I lack the most important thing in the city - my mace. Perhaps, that's why you didn't recognize me.

*[Why don't you have it?]
->lost_mace

=== lost_mace ===
It has got lost in quite strange circumstances. Hmm, could I ask you for a favor, traveler? I'd be delighted if you could find it and deliver to me.
*[Okay, I will search for your mace.]
-> bye
*[Exchange #requiresItem: bulawa]
-> trade


=== parade ===
We are having a grand parade to officially celebrate opening of the new tombs. But what is a city parade without its mayor with his magnificent mace?
*[What happened to your mace?]
-> lost_mace
*[Good luck with it. Bye]
-> bye

=== trade ===
Thank you traveler! Now we can officially open the gates! Let's go people!
->DONE
