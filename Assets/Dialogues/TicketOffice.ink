-> main

=== main ===
Museum? Visit? Must buy ticket.
    * [I have the ticket. #requiresItem: TownHall/MuseumTicket #getsItem: TownHall/ValidatedMuseumTicket]
        -> ma_bilet
    * [I don't have any money, maybe you'd like to trade for something.]
        -> nie_ma_biletu
    * [*Go away*]
        -> DONE
        
=== nie_ma_biletu ===
10 gold coins or no trade.
-> DONE
     
=== ma_bilet ===
Have ticket? Go! 
-> DONE