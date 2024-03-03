-> main

=== main ===
Hello, how can I help you?
    * [Can you tell me how to get inside to this building?]
        -> to_rafinery
    * [I am just passing by, sorry. Bye]
    ->DONE
        
=== bywaj ===
You can't go inside then. Bye!
-> DONE

== to_rafinery ==
This is rafinery, this is  where inventions come to life and all plans about the city improvements are tested, if you want to go through the door you need a code. Do you have the code?
    * [Exhchange #requiresItem: Torch #getsItem: BeigeGem]
        -> trade
    * [I don't.]
        -> bywaj
        
=== trade ===
I see that you do. Enjoy the laboratory inside but please try to not distrub the scientists!
-> DONE
