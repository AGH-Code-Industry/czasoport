-> main

=== main ===
Hi traveler. Would you like to buy something? Didn't I see you already somewhere? In the past perhaps?
    *[Yes. I think we met. Can I buy a pickaxe?]
        -> seller
    * [Thanks, I'm not interested. ]
        -> bye

=== bye ===
Goodbye. Come back when you need something!
-> DONE
=== seller ===
Oh yes! of course. Here it is, a pickaxe for your help on the past! Thanks a lot!
*[ #getsItem: pickaxe]
-> bye