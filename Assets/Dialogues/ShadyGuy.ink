-> main

=== main ===
Oh, you look like a smart guy...I think we can help each other. 
    * [How do you think we can help each other?]
        -> steal
    * [No, thank you. I'm not interested right now.]
    ->DONE
        
=== bywaj ===
Bye!
-> DONE

=== steal ===
I have something that I think belogns to you. A part of a device. If you would bring me that big gem from the upper left room we can make a little exchange. 
    * [I have what you want, let's make the deal.]
        -> show
    * [No, thank you. I'm not interested right now.]
    ->DONE

== show ==
    * [Exchange #requiresItem: Crystal #getsItem: CzasoportPart]
        -> trade
    * [Bye]
        -> bywaj
        
=== trade ===
If anyone asks, we don't know each other right?
-> DONE
-> main
