-> main

=== main ===
Hi. There are rumors that there is a time traveler in the city. Is it you?
    *[Yes. It's me. Why?]
        -> timetraveler
    * [Sorry, didn't mean to disturb you.]
        -> bywaj

=== bywaj ===
Bye.
-> DONE
=== timetraveler===
We are trying to find a beautiful new golden mineral. We know that its deposit is located somewhere in this area, unfortunately according to our knowledge it lays really deep underground - it's a lot of years of excavations...
...I suppose we could be really close to the deposit in the future. Could you go to the future and get us the mineral? 
*[Sure let me do that.]
-> gift
*[Sorry, I don't have time for that now]
-> bywaj

===gift===
Splendid! Bring it along and you will receive a gift from us.
-> DONE