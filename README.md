# Tic-Tac-Toe - Developed with Unity

Welcome to the ultimate showdown of X’s and O’s! This repo holds the source code for the classic Tic-Tac-Toe—but with a twist: you’re up against an AI opponent that’s ready to bring its A-game. Choose your difficulty wisely:

* Normal: The AI's warming up—think of it as a friendly sparring match. You’ll probably win, but hey, even computers need confidence boosters.
* Hard: Now it’s serious. The AI’s getting clever, so don’t underestimate it. Victory is still possible... if you’re on top of your game.
* Unbeatable: Brace yourself. This AI’s been training for this moment. It’s flawless, relentless, and possibly out to make you question your own logic. Your fight will be legendary (but, honestly, good luck beating it).

Think you've got what it takes? Let the Tic-Tac-Toe battles begin! Play the game here - https://ehsan-ashik.github.io/tic-tac-toe-unity-game/


# Agent Intelligence 

The AI agent's intelligence relies on the *Minimax Algorithm*, a strategy used to make optimal decisions in competitive games by simulating possible moves and choosing the one that minimizes potential loss while maximizing gain. In each game state, Minimax predicts all future moves to anticipate the opponent’s best responses, ensuring the AI picks the most favorable outcome.

*Alpha-Beta Pruning* optimizes this by "pruning" or cutting off branches of moves that won’t affect the final decision, thus speeding up the process. It skips unnecessary checks by using two values — *alpha* (the best option for the maximizing player) and *beta* (for the minimizing player) — to avoid analyzing moves that won’t influence the outcome. This combination allows the AI agent to evaluate moves faster without compromising decision quality.


# References

1. Minmax algorithm - https://en.wikipedia.org/wiki/Minimax
2. Alpha-Beta pruning - https://en.wikipedia.org/wiki/Alpha%E2%80%93beta_pruning 