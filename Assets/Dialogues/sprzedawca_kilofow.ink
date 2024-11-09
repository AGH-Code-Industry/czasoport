-> main

=== main ===
This is my customer! I'm done talking to you! Thief! What am I supposed to sell to him? Nothing?
Sorry sir. Would you like something?
    *[Who are you?]
        -> seller
    * [What are you arguing about? ]
        -> thief
    * [Sorry, didn't mean to disturb you.]
        -> bye

=== bye ===
Goodbye. Come back when you need something!
-> DONE
=== seller ===
I'm selling axes here. Very handy for destroying big rocks. But for now I can't really sell you anything.
*[Why are you not selling?]
-> thief

=== thief ===
Because someone is stealing my components to make axes! I'm telling you it's the chisel seller!
*[Okay I understand. I will leave you to it.]
-> bye
*[I will try to get to know what is happening here.]
-> the_truth


=== the_truth ===
Thanks. Tell me when you know anything.
*[Okay bye. ]
-> bye
*[#requiresItem: CzescDoKradnieciaKilofa #getsItem: Pickaxe]
-> lost_components

=== lost_components ===
Oh I see. So that was the shovel seller at the end... Thank you.
->DONE

