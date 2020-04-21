//// Lesson 14 - Capstone 2


(*  14.1 Defining the problem
1 The application should allow a customer to deposit and withdraw from an
  account that the customer owns, and maintain a running total of the balance in
  the account.

2 If the customer tries to withdraw more money than is in the account, the transaction
  should be declined (the balance should stay as is).

3 The system should write out all transactions to a data store when they’re
  attempted. The data store should be pluggable (filesystem, console, and so forth).

4 The code shouldn’t be coupled to, for example, the filesystem or console input. It
  should be possible to access the code API directly without resorting to a console
  application.

5 Another developer will review your work, and that developer should be able to
  easily access all of the preceding components in isolation from one another.

6 The application should be an executable as a console application.

7 On startup, the system should ask for the customer’s name and opening balance.
  It then should create (in memory) an account for that customer with the specified
  balance.

8 The system should repeatedly ask whether the customer wants to deposit or
  withdraw money from the account.

9 The system should print out the updated balance to the user after every
  transaction.

*)

