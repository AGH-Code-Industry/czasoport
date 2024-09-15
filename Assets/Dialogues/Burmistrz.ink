-> main

=== main ===
I don't recognize you. What are you doing here?
    *[Who are you?]
        -> mayor
    * [What is happening here? ]
        -> parade
    * [Sorry, didn't mean to disturb you.]
        -> bye

=== bye ===
Goodbye...
-> DONE
=== mayor ===
I'm the most important person in this city. I'm the mayor. But I lack the most important thing in the city which is my mace so probably that's why you didn't recognize me.

*[Why you don't have it?]
->lost_mace

=== lost_mace ===
I've lost it somewhere. I'm not sure where. Can you look for it? It's very important please. Give it to me when you find it.
*[Okay I will look for it]
-> bye
*[Exhchange #requiresItem: bulawa #getsItem: parada_se_idzie]
-> trade


=== parade ===
We are having a big parade to officially open new tombs. But we can't open it without my mace...
*[What happened to your mace?]
-> lost_mace
*[Good luck with it. Bye]
-> bye

=== trade ===
Thank you traveler! Now we can officially open the gates! Let's go people!
->DONE
