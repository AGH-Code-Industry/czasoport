-> main

=== main ===
Hi, normal or discounted?
 *[What are you selling here?]
  -> museum_tickets
 * [I don't want to buy any..]
  -> bywaj

=== bywaj ===
Then go away, and don't create an artificial queue. NEXT!
-> DONE

=== museum_tickets ===
Tickets to the museum. You can't enter without one.
You can buy normal for one blue gem and discounted for a small green gem.
Discounted only if you are citizen of this city,
* [Exhchange #requiresItem: torch #getsItem: ticket]
-> trade
*[No, I don't have the gems.]
->bywaj

=== trade ===
Okay, thank you. Here is the ticket. NEXT!
-> DONE