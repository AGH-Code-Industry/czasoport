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
You can buy normal for one 10 blue gems and discounted for 10 green gems.
Discounted only if you are citizen of this city,
*[No, I don't have the gems.]
->bywaj

=== trade ===
Okay, thank you. Here is the ticket. NEXT!
-> DONE