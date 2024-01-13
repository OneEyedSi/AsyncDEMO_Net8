# AsyncDEMO_Net8
A cross-platform console application demonstrating async programming in C# and .NET 8.  It is loosely based on MS article "Asynchronous programming with async and await", https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/.

The various versions of the code, from fully synchronous to full asynchronous functionality, are saved side-by-side, rather than as different commits in source control.  The different versions can be accessed via a menu.

The scenario developed is cooking breakfast.  The tasks involved are:

1. Make coffee.
2. Heat a frying pan.
3. Fry two eggs.
4. Fry three slices of bacon.
5. Toast two pieces of bread.
6. Add butter and jam to the toast.
7. Pour a glass of orange juice.

The synchronous version of the code executes the steps in the order shown:

![The steps in cooking breakfast the naive way, performing each task in turn synchronously](Images/SynchronousProcess.png "Synchronous process for cooking breakfast")

The time to cook breakfast is the sum of the times for each task.

Adding the butter and jam to the toast and pouring the juice are quick tasks.  Making the coffee, heating the frying pan, frying the eggs, frying the bacon and making the toast are long-running tasks.  Heating the frying pan, frying the eggs and frying the bacon must be done sequentially, since there is only one frying pan.  The other long-running tasks can be done in parallel with the frying, however, to reduce the overall cooking time.  Ideally, we would want the various frying tasks, making the coffee and making the toast to complete at about the same time, so food or drink doesn't get cold.

So the most efficient way of executing the process would be:

![The steps in cooking breakfast the most efficient way, performing long-running tasks asynchronously](Images/FinalProcess.png "Most efficient process for cooking breakfast")

This requires the long-running steps to be performed asynchronously.