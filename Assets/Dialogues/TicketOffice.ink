-> main

=== main ===
museum? iść? musisz kupić bilet
    * [Mam już bilet #requiresItem: TownHall/MuseumTicket #getsItem: TownHall/ValidatedMuseumTicket]
        -> ma_bilet
    * [Nie mam pieniędzy może dasz mi bilet za coś co mam]
        -> nie_ma_biletu
    * [Odejdź]
        -> DONE
        
=== nie_ma_biletu ===
10 monet albo nici z wymiany
-> DONE
     
=== ma_bilet ===
masz bilet? idź 
-> DONE