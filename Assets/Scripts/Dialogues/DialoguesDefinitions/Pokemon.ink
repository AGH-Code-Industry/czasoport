-> main

=== main ===
Which pokemon do you choose?
    * [Charmander #requiresItem: Tutorial/KeyCircle #getsItem: Torch]
        -> chosen("Charmander")
    * [Bulbasaur]
        -> bulb
    * [Squirtle]
        -> squirtle
        
=== bulb ===
Bulbasaurs are pretty nasty, I don't want to speak with you anymore...
-> DONE

== squirtle ===
I LOVE Squirtle! Do you have one on you?
    * [Tak]
        -> playWithSquirtle
    * [Nie]
        No dobrze, to nie mamy o czym rozmawiać...
        -> DONE
    * [Co cię to obchodzi? # requiresItem: WaterBucket] 
        Łałałiła, nie to nie...
        -> DONE
-> DONE

=== playWithSquirtle ===
Wspaniale! Mogę zobaczyć?
A w ogóle wiesz jak trzymać pokemony?
Jasne że wiem! Proszę, mogę potrzymać?
    * [Tak # requiresItem: Torch] 
        Tak, patrz jaki piękny okaz
        Naprawdę super, ale muszę już lecieć. Mam nadzieję że jeszcze się spotkamy!
        -> DONE
    * [Nie]
        No cóż, nie można mieć wszystkiego co się chce...
        -> DONE
        
=== chosen(pokemon) ===
You chose {pokemon}! Not really a surprising choice... have anything better?
-> main