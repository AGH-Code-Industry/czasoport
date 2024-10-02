-> main

=== main ===
Oh you look interesting. Similar to something I found recently... do you want to take a look?
    * [You've got my attention, please go on.]
        -> show
    * [No, thank you. Bye.]
    ->bywaj

=== bywaj ===
Bye!
-> DONE

=== show ===
This is probably a part of your..device? I will give it to you if you can bring me some shiny rock. There are some laying on the ground on the main square sometimes.
    * [ #requiresItem: GreenRock #getsItem: CzasoportPart]
        -> trade

    * [I don't want anything. Bye.]
        -> bywaj

=== trade ===
Have a nice day!
-> DONE