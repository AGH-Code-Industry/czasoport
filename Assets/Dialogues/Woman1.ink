-> main

=== main ===
Hello, how can I help you?
*[Can I get the A38 form?]
  -> A38
*[Can I get the blue form?]
-> blue
* [Sorry, didn't mean to disturb you.]
    -> bywaj

=== bywaj ===
Bye. NEXT!
-> DONE
=== administrator ===
Find administrator and ask him about it. He should be at the very back of the building.
-> DONE

=== A38 ===
There must have been a misinformation somewhere. To get the A38 form you need to look for the window 2.
    *[Ok, thank you...]
    -> bywaj

=== blue ===
To get the blue form you need to fill in the orange one first. Do you have it?
    *[Exchange #requiresItem: OrangeForm #getsItem: BlueForm]
-> trade
    *[No.]
        -> noBlue
        
=== noBlue ===
You can then find it in the window number 4. Thank you. NEXT!
-> DONE

=== trade ===
Here you are. Thank you. NEXT!
-> DONE